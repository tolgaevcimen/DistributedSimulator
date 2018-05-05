using System;
using System.Collections.Generic;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    class RandomTopologyGenerator : ITopologyGenerator
    {
        public int Grade { get; set; }
        public RandomTopologyGenerator(int grade)
        {
            Grade = grade;
        }

        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();
            topology.States = new Dictionary<int, int>();

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    topology.Neighbourhoods.Add(new Tuple<int, int>(i, i + 1));
                }
            }

            var randomizer = new Random();
            
            for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
            {
                if (topology.States != null)
                {
                    topology.States[nodeIndex] = randomizer.Next(0, 2);
                }

                for (int i = 0; i < Grade; i++)
                {
                    if (topology.DegreeOf(nodeIndex) >= Grade) break;
                    
                    var nextNodeId = randomizer.Next(0, nodeCount);
                    while (nextNodeId == nodeIndex || topology.AreNeighbours(nodeIndex, nextNodeId))
                    {
                        nextNodeId = randomizer.Next(0, nodeCount);
                    }

                    topology.Neighbourhoods.Add(new Tuple<int, int>(nodeIndex, nextNodeId));
                }
            }

            return topology;
        }
    }
}
