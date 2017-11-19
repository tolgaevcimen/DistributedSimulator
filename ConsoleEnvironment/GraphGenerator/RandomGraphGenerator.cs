using AsyncSimulator;
using System;
using System.Linq;

namespace ConsoleEnvironment.GraphGenerator
{
    class RandomGraphGenerator : AbstractGraphGenerator
    {
        public int Grade { get; set; }
        public RandomGraphGenerator(int grade)
        {
            Grade = grade;
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(i + 1);

                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
            }

            var randomizer = new Random();

            foreach (var node1 in nodeHolder.GetCopyList())
            {
                for (int i = 0; i < Grade; i++)
                {
                    if (node1.Neighbours.Count >= Grade)
                    {
                        break;
                    }
                    var nextNodeId = randomizer.Next(0, nodeCount);
                    while (nextNodeId == node1.Id || node1.IsNeigbourOf(nextNodeId))
                    {
                        nextNodeId = randomizer.Next(0, nodeCount);
                    }
                    
                    edgeHolder.AddEgde(new ConsoleEdge(node1, nodeHolder.GetNodeById(nextNodeId)));
                }
                //foreach (var node2 in nodeHolder.GetCopyList().Where(n => n != node1))
                //{
                //    if (randomizer.Next() % 100 > 2) continue;

                //    if (node1.IsNeigbourOf(node2.Id)) continue;
                    
                //    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                //}
            }
        }
    }
}
