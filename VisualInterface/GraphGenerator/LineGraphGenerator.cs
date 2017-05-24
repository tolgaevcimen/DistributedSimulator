using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    class LineGraphGenerator : IGraphGenerator
    {
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, List<_Node> AllNodes, List<VisualEdge> AllEdges, string SelectedAlgorithm)
        {
            var randomizer = new Random();
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            for (int i = 0; i < nodeCount; i++)
            {
                var interval = (drawing_panel.Width - 80) / (nodeCount - 1);

                var p = new Point(40 + (interval * i), drawing_panel.Height / 2);

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

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 = AllNodes[i];
                    var node2 = AllNodes[i + 1];
                    
                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    AllEdges.Add(edge);
                }
            }
        }
    }
}
