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

        public int NumberToRunForEachNodeCount = 5;
        public int IndexToRunForEachNodeCount = 0;
        public int NumberToIncreaseNodeCount = 25;//-->wrong
        public int IndexToIncreaseNodeCount = 1;
        public int NodeCountFold = 10;

        public AlgorithmRunner(AlgorithmType algorithmType, GraphType graphType)
        {
            AlgorithmType = algorithmType;
            GraphType = graphType;
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
            Console.ReadKey();
        }

        private void Run()
        {
            Console.WriteLine("Running for {0}-{1}", IndexToIncreaseNodeCount, IndexToIncreaseNodeCount);
            InitializeHolders();

            NodeCount = IndexToIncreaseNodeCount * NodeCountFold;

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
            
            using (var streamWriter = new StreamWriter("test.txt", true))
                streamWriter.WriteLine(RunReport.ToString());
        }
        
        private void InitializeHolders()
        {
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();
            NodeHolder.Terminated += NodeHolder_Terminated;
            RunReport = new RunReport(AlgorithmType, GraphType);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
