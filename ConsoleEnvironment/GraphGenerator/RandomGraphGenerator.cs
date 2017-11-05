using AsyncSimulator;
using System;
using System.Linq;

namespace ConsoleEnvironment.GraphGenerator
{
    class RandomGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            var randomizer = new Random();

            foreach (var node1 in nodeHolder.GetCopyList())
            {
                foreach (var node2 in nodeHolder.GetCopyList().Where(n => n != node1))
                {
                    if (randomizer.Next() % 100 > 10) continue;

                    if (node1.Neighbours.Contains(node2)) continue;
                    
                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
            }
        }
    }
}
