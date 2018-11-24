using SupportedAlgorithmAndGraphTypes;
using System;

namespace PerformanceAnalyserLibrary
{
    public class StepDoneArgs : EventArgs
    {
        public int GraphIndex { get; set; }
        public int NodeCountIndex { get; set; }
        public int AlgorithmIndex { get; set; }
        public int TopologyIndex { get; set; }
        
        public int NumberToIncreaseNodeCount { get; set; }
        public int AlgorithmCount { get; set; }
        public int TopologyCount { get; set; }

        public int NodeCount { get; set; }
        public GraphType GraphType { get; set; }
        public string AlgorithmType { get; set; }

        public int CurrentStepNumber()
        {
            return (GraphIndex * (NumberToIncreaseNodeCount * AlgorithmCount * TopologyCount)) +
                ((NodeCountIndex - 1) * (AlgorithmCount * TopologyCount)) +
                (AlgorithmIndex * (TopologyCount)) +
                (TopologyIndex);
        }
    }
}
