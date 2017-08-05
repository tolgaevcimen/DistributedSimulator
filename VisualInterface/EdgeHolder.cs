using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualInterface
{
    internal class EdgeHolder
    {
        DrawingPanelHelper DrawingPanelHelper { get; set; }
       
        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        List<VisualEdge> AllEdges { get; set; }

        object AllEdgesLock { get; set; }

        public EdgeHolder(DrawingPanelHelper drawingPanelHelper)
        {
            DrawingPanelHelper = drawingPanelHelper;
            AllEdges = new List<VisualEdge>();
            AllEdgesLock = new object();
        }

        public void EmptyAllEdges()
        {
            AllEdges.Clear();
        }

        public void RemoveEdge(VisualEdge edge)
        {
            lock (AllEdgesLock)
            {
                AllEdges.Remove(edge);
            }
        }

        public void RedrawAllEdges()
        {
            lock (AllEdgesLock)
            {
                AllEdges.ForEach(e => e.Draw(null));
            }
        }

        public List<VisualEdge> GetRelatedEdges(_Node node, out List<_Node> relatedNodes)
        {
            var edgesToBeRemoved = new List<VisualEdge>();
            relatedNodes = new List<_Node>();

            lock (AllEdgesLock)
            {
                // go through all edges, find the nodes and edges adjacent
                foreach (var edge in AllEdges)
                {
                    if (edge.Node1 == node)
                    {
                        edgesToBeRemoved.Add(edge);
                        relatedNodes.Add(edge.Node2);
                    }
                    else if (edge.Node2 == node)
                    {
                        edgesToBeRemoved.Add(edge);
                        relatedNodes.Add(edge.Node1);
                    }
                } 
            }

            return edgesToBeRemoved;
        }

        public void AddEgde(VisualEdge edge)
        {
            lock (AllEdgesLock)
            {
                AllEdges.Add(edge); 
            }
        }

        public VisualEdge FindEdge(Point location)
        {
            lock (AllEdgesLock)
            {
                return AllEdges.FirstOrDefault(edge => edge.Path.IsOutlineVisible(location, edge.SolidPen));
            }
        }

        [Obsolete]
        public List<VisualEdge> GetCopyList()
        {
            lock (AllEdgesLock)
            {
                return new List<VisualEdge>(AllEdges);
            }
        }
    }
}
