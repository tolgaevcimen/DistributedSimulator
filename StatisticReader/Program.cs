using Newtonsoft.Json;
using PerformanceAnalyserLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StatisticReaderLibrary
{
    public class StatisticReader
    {
        private readonly SimulationProperties simulationProperties;
        private readonly SystemProperties systemProperties;
        private readonly string inputFolderPath;
        private readonly string outputFilePath;

        public StatisticReader(SimulationProperties simulationProperties, SystemProperties systemProperties, string inputFolderPath, string outoutFilePath)
        {
            this.simulationProperties = simulationProperties;
            this.systemProperties = systemProperties;
            this.inputFolderPath = inputFolderPath;
            this.outputFilePath = outoutFilePath;
        }

        public void Process()
        {
            var zeroHeader = "\t" + string.Join("\t", simulationProperties.GraphTypes.Select(gt => string.Join("\t", simulationProperties.AlgorithmTypes.Select(at => string.Format("{0} - {1}\t{0} - {1}\t{0} - {1}\t{0} - {1}", gt, at)))));
            var header = "\t" + string.Join("\t\t\t\t", simulationProperties.GraphTypes.Select(gt => string.Join("\t\t\t\t", simulationProperties.AlgorithmTypes.Select(at => string.Format("{0} - {1}", gt, at)))));
            var secondHeader = "NodeCount\t" + string.Join("\t", simulationProperties.GraphTypes.Select(gt => string.Join("\t", simulationProperties.AlgorithmTypes.Select(at => "Energy\tDuration\tMoveCount\tDominators"))));

            using (var streamWriter = new StreamWriter(outputFilePath, false))
            {
                streamWriter.WriteLine(zeroHeader);
                streamWriter.WriteLine(header);
                streamWriter.WriteLine(secondHeader);
            }

            for (int nodeCountIndex = 1; nodeCountIndex <= simulationProperties.NumberToIncreaseNodeCount; nodeCountIndex++)
            {
                var nodeCount = nodeCountIndex * simulationProperties.NodeCountFold;

                var reportLine = new List<ReportData>();

                foreach (var graphType in simulationProperties.GraphTypes)
                {
                    Console.WriteLine("Calculating for {0}-{1}", graphType, nodeCount);

                    foreach (var algorithm in simulationProperties.AlgorithmTypes)
                    {
                        var totalEnergy = 0.0;
                        var totalDuration = 0.0;
                        var totalMoveCount = 0.0;
                        var totalDominatorCount = 0.0;

                        for (int j = 0; j < simulationProperties.EachNodeCountRunCount; j++)
                        {
                            // avg
                            using (var jsonFile = new StreamReader(Path.Combine(inputFolderPath, string.Format("{0}.{1}.{2}.{3}.json", graphType, algorithm, nodeCount, j))))
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
                            totalEnergy / simulationProperties.EachNodeCountRunCount,
                            totalDuration / simulationProperties.EachNodeCountRunCount,
                            totalMoveCount / simulationProperties.EachNodeCountRunCount,
                            totalDominatorCount / simulationProperties.EachNodeCountRunCount));
                    }
                }


                // line
                var line = nodeCount.ToString();

                foreach (var data in reportLine)
                {
                    line += string.Format("\t{0}\t{1}\t{2}\t{3}", data.Energy, data.Duration, data.MoveCount, data.DominatorCount);
                }

                using (var streamWriter = new StreamWriter(outputFilePath, true))
                    streamWriter.WriteLine(line);
            }

            Console.WriteLine("Finished");
            Console.Read();
        }

        double CalculateEnergy(RunReport report)
        {
            if (report.AlgorithmType.StartsWith("GoddardMDS"))
            {
                var transmitTime = report.TotalSentCount * systemProperties.TimeToTransmit;
                var receiveTime = report.TotalReceiveCount * systemProperties.TimeToReceive;

                var transmitEnergy = transmitTime * systemProperties.TransmitEnergy * 2;
                var receiveEnergy = receiveTime * systemProperties.ReceiveEnergy * 2;
                var idleEnergy = (report.Duration - (transmitTime + receiveTime)) * systemProperties.IdleEnergy;

                return transmitEnergy + receiveEnergy + idleEnergy;
            }
            else
            {
                var transmitTime = report.TotalSentCount * systemProperties.TimeToTransmit;
                var receiveTime = report.TotalReceiveCount * systemProperties.TimeToReceive;

                var transmitEnergy = transmitTime * systemProperties.TransmitEnergy;
                var receiveEnergy = receiveTime * systemProperties.ReceiveEnergy;
                var idleEnergy = (report.Duration - (transmitTime + receiveTime)) * systemProperties.IdleEnergy;

                return transmitEnergy + receiveEnergy + idleEnergy;
            }
        }
    }
}
