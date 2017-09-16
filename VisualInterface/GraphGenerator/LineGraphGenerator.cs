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
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var randomizer = new Random();
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            for (int i = 0; i < nodeCount; i++)
            {
                var interval = (drawing_panel.Width - 80) / (nodeCount - 1);

                var p = new Point(40 + (interval * i), drawing_panel.Height / 2);

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

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(i + 1);
                    
                    edgeHolder.AddEgde(new VisualEdge(arg, node1, node2));
                }
            }
        }
    }
}
