﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    class BipartiteGraphGenerator : IGraphGenerator
    {
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var randomizer = new Random();
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            var horizontalInterval = (drawing_panel.Width - 80) / 3;
            var verticalInterval = (drawing_panel.Height - 80) / ((nodeCount / 2) - (nodeCount % 2 == 0 ? 1 : 0));

            for (int i = 0; i < nodeCount; i++)
            {
                var x = i < nodeCount / 2 ?
                    40 + horizontalInterval :
                    40 + (horizontalInterval * 2);

                var y = 40 + (verticalInterval * (i < (nodeCount / 2) ? 
                    i : 
                    i - (nodeCount / 2)));

                var p = new Point(x, y);

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

            for (int i = 0; i < nodeCount / 2; i++)
            {
                for (int j = nodeCount / 2; j < nodeCount; j++)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(j);

                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    edgeHolder.AddEgde(edge);
                }
            }
        }
    }
}
