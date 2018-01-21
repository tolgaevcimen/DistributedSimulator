using AsyncSimulator;
using ConsoleForStatisticsEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadStatistics
{
    class ReportData
    {
        public ReportData(double energy, double duration, double moveCount, double dominatorCount)
        {
            Energy = energy;
            Duration = duration;
            MoveCount = moveCount;
            DominatorCount = dominatorCount;
        }
        
        public double Energy { get; set; }
        public double Duration { get; set; }
        public double MoveCount { get; set; }
        public double DominatorCount { get; set; }
    }
}
