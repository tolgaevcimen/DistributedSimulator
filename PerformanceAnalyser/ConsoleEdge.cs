using AsyncSimulator;
using System;
using System.Drawing;

namespace PerformanceAnalyserLibrary
{
    class ConsoleEdge : AbstractEdge
    {
        public ConsoleEdge(_Node node1, _Node node2) : base(node1, node2)
        {
            HandleVisualization();
        }

        protected override void HandleVisualization()
        {
            Draw();
        }

        public override void Colorify(bool reverted)
        {
            Console.WriteLine("edge between {0}-{1} is colorified. (Reverted: {2})", Node1.Id, Node2.Id, reverted);
        }

        public override void Delete(bool onlyEdgeDeleted)
        {
            base.Delete(onlyEdgeDeleted);
        }

        public override void Draw()
        {
            //Console.WriteLine("There is an edge between {0}-{1}", Node1.Id, Node2.Id);
        }

        public override bool OnPoint(Point location)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("({0}-{1})", Node1.Id, Node2.Id);
        }
    }
}