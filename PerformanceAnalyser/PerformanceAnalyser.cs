using PerformanceAnalyserLibrary.GraphGenerator;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using SupportedAlgorithmAndGraphTypes;

namespace PerformanceAnalyserLibrary
{
    public class PerformanceAnalyser
    {
        private readonly int TopologyCount;
        private readonly int NumberToIncreaseNodeCount;
        private readonly int NodeCountFold;
        private readonly List<GraphType> GraphTypes;
        private readonly List<AlgorithmType> AlgorithmTypes;
        private int IndexToIncreaseNodeCount;
        private GraphType CurrentGraphType;
        private AlgorithmType CurrentAlgorithmType;
        private int NodeCount;

        public int TotalStepCount { get; set; }

        public event EventHandler StepDone;

        public PerformanceAnalyser(SimulationProperties simulationProperties)
        {
            TopologyCount = simulationProperties.EachNodeCountRunCount;
            NumberToIncreaseNodeCount = simulationProperties.NumberToIncreaseNodeCount;
            NodeCountFold = simulationProperties.NodeCountFold;
            GraphTypes = simulationProperties.GraphTypes;
            AlgorithmTypes = simulationProperties.AlgorithmTypes;

            CalculateStepCount();
        }

        private void CalculateStepCount()
        {
            TotalStepCount = 
                TopologyCount *
                NumberToIncreaseNodeCount * 
                AlgorithmTypes.Count * 
                GraphTypes.Count;
        }

        public void Run(string folderName)
        {
            foreach (var graphType in GraphTypes)
            {
                CurrentGraphType = graphType;
                try
                {
                    for (IndexToIncreaseNodeCount = 1;
                                IndexToIncreaseNodeCount <= NumberToIncreaseNodeCount;
                                IndexToIncreaseNodeCount++)
                    {
                        try
                        {
                            NodeCount = IndexToIncreaseNodeCount * NodeCountFold;

                            var topologyGenerator = TopologyGeneratorFactory.GetTopologyGenerator(CurrentGraphType);
                            List<Topology> topologies = new List<Topology>();
                            for (int i = 0; i < TopologyCount; i++)
                            {
                                if (AlgorithmTypes.All(a => File.Exists(GetFileNameForCurrentRun(CurrentGraphType, a, NodeCount, i, folderName))))
                                {
                                    continue;
                                }

                                var topology = topologyGenerator.Generate(NodeCount);
                                topologies.Add(topology);
                            }

                            if (!topologies.Any())
                            {
                                continue;
                            }

                            foreach (var algorithm in AlgorithmTypes)
                            {
                                CurrentAlgorithmType = algorithm;
                                try
                                {
                                    using (var runner = new AlgorithmRunner(topologies, CurrentAlgorithmType, CurrentGraphType, NodeCount, folderName))
                                    {
                                        runner.StepDone += Runner_StepDone;
                                        runner.StartRunning();
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw new Exception(string.Format("Exception occured while processing algorithm: {0}. ExceptionMessage: {1}", CurrentAlgorithmType, e.Message));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("Exception occured while processing for nodeCount: {0}. ExceptionMessage: {1}", NodeCount, e.Message));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Exception occured while processing for graphType: {0}. ExceptionMessage: {1}", CurrentGraphType, e.Message));
                }
            }

            IndexToIncreaseNodeCount--;
            LastStepDoneArgs.TopologyIndex++;
            OnStepDone(LastStepDoneArgs);
        }

        private void Runner_StepDone(object sender, EventArgs e)
        {
            OnStepDone(e);
        }

        public StepDoneArgs LastStepDoneArgs { get; set; }

        protected void OnStepDone(EventArgs e)
        {
            LastStepDoneArgs = new StepDoneArgs()
            {
                GraphIndex = GraphTypes.IndexOf(CurrentGraphType),
                NodeCountIndex = IndexToIncreaseNodeCount,
                AlgorithmIndex = AlgorithmTypes.IndexOf(CurrentAlgorithmType),
                TopologyIndex = ((StepDoneArgs)e).TopologyIndex,

                GraphType = CurrentGraphType,
                NodeCount = NodeCount,
                AlgorithmType = CurrentAlgorithmType,

                NumberToIncreaseNodeCount = NumberToIncreaseNodeCount,
                AlgorithmCount = AlgorithmTypes.Count(),
                TopologyCount = TopologyCount
            };

            StepDone?.Invoke(this, LastStepDoneArgs);
        }

        private string GetFileNameForCurrentRun(GraphType GraphType, AlgorithmType AlgorithmType, int NodeCount, int IndexToRunForEachNodeCount, string FolderName)
        {
            return string.Format("{4}\\{0}.{1}.{2}.{3}.json", GraphType, AlgorithmType, NodeCount, IndexToRunForEachNodeCount, FolderName);
        }
    }
}