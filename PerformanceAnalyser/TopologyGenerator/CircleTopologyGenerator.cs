using System;

namespace PerformanceAnalyserLibrary.GraphGenerator
{
    class CircleTopologyGenerator : ITopologyGenerator
    {
        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    topology.Neighbourhoods.Add(new Tuple<int, int>(i, i + 1));
                }
            }

            return topology;
        }
    }
}
