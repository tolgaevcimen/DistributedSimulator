using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ConsoleForStatisticsEnvironment
{
    class Program
    {
        static int NumberToIncreaseNodeCount = 10;
        static int NodeCountFold = 150;

        static void Main(string[] args)
        {
            var randomAlgorithms = new List<AlgorithmType> { AlgorithmType.ChiuMDS_rand, AlgorithmType.GoddardMDS_rand, AlgorithmType.TurauMDS_rand };
            var inAlgorithms = new List<AlgorithmType> { AlgorithmType.ChiuMDS_allIn, AlgorithmType.GoddardMDS_allIn, AlgorithmType.TurauMDS_allIn};
            var waitAlgorithms = new List<AlgorithmType> { AlgorithmType.ChiuMDS_allWait, AlgorithmType.GoddardMDS_allWait, AlgorithmType.TurauMDS_allWait };

            var randomGraphTypes = new List<GraphType>(new[] { GraphType.Random3, GraphType.Random6, GraphType.Random9 });
            var specialGraphTypes = new List<GraphType>(new[] { GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Bipartite, GraphType.Complete });


            RunNTimesForGraphTypes(25, randomGraphTypes, randomAlgorithms, "random_graphs_random_states_25x150x10-10");

            RunNTimesForGraphTypes(25, randomGraphTypes, inAlgorithms, "random_graphs_in_states_25x150x10-10");

            RunNTimesForGraphTypes(25, randomGraphTypes, waitAlgorithms, "random_graphs_wait_states_25x150x10-10");

            RunNTimesForGraphTypes(1, specialGraphTypes, randomAlgorithms, "special_graphs_random_algs_1x150x10-10");

            RunNTimesForGraphTypes(1, specialGraphTypes, inAlgorithms, "special_graphs_in_algs_1x150x10-10");

            RunNTimesForGraphTypes(1, specialGraphTypes, waitAlgorithms, "special_graphs_wait_algs_1x150x10-10");

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static void RunNTimesForGraphTypes(int n, List<GraphType> graphTypes, List<AlgorithmType> algorithmTypes, string folderName)
        {
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
                            for (int i = 0; i < n; i++)
                            {
                                if (algorithmTypes.All(a => File.Exists("..\\Release\\" + GetFileNameForCurrentRun(graphType, a, nodeCount, i, folderName))))
                                {
                                    continue;
                                }

                                var topology = topologyGenerator.Generate(nodeCount);
                                topologies.Add(topology);
                            }

                            if (!topologies.Any())
                            {
                                continue;
                            }

                            foreach (var algorithm in algorithmTypes)
                            {
                                try
                                {
                                    using (var runner = new AlgorithmRunner(topologies, algorithm, graphType, nodeCount, folderName))
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
        }


        private static string GetFileNameForCurrentRun(GraphType GraphType, AlgorithmType AlgorithmType, int NodeCount, int IndexToRunForEachNodeCount, string FolderName)
        {
            return string.Format("{4}\\{0}.{1}.{2}.{3}.json", GraphType, AlgorithmType, NodeCount, IndexToRunForEachNodeCount, FolderName);
        }

    }
}