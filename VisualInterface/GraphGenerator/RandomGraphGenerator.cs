using AsyncSimulator;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    class RandomGraphGenerator : IGraphGenerator
    {
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());
            var randomizer = new Random();
            for (int i = 0; i < nodeCount; i++)
            {
                var p = new Point(randomizer.Next(40, drawing_panel.Width - 40), randomizer.Next(40, drawing_panel.Height - 40));

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new NodeVisualizer(arg, p.X, p.Y, nodeHolder.NodeCount, parentForm));

                    nodeHolder.AddNode(node);
                }
                else
                {
                    i--;
                }
            };

            foreach (var node1 in nodeHolder.GetCopyList())
            {
                foreach (var node2 in nodeHolder.GetCopyList().Where(n => n != node1))
                {
                    if (randomizer.Next() % 100 > 10) continue;

                    if (node1.Neighbours.Contains(node2)) continue;
                    
                    edgeHolder.AddEgde(new VisualEdge(arg, node1, node2));
                }
            }
        }
    }
}
