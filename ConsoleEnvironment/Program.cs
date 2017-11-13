using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEnvironment
{
    class Program
    {
        public static bool BatchCompleted { get; set; }

        static void Main(string[] args)
        {
            var algorithms = Enum.GetNames(typeof(AlgorithmType)).Select(atn => (AlgorithmType)Enum.Parse(typeof(AlgorithmType), atn)).ToList();

            var graphTypes = new List<GraphType>(new[] { GraphType.Random, GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Complete, GraphType.Random });
            
            foreach (var algorithm in algorithms)
            {
                foreach (var graphType in graphTypes)
                {
                    var runner = new AlgorithmRunner(algorithm, graphType);
                    runner.StartRunning();
                }
            }
        }        
    }
}