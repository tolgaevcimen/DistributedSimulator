using SupportedAlgorithmAndGraphTypes;
using System.Collections.Generic;

namespace PerformanceAnalyserLibrary
{
    public class SimulationProperties
    {
        public int NumberToIncreaseNodeCount { get; }
        public int NodeCountFold { get; }
        public int EachNodeCountRunCount { get; }
        public List<string> AlgorithmTypes { get; }
        public List<GraphType> GraphTypes { get; }

        public SimulationProperties(int numberToIncreaseNodeCount, int nodeCountFold, int eachNodeCountRunCount, List<string> algorithmTypes, List<GraphType> graphTypes)
        {
            NumberToIncreaseNodeCount = numberToIncreaseNodeCount;
            NodeCountFold = nodeCountFold;
            EachNodeCountRunCount = eachNodeCountRunCount;
            AlgorithmTypes = algorithmTypes;
            GraphTypes = graphTypes;
        }
    }
}
