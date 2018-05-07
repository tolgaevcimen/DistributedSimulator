using System;
using System.Collections.Generic;
using SupportedAlgorithmAndGraphTypes;
using PerformanceAnalyserLibrary;

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


            PerformanceAnalyser.Run(25, NumberToIncreaseNodeCount, NodeCountFold, randomGraphTypes, randomAlgorithms, "random_graphs_random_states_25x150x10-10");

            PerformanceAnalyser.Run(25, NumberToIncreaseNodeCount, NodeCountFold, randomGraphTypes, inAlgorithms, "random_graphs_in_states_25x150x10-10");

            PerformanceAnalyser.Run(25, NumberToIncreaseNodeCount, NodeCountFold, randomGraphTypes, waitAlgorithms, "random_graphs_wait_states_25x150x10-10");

            PerformanceAnalyser.Run(1, NumberToIncreaseNodeCount, NodeCountFold, specialGraphTypes, randomAlgorithms, "special_graphs_random_algs_1x150x10-10");

            PerformanceAnalyser.Run(1, NumberToIncreaseNodeCount, NodeCountFold, specialGraphTypes, inAlgorithms, "special_graphs_in_algs_1x150x10-10");

            PerformanceAnalyser.Run(1, NumberToIncreaseNodeCount, NodeCountFold, specialGraphTypes, waitAlgorithms, "special_graphs_wait_algs_1x150x10-10");

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}