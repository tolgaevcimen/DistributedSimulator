using SupportedAlgorithmAndGraphTypes;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    internal class TopologyGeneratorFactory
    {
        public static ITopologyGenerator GetTopologyGenerator(GraphType graphType)
        {
            switch (graphType)
            {
                case GraphType.Random3:
                    return new RandomTopologyGenerator(3);
                case GraphType.Random6:
                    return new RandomTopologyGenerator(6);
                case GraphType.Random9:
                    return new RandomTopologyGenerator(9);
                case GraphType.Line:
                    return new LineTopologyGenerator();
                case GraphType.Circle:
                    return new CircleTopologyGenerator();
                case GraphType.Star:
                    return new StarTopologyGenerator();
                case GraphType.Bipartite:
                    return new BipartiteTopologyGenerator();
                case GraphType.BinaryTree:
                    return new BinaryTreeTopologyGenerator();
                case GraphType.Complete:
                    return new CompleteTopologyGenerator();
            }

            return null;
        }
    }
}
