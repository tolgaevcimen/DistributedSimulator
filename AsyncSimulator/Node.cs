using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSimulator
{
    public interface IVisualizer
    {
        /// <summary>
        /// Location of this node.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Checks whether the given point is on this node.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool OnIt ( Point p );

        /// <summary>
        /// Checks whether the given point is dangerously close to this node.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool Intersects ( Point p );

        /// <summary>
        /// Method for drawing the node.
        /// </summary>
        /// <param name="changeColor">if set to true, the color of the node will change from default color to selected color.</param>
        void Draw ( bool changeColor = false );

        /// <summary>
        /// Method for showing a selected edge. Changes an edges color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="m"></param>
        void VisualizeMessage ( Message m );

        /// <summary>
        /// Method for reverting a selected edge. Changes an edges color to unselected color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        void RevertEdgeBetween ( _Node n1, _Node n2 );

        /// <summary>
        /// Method for showing a text message.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="args"></param>
        void Log ( string l, params object[] args );
    }

    public abstract class _Node
    {
        /// <summary>
        /// Id of a node.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of neighbours of this node. Empty initially.
        /// </summary>
        public List<_Node> Neighbours { get; set; }

        /// <summary>
        /// A thread safe queue for received messages.
        /// </summary>
        protected ConcurrentQueue<Message> ReceiveQueue { get; set; }

        /// <summary>
        /// Flag for running state of the underlying thread.
        /// </summary>
        protected bool Running { get; set; }

        /// <summary>
        /// This is the hooker of the strategy pattern.
        /// </summary>
        public IVisualizer Visualizer { get; set; }

        /// <summary>
        /// As soon a node is created, the thread starts running.
        /// </summary>
        /// <param name="id"></param>
        public _Node ( int id )
        {
            Id = id;

            /// initialize lists
            Neighbours = new List<_Node>();
            ReceiveQueue = new ConcurrentQueue<Message>();

            /// initiate the thread. Magic occurs here as this call create a thread based running.
            Task.Run(() => Receive());
        }
        
        /// <summary>
        /// This method will be implemented in sub classes for algorithm details.
        /// </summary>
        /// <param name="m"></param>
        protected abstract void UserDefined_ReceiveMessageProcedure ( Message m );

        #region initiator

        /// <summary>
        /// This method will be implemented for single initiation strategies.
        /// </summary>
        /// <param name="root"></param>
        public abstract void UserDefined_SingleInitiatorProcedure ( _Node root );

        /// <summary>
        /// This method will be implemented for concurrent initiation strategies.
        /// </summary>
        /// <param name="allNodes"></param>
        public abstract void UserDefined_ConcurrentInitiatorProcedure ( List<_Node> allNodes );

        #endregion

        #region Sender

        /// <summary>
        /// This method puts the given message to destinations ReceiveQueue
        /// </summary>
        /// <param name="m"></param>
        public void Underlying_Send ( Message m )
        {
            m.Destination.ReceiveQueue.Enqueue(m);
        }

        #endregion

        #region Receiver

        /// <summary>
        /// The implementation of the mechanism running in thread.
        /// </summary>
        void Receive ()
        {
            while ( true )
            {
                if ( Running )
                {
                    Message incomingMessage;
                    while ( !ReceiveQueue.TryDequeue(out incomingMessage) ) { Thread.Sleep(50); }
                    
                    UserDefined_ReceiveMessageProcedure(incomingMessage);
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }
        
        #endregion

        /// <summary>
        /// Starts the node
        /// </summary>
        public void Start()
        {
            Running = true;
        }

        /// <summary>
        /// Stops the node
        /// </summary>
        public void Stop()
        {
            Running = false;
        }
    }

    public class Message
    {
        public _Node Source { get; set; }
        public _Node Destination { get; set; }
        public MessageTypes MessageType { get; set; }
        public object Data { get; set; }

        public override string ToString ()
        {
            return string.Format("SId: {0}, DId: {1}", Source != null ? Source.Id : -1, Destination != null ? Destination.Id : -1);
        }
    }

    public enum MessageTypes
    {
        Layer,
        Ack,
        Reject,

        Token
    }
}
