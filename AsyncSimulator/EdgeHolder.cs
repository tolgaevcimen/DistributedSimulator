using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AsyncSimulator
{
    public class EdgeHolder
    {
        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        List<IEdge> AllEdges { get; set; }

        object AllEdgesLock { get; set; }

        public EdgeHolder()
        {
            AllEdges = new List<IEdge>();
            AllEdgesLock = new object();
        }

        public void EmptyAllEdges()
        {
            AllEdges.Clear();
        }

        public void RemoveEdge(IEdge edge)
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
                AllEdges.ForEach(e => e.Draw());
            }
        }

        public List<IEdge> GetRelatedEdges(_Node node, out List<_Node> relatedNodes)
        {
            var edgesToBeRemoved = new List<IEdge>();
            relatedNodes = new List<_Node>();

            lock (AllEdgesLock)
            {
                // go through all edges, find the nodes and edges adjacent
                foreach (var edge in AllEdges)
                {
                    if (edge.GetNode1() == node)
                    {
                        edgesToBeRemoved.Add(edge);
                        relatedNodes.Add(edge.GetNode2());
                    }
                    else if (edge.GetNode2() == node)
                    {
                        edgesToBeRemoved.Add(edge);
                        relatedNodes.Add(edge.GetNode1());
                    }
                } 
            }

            return edgesToBeRemoved;
        }

        public void AddEgde(IEdge edge)
        {
            lock (AllEdgesLock)
            {
                AllEdges.Add(edge); 
            }
        }

        public IEdge FindEdge(Point location)
        {
            lock (AllEdgesLock)
            {
                return AllEdges.FirstOrDefault(edge => edge.OnPoint(location));
            }
        }

        [Obsolete]
        public List<IEdge> GetCopyList()
        {
            lock (AllEdgesLock)
            {
                return new List<IEdge>(AllEdges);
            }
        }
    }
}
