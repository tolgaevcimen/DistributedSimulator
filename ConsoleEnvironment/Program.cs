using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEnvironment
{
    class Program
    {
        static int NumberToIncreaseNodeCount = 25;
        static int NodeCountFold = 10;

        static void Main(string[] args)
        {
            var algorithms = Enum.GetNames(typeof(AlgorithmType)).Select(atn => (AlgorithmType)Enum.Parse(typeof(AlgorithmType), atn)).ToList();

            var graphTypes = new List<GraphType>(new[] { GraphType.Random, GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Complete, GraphType.Random });

            foreach (var algorithm in algorithms)
            {
                foreach (var graphType in graphTypes)
                {
                    for (int indexToIncreaseNodeCount = 1;
                        indexToIncreaseNodeCount < NumberToIncreaseNodeCount;
                        indexToIncreaseNodeCount++)
                    {
                        var nodeCount = indexToIncreaseNodeCount * NodeCountFold;
                        using (var runner = new AlgorithmRunner(algorithm, graphType, nodeCount))
                            runner.StartRunning();
                    }
                }
            }
        }
    }
}