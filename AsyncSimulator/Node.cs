using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSimulator
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class _Node //: ISerializableNode
    {
        /// <summary>
        /// Id of a node.
        /// </summary>
        [JsonProperty]
        public int Id { get; set; }

        public NodeHolder NodeHolder { get; set; }

        /// <summary>
        /// List of neighbours of this node. Empty initially.
        /// </summary>
        public Dictionary<int, _Node> Neighbours { get; set; }

        [JsonProperty]
        public List<int> _Neighbours { get; set; }
        [JsonProperty]
        public PointF _Position { get; set; }

        /// <summary>
        /// A thread safe queue for received messages.
        /// </summary>
        protected MessageQueue<Message> ReceiveQueue { get; set; }

        /// <summary>
        /// This is the hooker of the strategy pattern.
        /// </summary>
        public IVisualizer Visualizer { get; set; }

        object ReceiveLock { get; set; }
        object NeighbourhoodLock { get; set; }

        int PreviousReceiveQueueLength { get; set; }

        int BackoffPeriod = 10;
        
        public int SentMessageCount { get; set; }
        public int ReceivedMessageCount { get; set; }

        public int MoveCount { get; set; }

        public int MessageJammed { get; set; }

        public DateTime LastReceivedMessageTime { get; set; }

        /// <summary>
        /// As soon a node is created, the thread starts running.
        /// </summary>
        /// <param name="id"></param>
        public _Node(int id, NodeHolder nodeHolder)
        {
            Id = id;

            NodeHolder = nodeHolder;

            /// initialize lists
            Neighbours = new Dictionary<int, _Node>();
            ReceiveQueue = new MessageQueue<Message>();
            ReceiveQueue.MessageAdded += ReceiveQueue_NewMessage;

            ReceiveLock = new object();
            NeighbourhoodLock = new object();
        }

        private void ReceiveQueue_NewMessage(object sender, EventArgs e)
        {
            if (ReceiveQueue.Count == 0) return;

            Message m = null;

            //Trace.WriteLine(String.Format("Acquiring lock - {0}", m));
            lock (ReceiveLock)
            {
                m = ReceiveQueue.Dequeue();
                //Trace.WriteLine(string.Format("receive queue has {0} messages after dequeueing {1}", ReceiveQueue.Count, m));

                HandleCongestionBackoff();

                //Trace.WriteLine(String.Format("Acquiried lock - {0}", m));
                UserDefined_ReceiveMessageProcedure(m);
                //Trace.WriteLine(String.Format("Releasing lock - {0}", m));
            }
            //Trace.WriteLine(String.Format("Released lock - {0}", m));
        }

        void HandleCongestionBackoff()
        {
            if (ReceiveQueue.Count >= 1 && PreviousReceiveQueueLength == 0)
            {
                MessageJammed++;
                Trace.WriteLine(string.Format("congested at {0}", Id));
                ReceiveQueue.ForEach(message => message.Source.SentMessageCount++);
                ReceivedMessageCount++;
                Thread.Sleep(BackoffPeriod);
            }
            PreviousReceiveQueueLength = ReceiveQueue.Count;
        }

        public virtual bool Selected()
        {
            return false;
        }
        
        public abstract NodeState GetState();

        //public abstract void SetState(NodeState nodeState);

        /// <summary>
        /// This method will be implemented in sub classes for algorithm details.
        /// </summary>
        /// <param name="m"></param>
        protected void UserDefined_ReceiveMessageProcedure(Message m)
        {
            ReceivedMessageCount++;
            LastReceivedMessageTime = DateTime.Now;
            UpdateNeighbourInformation(m.Source);
            RunRules();
        }

        protected abstract void UpdateNeighbourInformation(_Node neighbour);

        protected abstract void RunRules();

        #region initiator

        /// <summary>
        /// This method will be implemented for single initiation strategies.
        /// </summary>
        /// <param name="root"></param>
        public void UserDefined_SingleInitiatorProcedure()
        {
            Task.Run(() => RunRules());
        }

        #endregion

        #region Sender

        protected void BroadcastState()
        {
            lock (NeighbourhoodLock)
            {
                foreach (var neighbor in Neighbours)
                {
                    Task.Run(() =>
                    {
                        Underlying_Send(new Message
                        {
                            Source = this,
                            DestinationId = neighbor.Key
                        });
                    });
                }
            }

            Task.Run(() => RunRules());
        }

        /// <summary>
        /// This method puts the given message to destinations ReceiveQueue
        /// </summary>
        /// <param name="m"></param>
        public void Underlying_Send(Message m)
        {
            var destination = NodeHolder.GetNodeById(m.DestinationId);
            if (destination == null)
            {
                throw new ArgumentNullException("Destination");
            }
            SentMessageCount++;
            destination.ReceiveQueue.Enqueue(m);
        }

        #endregion

        #region Neighborhood management

        public void AddNeighbour(_Node node)
        {
            lock (NeighbourhoodLock)
            {
                Neighbours.Add(node.Id, node);
            }
        }

        public void RemoveNeighbour(int nodeId)
        {
            lock (NeighbourhoodLock)
            {
                Neighbours.Remove(nodeId);
            }
        }

        public void UpdateNeighbour(_Node neighbour)
        {
            lock (NeighbourhoodLock)
            {
                Neighbours[neighbour.Id] = neighbour;
            }
        }

        public List<_Node> GetCopyOfNeigbours()
        {
            lock (NeighbourhoodLock)
            {
                return new List<_Node>(Neighbours.Values);
            }
        }

        public bool IsNeigbourOf(int nodeId)
        {
            lock (NeighbourhoodLock)
            {
                return Neighbours.ContainsKey(nodeId);
            }
        }

        #endregion

        public virtual bool IsValid()
        {
            return false;
        }

        //public abstract string Serialize();
    }
}
