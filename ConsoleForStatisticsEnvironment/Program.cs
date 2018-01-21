using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;

namespace ConsoleForStatisticsEnvironment
{
    class Program
    {
        static int NumberToIncreaseNodeCount = 40;
        static int NodeCountFold = 20;

        static void Main(string[] args)
        {
            var graphTypes = new List<GraphType>(new[] { GraphType.Random3, GraphType.Random6, GraphType.Random9, GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Complete });

            foreach (var graphType in graphTypes)
            {
                try
                {
                    for (int indexToIncreaseNodeCount = 1;
                                indexToIncreaseNodeCount <= NumberToIncreaseNodeCount;
                                indexToIncreaseNodeCount++)
                    {
                        try
                        {
                            var nodeCount = indexToIncreaseNodeCount * NodeCountFold;

                            var topologyGenerator = TopologyGeneratorFactory.GetTopologyGenerator(graphType);
                            List<Topology> topologies = new List<Topology>();
                            for (int i = 0; i < 5; i++)
                            {
                                var topology = topologyGenerator.Generate(nodeCount);
                                topologies.Add(topology);
                            }

                            foreach (var algorithm in new List<AlgorithmType> { AlgorithmType.ChiuMDS_allWait, AlgorithmType.GoddardMDS_allWait, AlgorithmType.TurauMDS_allWait })
                            {
                                try
                                {
                                    using (var runner = new AlgorithmRunner(topologies, algorithm, graphType, nodeCount))
                                        runner.StartRunning();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception occured while processing algorithm: {0} for nodeCount: {1} for graphType: {2}", algorithm, indexToIncreaseNodeCount, graphType);
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception occured while processing for nodeCount: {0} for graphType: {1}", indexToIncreaseNodeCount, graphType);
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured while processing for graphType: {0}", graphType);
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}