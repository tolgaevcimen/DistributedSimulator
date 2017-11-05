using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AsyncSimulator;

namespace ConsoleEnvironment.GraphGenerator
{
    class TripletTreeGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            var queue = new Queue<int>();
            for (int i = 1; i < nodeCount; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                var parent = nodeHolder.GetNodeAt(i);

                for (int j = 0; j < 3; j++)
                {
                    if (!queue.Any()) break;

                    var firstChildId = queue.Dequeue();
                    var child = nodeHolder.GetNodeAt(firstChildId);
                    
                    edgeHolder.AddEgde(new ConsoleEdge(parent, child));
                }

            }
        }
    }
}
