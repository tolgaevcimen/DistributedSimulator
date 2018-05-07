using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceAnalyserLibrary.GraphGenerator
{
    class BinaryTreeTopologyGenerator : ITopologyGenerator
    {
        public Topology Generate(int nodeCount)
        {
            var topology = new Topology();

            var queue = new Queue<int>();
            for (int i = 1; i < nodeCount; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                var parent = i;

                for (int j = 0; j < 2; j++)
                {
                    if (!queue.Any()) break;

                    var child = queue.Dequeue();

                    topology.Neighbourhoods.Add(new Tuple<int, int>(parent, child));
                }
            }


            return topology;
        }
    }
}
