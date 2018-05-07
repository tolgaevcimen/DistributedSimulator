using SupportedAlgorithmAndGraphTypes;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    internal class GraphFactory
    {
        public static IGraphGenerator GetGraphGenerator(GraphType graphType, Presenter parentForm, Panel drawing_panel)
        {
            switch (graphType)
            {
                case GraphType.Random3:
                    return new RandomGraphGenerator(parentForm, drawing_panel, 3);
                case GraphType.Random6:
                    return new RandomGraphGenerator(parentForm, drawing_panel, 6);
                case GraphType.Random9:
                    return new RandomGraphGenerator(parentForm, drawing_panel, 9);
                case GraphType.Line:
                    return new LineGraphGenerator(parentForm, drawing_panel);
                case GraphType.Circle:
                    return new CircleGraphGenerator(parentForm, drawing_panel);
                case GraphType.Star:
                    return new StarGraphGenerator(parentForm, drawing_panel);
                case GraphType.Bipartite:
                    return new BipartiteGraphGenerator(parentForm, drawing_panel);
                case GraphType.BinaryTree:
                    return new BinaryTreeGraphGenerator(parentForm, drawing_panel);
                //case GraphType.TripletTree:
                //    return new TripletTreeGraphGenerator(parentForm, drawing_panel);
                case GraphType.Complete:
                    return new CompleteGraphGenerator(parentForm, drawing_panel);
            }

            return null;
        }
    }
}
