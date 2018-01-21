using System;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    class CompleteTopologyGenerator : ITopologyGenerator
    {
        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();

            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = i; j < nodeCount; j++)
                {
                    if (i == j) continue;
                    
                    topology.Neighbourhoods.Add(new Tuple<int, int>(i, j));
                }
            }
            
            return topology;
        }
    }
}
