using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    class RandomGraphGenerator : IGraphGenerator
    {
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, List<_Node> AllNodes, List<VisualEdge> AllEdges, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());
            var randomizer = new Random();
            for (int i = 0; i < nodeCount; i++)
            {
                var p = new Point(randomizer.Next(40, drawing_panel.Width - 40), randomizer.Next(40, drawing_panel.Height - 40));

                if (!AllNodes.Any(n => n.Visualizer.Intersects(p)))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, AllNodes.Count, new NodeVisualizer(arg, p.X, p.Y, AllNodes.Count, parentForm));

                    AllNodes.Add(node);
                }
                else
                {
                    i--;
                }
            };

            foreach (var node1 in AllNodes)
            {
                foreach (var node2 in AllNodes.Where(n => n != node1))
                {
                    if (randomizer.Next() % 100 > 10) continue;

                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    AllEdges.Add(edge);
                }
            }
        }
    }
}
