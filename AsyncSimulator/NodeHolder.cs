using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSimulator
{
    public class NodeHolder
    {
        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        List<_Node> AllNodes { get; set; }

        object AllNodesLock { get; set; }

        public int NodeCount { get; private set; }

        public NodeHolder()
        {
            AllNodes = new List<_Node>();
            AllNodesLock = new object();
        }

        public void EmptyAllNodes()
        {
            lock (AllNodesLock)
            {
                AllNodes.Clear();
                NodeCount = 0;
            }
        }

        public void AddNode(_Node node)
        {
            lock (AllNodesLock)
            {
                NodeCount++;
                AllNodes.Add(node);
            }
        }

        public void RemoveNode(_Node node)
        {
            lock (AllNodesLock)
            {
                AllNodes.Remove(node);
            }
        }

        public _Node GetMinimumNode()
        {
            lock (AllNodesLock)
            {
                return AllNodes.FirstOrDefault(fn => fn.Id == AllNodes.Min(n => n.Id)); 
            }
        }

        public void RedrawAllNodes()
        {
            lock (AllNodesLock)
            {
                AllNodes.ForEach(n => n.Visualizer.Draw(n.GetState()));
            }
        }

        public bool AnyIntersecting(PointF p)
        {
            lock (AllNodesLock)
            {
                return AllNodes.Any(n => n.Visualizer.Intersects(p));
            }
        }


        public _Node GetNodeAt(Point p)
        {
            lock (AllNodesLock)
            {
                return AllNodes.FirstOrDefault(n => n.Visualizer.OnIt(p));
            }
        }

        public _Node GetNodeAt(int i)
        {
            lock (AllNodesLock)
            {
                return AllNodes[i];
            }
        }

        public _Node GetNodeById(int id)
        {
            lock (AllNodesLock)
            {
                return AllNodes.FirstOrDefault(n => n.Id == id);
            }
        }

        [Obsolete]
        public List<_Node> GetCopyList()
        {
            lock (AllNodesLock)
            {
                return new List<_Node>(AllNodes);
            }
        }

        public bool DetectingTermination { get; set; }

        public void StartTerminationDetection()
        {
            DetectingTermination = true;
            Task.Run(() =>
            {
                while (DetectingTermination)
                {
                    Thread.Sleep(100);
                    lock (AllNodesLock)
                    {
                        if (AllNodes.All(n => n.IsValid() && DateTime.Now.Subtract(n.LastReceivedMessageTime) > TimeSpan.FromMilliseconds(400)))
                        {
                            OnTerminated();
                            DetectingTermination = false;
                        }
                    }
                }
            });
        }

        public event EventHandler Terminated;
        protected virtual void OnTerminated()
        {
            DetectingTermination = false;
            Terminated?.Invoke(this, EventArgs.Empty);
        }
    }
}
