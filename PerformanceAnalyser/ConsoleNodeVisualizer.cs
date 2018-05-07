using AsyncSimulator;
using System;
using System.Diagnostics;

namespace PerformanceAnalyserLibrary
{
    class ConsoleNodeVisualizer : IVisualizer
    {
        public System.Drawing.PointF Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private EdgeHolder EdgeHolder { get; set; }
        private int Id { get; set; }
        
        public ConsoleNodeVisualizer(int id, EdgeHolder edgeHolder)
        {
            Id = id;
            EdgeHolder = edgeHolder;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Draw(NodeState nodeState)
        {
            if (nodeState == NodeState.IN)
            {
                Console.WriteLine("I'm({0}) IN", Id);
            }
            else
            {
                //Console.WriteLine("I'm({0}) not IN", Id);
            }
        }

        public IEdge GetEdgeTo(_Node n2)
        {
            foreach (var edge in EdgeHolder.GetCopyList())
            {
                if ((edge.GetNode1().Id == Id && edge.GetNode2().Id == n2.Id) ||
                    (edge.GetNode2().Id == Id && edge.GetNode1().Id == n2.Id))
                {
                    return edge;
                }
            }

            return null;
        }

        public bool Intersects(System.Drawing.PointF p)
        {
            throw new NotImplementedException();
        }

        public void Log(string l, params object[] args)
        {
            Trace.WriteLine(string.Format(l, args));
        }

        public bool OnIt(System.Drawing.PointF p)
        {
            throw new NotImplementedException();
        }

        public void VisualizeMessage(Message m)
        {
            throw new NotImplementedException();
        }
    }
}
