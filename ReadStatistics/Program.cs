using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadStatistics
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
            //var graphTypes = new List<GraphType> { GraphType.Random3, GraphType.Random6, GraphType.Random9, GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Bipartite, GraphType.Complete };
            var graphTypes = new List<GraphType> { GraphType.Random3, GraphType.Random6, GraphType.Random9 };
            //var algorithTypes = new List<AlgorithmType> { AlgorithmType.ChiuMDS_allWait, AlgorithmType.GoddardMDS_allWait, AlgorithmType.TurauMDS_allWait };
            //var algorithTypes = new List<AlgorithmType> { AlgorithmType.ChiuMDS_allIn, AlgorithmType.GoddardMDS_allIn, AlgorithmType.TurauMDS_allIn };
            var algorithTypes = new List<AlgorithmType> { AlgorithmType.ChiuMDS_rand, AlgorithmType.GoddardMDS_rand, AlgorithmType.TurauMDS_rand };

            var zeroHeader = "\t" + string.Join("\t", graphTypes.Select(gt => string.Join("\t", algorithTypes.Select(at => string.Format("{0} - {1}\t{0} - {1}\t{0} - {1}\t{0} - {1}", gt, at)))));
            var header = "\t" + string.Join("\t\t\t\t", graphTypes.Select(gt => string.Join("\t\t\t\t", algorithTypes.Select(at => string.Format("{0} - {1}", gt, at)))));
            var secondHeader = "NodeCount\t" + string.Join("\t", graphTypes.Select(gt => string.Join("\t", algorithTypes.Select(at => "Energy\tDuration\tMoveCount\tDominators"))));

            using (var streamWriter = new StreamWriter(OutputFileName, false))
            {
                streamWriter.WriteLine(zeroHeader);
                streamWriter.WriteLine(header);
                streamWriter.WriteLine(secondHeader);
            }

            for (int nodeCountIndex = 1; nodeCountIndex <= NumberToIncreaseNodeCount; nodeCountIndex++)
            {
                var nodeCount = nodeCountIndex * NodeCountFold;

                var reportLine = new List<ReportData>();

                foreach (var graphType in graphTypes)
                {
                    Console.WriteLine("Calculating for {0}-{1}", graphType, nodeCount);

                    foreach (var algorithm in algorithTypes)
                    {
                        var totalEnergy = 0.0;
                        var totalDuration = 0.0;
                        var totalMoveCount = 0.0;
                        var totalDominatorCount = 0.0;

                        for (int j = 0; j < EachNodeCountRunCount; j++)
                        {
                            // avg
                            using (var jsonFile = new StreamReader(Path.Combine(JsonsPath, string.Format("{0}.{1}.{2}.{3}.json", graphType, algorithm, nodeCount, j))))
                            {
                                var jsonString = jsonFile.ReadToEnd();

                                var runReport = JsonConvert.DeserializeObject<RunReport>(jsonString);
                                totalEnergy += CalculateEnergy(runReport);
                                totalDuration += runReport.Duration;
                                totalMoveCount += runReport.TotalMoveCount;
                                totalDominatorCount += runReport.AfterInNodes.Count;
                            }
                        }

                        // alg columns
                        reportLine.Add(new ReportData(
                            totalEnergy / EachNodeCountRunCount,
                            totalDuration / EachNodeCountRunCount,
                            totalMoveCount / EachNodeCountRunCount,
                            totalDominatorCount / EachNodeCountRunCount));
                    }
                }


                // line
                var line = nodeCount.ToString();

                foreach (var data in reportLine)
                {
                    line += string.Format("\t{0}\t{1}\t{2}\t{3}", data.Energy, data.Duration, data.MoveCount, data.DominatorCount);
                }

                using (var streamWriter = new StreamWriter(OutputFileName, true))
                    streamWriter.WriteLine(line);
            }

            Console.WriteLine("Finished");
            Console.Read();
        }

        static double CalculateEnergy(RunReport report)
        {
            if (report.AlgorithmType == AlgorithmType.GoddardMDS_rand)
            {
                var transmitTime = report.TotalSentCount * TimeToTransmit;
                var receiveTime = report.TotalReceiveCount * TimeToReceive;

                var transmitEnergy = transmitTime * TransmitEnergy * 2;
                var receiveEnergy = receiveTime * ReceiveEnergy * 2;
                var idleEnergy = (report.Duration - (transmitTime + receiveTime)) * IdleEnergy;

                return transmitEnergy + receiveEnergy + idleEnergy;
            }
            else
            {
                var transmitTime = report.TotalSentCount * TimeToTransmit;
                var receiveTime = report.TotalReceiveCount * TimeToReceive;

                var transmitEnergy = transmitTime * TransmitEnergy;
                var receiveEnergy = receiveTime * ReceiveEnergy;
                var idleEnergy = (report.Duration - (transmitTime + receiveTime)) * IdleEnergy;

                return transmitEnergy + receiveEnergy + idleEnergy;
            }
        }
    }
}
