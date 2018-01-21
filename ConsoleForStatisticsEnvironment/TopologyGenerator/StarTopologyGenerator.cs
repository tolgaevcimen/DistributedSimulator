using System;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    class StarTopologyGenerator : ITopologyGenerator
    {
        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();

            for (int i = 1; i < nodeCount; i++)
            {
                topology.Neighbourhoods.Add(new Tuple<int, int>(0, i));                
            }

            return topology;
        }
    }
}
