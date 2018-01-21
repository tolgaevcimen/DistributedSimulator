using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleForStatisticsEnvironment
{
    public class AlgorithmRunner : IDisposable
    {
        public AlgorithmType AlgorithmType { get; set; }
        public GraphType GraphType { get; set; }
        public int NodeCount { get; set; }

        public EdgeHolder EdgeHolder { get; set; }
        public NodeHolder NodeHolder { get; set; }
        
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public RunReport RunReport { get; set; }
        
        public int IndexToRunForEachNodeCount = 0;
        
        public bool BatchCompleted { get; set; }

        public List<Topology> Topologies { get; set; }

        public AlgorithmRunner(List<Topology> topologies, AlgorithmType algorithmType, GraphType graphType, int nodeCount)
        {
            Topologies = topologies;
            AlgorithmType = algorithmType;
            GraphType = graphType;
            NodeCount = nodeCount;
        }

        public void StartRunning()
        {
            BatchCompleted = false;
            Task.Run(() => Run());

            while (!BatchCompleted)
            {
                Thread.Sleep(200);
            }
            Console.WriteLine("batch completed!!!!!");
        }

        private void Run()
        {
            StartTime = DateTime.Now;
            InitializeHolders();
            
            RunReport.ReportTopology(EdgeHolder.GetCopyList());
            RunReport.ReportNodes(NodeHolder.GetCopyList(), true);

            foreach (var node in NodeHolder.GetCopyList().AsParallel())
            {
                Task.Run(() =>
                {
                    node.UserDefined_SingleInitiatorProcedure();
                });
            }

            NodeHolder.StartTerminationDetection();
        }

        private void NodeHolder_Terminated(object sender, EventArgs e)
        {
            Duration = DateTime.Now.Subtract(StartTime);
            Console.Clear();
            HandleReport();
            IndexToRunForEachNodeCount++;
            if (IndexToRunForEachNodeCount >= Topologies.Count)
            {
                BatchCompleted = true;
                IndexToRunForEachNodeCount = 0;
                return;
            }
            Run();
        }

        private void HandleReport()
        {
            RunReport.ReportNodes(NodeHolder.GetCopyList(), false);
            RunReport.GatherMessageCounts(NodeHolder.GetCopyList());
            RunReport.GatherMoveCount(NodeHolder.GetCopyList());
            RunReport.ReportInvalidNodes(NodeHolder.GetCopyList());
            RunReport.ReportDegrees(NodeHolder.GetCopyList());
            RunReport.ReportCongestions(NodeHolder.GetCopyList());
            RunReport.SetDuration(Duration.TotalSeconds);
            
            using (var streamWriter = new StreamWriter("5_try_all_types_2.txt", true))
                streamWriter.WriteLine(RunReport.ToString());
            using (var streamWriter = new StreamWriter(string.Format("jsons\\{0}.{1}.{2}.{3}.json", GraphType, AlgorithmType, NodeCount, IndexToRunForEachNodeCount), true))
                streamWriter.WriteLine(RunReport.Serialize());
        }
        
        private void InitializeHolders()
        {
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();

            var graphGenerator = new TopologyGivenGraphGenerator(Topologies[IndexToRunForEachNodeCount], AlgorithmType);
            graphGenerator.GenerateGraph(NodeHolder, EdgeHolder);

            NodeHolder.Terminated += NodeHolder_Terminated;
            RunReport = new RunReport(AlgorithmType, GraphType, NodeHolder.NodeCount);
        }

        public void Dispose()
        {
            EdgeHolder = null;
            NodeHolder = null;
            RunReport = null;
        }
    }
}
