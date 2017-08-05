using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualInterface
{
    internal class NodeHolder
    {
        DrawingPanelHelper DrawingPanelHelper { get; set; }
        
        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        List<_Node> AllNodes { get; set; }

        object AllNodesLock { get; set; }

        public int NodeCount { get; private set; }

        public NodeHolder(DrawingPanelHelper drawingPanelHelper)
        {
            DrawingPanelHelper = drawingPanelHelper;
            AllNodes = new List<_Node>();
            AllNodesLock = new object();
        }

        public void EmptyAllNodes()
        {
            lock (AllNodesLock)
            {
                AllNodes.Clear();
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
                AllNodes.ForEach(n => n.Visualizer.Draw(n.Selected()));
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

        [Obsolete]
        public List<_Node> GetCopyList()
        {
            lock (AllNodesLock)
            {
                return new List<_Node>(AllNodes);
            }
        }
    }
}
