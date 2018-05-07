using AsyncSimulator;
using PerformanceAnalyserLibrary.GraphGenerator;
using SupportedAlgorithmAndGraphTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerformanceAnalyserLibrary
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
        public string FolderName { get; set; }

        public event EventHandler StepDone;
        protected void OnStepDone()
        {
            Task.Run(() =>
            {
                var stepDoneArgs = new StepDoneArgs()
                {
                    TopologyIndex = IndexToRunForEachNodeCount
                };

                StepDone?.Invoke(this, stepDoneArgs);
            });
        }

        public AlgorithmRunner(List<Topology> topologies, AlgorithmType algorithmType, GraphType graphType, int nodeCount, string folderName)
        {
            Topologies = topologies;
            AlgorithmType = algorithmType;
            GraphType = graphType;
            NodeCount = nodeCount;
            FolderName = folderName;
        }

        public void StartRunning()
        {
            BatchCompleted = false;
            Task.Run(() => Run());

            while (!BatchCompleted)
            {
                Thread.Sleep(200);
            }
            //Console.WriteLine("batch completed!!!!!");
        }

        private void Run()
        {
            OnStepDone();

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
            //Console.Clear();
            //Console.WriteLine(GetFileNameForCurrentRun());
            HandleReport();
            if (PrepareNextRun())
            {
                Run();
            }
        }

        private bool PrepareNextRun()
        {
            IndexToRunForEachNodeCount++;
            if (IndexToRunForEachNodeCount >= Topologies.Count)
            {
                BatchCompleted = true;
                IndexToRunForEachNodeCount = 0;
                return false;
            }

            return true;
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

            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }

            using (var streamWriter = new StreamWriter(FolderName + ".txt", true))
                streamWriter.WriteLine(RunReport.ToString());
            using (var streamWriter = new StreamWriter(GetFileNameForCurrentRun(), false))
                streamWriter.WriteLine(RunReport.Serialize());
        }

        private string GetFileNameForCurrentRun()
        {
            return string.Format("{4}\\{0}.{1}.{2}.{3}.json", GraphType, AlgorithmType, NodeCount, IndexToRunForEachNodeCount, FolderName);
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
