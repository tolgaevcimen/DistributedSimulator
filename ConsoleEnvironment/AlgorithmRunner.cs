using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEnvironment
{
    public class AlgorithmRunner : IDisposable
    {
        public AlgorithmType AlgorithmType { get; set; }
        public GraphType GraphType { get; set; }
        public int NodeCount { get; set; }

        public EdgeHolder EdgeHolder { get; set; }
        public NodeHolder NodeHolder { get; set; }
        public RunReport RunReport { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public int NumberToRunForEachNodeCount = 5;
        public int IndexToRunForEachNodeCount = 0;

        public AlgorithmRunner(AlgorithmType algorithmType, GraphType graphType, int nodeCount)
        {
            AlgorithmType = algorithmType;
            GraphType = graphType;
            NodeCount = nodeCount;
        }

        public bool BatchCompleted { get; set; }

        public void StartRunning()
        {
            BatchCompleted = false;
            Task.Run(() => Run());
            while (!BatchCompleted)
            {
                Thread.Sleep(500);
            }
            Console.WriteLine("batch completed!!!!!");
        }

        private void Run()
        {
            StartTime = DateTime.Now;
            InitializeHolders();
            
            var graphGenerator = GraphFactory.GetGraphGenerator(GraphType);
            graphGenerator.Generate(NodeCount, NodeHolder, EdgeHolder, AlgorithmType);

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
            Console.Clear();
            Duration = DateTime.Now.Subtract(StartTime);
            HandleReport();
            IndexToRunForEachNodeCount++;
            if (IndexToRunForEachNodeCount >= NumberToRunForEachNodeCount)
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
            RunReport.GatherMessageCount(NodeHolder.GetCopyList());
            RunReport.GatherMoveCount(NodeHolder.GetCopyList());
            RunReport.ReportInvalidNodes(NodeHolder.GetCopyList());
            RunReport.ReportDegrees(NodeHolder.GetCopyList());
            RunReport.SetDuration(Duration.TotalSeconds);
            
            using (var streamWriter = new StreamWriter("test.txt", true))
                streamWriter.WriteLine(RunReport.ToString());
        }
        
        private void InitializeHolders()
        {
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();
            NodeHolder.Terminated += NodeHolder_Terminated;
            RunReport = new RunReport(AlgorithmType, GraphType, NodeCount);
        }

        public void Dispose()
        {
            EdgeHolder = null;
            NodeHolder = null;
            RunReport = null;
        }
    }
}
