using System;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    class BipartiteTopologyGenerator : ITopologyGenerator
    {
        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();

            for (int i = 0; i < nodeCount / 2; i++)
            {
                for (int j = nodeCount / 2; j < nodeCount; j++)
                {
                    topology.Neighbourhoods.Add(new Tuple<int, int>(i, j));
                }
            }
            
            return topology;
        }
    }
}
