using AsyncSimulator;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    internal interface ITopologyGenerator
    {
        Topology Generate(int nodeCount);
    }
}
