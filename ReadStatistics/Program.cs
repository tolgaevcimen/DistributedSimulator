using PerformanceAnalyserLibrary;
using SupportedAlgorithmAndGraphTypes;
using System.Collections.Generic;

namespace StatisticReaderLibrary
{
    class Program
    {
        static string JsonsPath = @"C:\Users\htolg\Documents\Visual Studio 2017\Projects\AsyncSimulator\AsyncSimulator\ConsoleForStatisticsEnvironment\bin\Release\random_graphs_random_states_25x150x10-10";
        static int NumberToIncreaseNodeCount = 10;
        static int NodeCountFold = 150;
        static int EachNodeCountRunCount = 25;

        static double TimeToTransmit = 0.001;
        static double TransmitEnergy = 1;
        static double TimeToReceive = 0.001;
        static double ReceiveEnergy = 1;
        static double IdleEnergy = 0.5;

        static string OutputFileName = "random_graphs_random_states_25x150x10-10.txt";

        static void Main(string[] args)
        {
            var graphTypes = new List<GraphType> { GraphType.Random3, GraphType.Random6, GraphType.Random9 };
            var algorithTypes = new List<string> { "ChiuMDS_rand", "GoddardMDS_rand", "TurauMDS_rand" };

            var statisticReader = new StatisticReader(new SimulationProperties(NumberToIncreaseNodeCount, NodeCountFold, EachNodeCountRunCount, algorithTypes, graphTypes),
                new SystemProperties(TimeToTransmit, TransmitEnergy, TimeToReceive, ReceiveEnergy, IdleEnergy), JsonsPath, OutputFileName);

            statisticReader.Process();            
        }
    }
}
