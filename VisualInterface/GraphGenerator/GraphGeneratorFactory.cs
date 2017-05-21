namespace VisualInterface.GraphGenerator
{
    public class GraphFactory
    {
        public static IGraphGenerator GetGraphGenerator(GraphType graphType)
        {
            switch (graphType)
            {
                case GraphType.Random:
                    return new RandomGraphGenerator();
                case GraphType.Line:
                    return new LineGraphGenerator();
                case GraphType.Circle:
                    return new CircleGraphGenerator();
                case GraphType.Star:
                    return new StarGraphGenerator();
                case GraphType.Bipartite:
                    return new BipartiteGraphGenerator();
                case GraphType.Complete:
                    return new CompleteGraphGenerator();
            }

            return null;
        }
    }
}
