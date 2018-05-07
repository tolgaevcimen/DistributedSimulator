using AsyncSimulator;

namespace PerformanceAnalyserLibrary.GraphGenerator
{
    internal interface ITopologyGenerator
    {
        Topology Generate(int nodeCount);
    }
}
