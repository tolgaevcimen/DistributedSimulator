using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadStatistics
{
    class RunReport
    {
        public AlgorithmType AlgorithmType { get; set; }
        public GraphType GraphType { get; set; }
        public int NodeCount { get; set; }

        public List<Tuple<int, int>> Topology { get; set; }

        public List<int> BeforeInNodes { get; set; }
        public List<int> BeforeNInNodes { get; set; }
        public List<int> AfterInNodes { get; set; }
        public List<int> AfterNInNodes { get; set; }
        public List<int> InvalidNodes { get; set; }

        public Dictionary<int, int> EachNodesReceiveCount { get; set; }
        public int TotalReceiveCount { get; set; }
        public Dictionary<int, int> EachNodesSentCount { get; set; }
        public int TotalSentCount { get; set; }
        public Dictionary<int, int> EachNodesMoveCount { get; set; }
        public int TotalMoveCount { get; set; }

        public int MaxDegree { get; set; }
        public double AvgDegree { get; set; }
        public int MinDegree { get; set; }

        public double Duration { get; set; }

        public Dictionary<int, int> EachNodesCongestionCount { get; set; }
        public int TotalCongestionCount { get; set; }

    }
}
