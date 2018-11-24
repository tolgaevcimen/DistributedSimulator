using AsyncSimulator;
using NodeGenerator;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    class RandomGraphGenerator : AbstractGraphGenerator
    {
        int Grade { get; set; }
        public RandomGraphGenerator(Presenter parentForm, Panel drawing_panel, int grade) : base(parentForm, drawing_panel)
        {
            Grade = grade;
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(Drawing_panel.CreateGraphics(), new Rectangle());

            var radius = (Math.Min(Drawing_panel.Height, Drawing_panel.Width) - 80) / 2;
            var origin = new Point(Drawing_panel.Width / 2, Drawing_panel.Height / 2);

            var randomizer = new Random();

            for (int i = 0; i < nodeCount; i++)
            {
                var angle = 360 / (float)nodeCount * i;

                var p = PointOnCircle(radius, angle, origin);

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, ParentForm), ParentForm.cb_selfStab.Checked, nodeHolder, randomizer.Next(0, 3));

                    nodeHolder.AddNode(node);
                }
                else
                {
                    i--;
                }
            };
            //for (int i = 0; i < nodeCount; i++)
            //{
            //    var p = new Point(randomizer.Next(40, Drawing_panel.Width - 40), randomizer.Next(40, Drawing_panel.Height - 40));

            //    if (!nodeHolder.AnyIntersecting(p))
            //    {
            //        var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, ParentForm), ParentForm.cb_selfStab.Checked, nodeHolder);

            //        nodeHolder.AddNode(node);
            //    }
            //    else
            //    {
            //        i--;
            //    }
            //};

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(i + 1);

                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
            }

            foreach (var node1 in nodeHolder.GetCopyList())
            {
                for (int i = 0; i < Grade; i++)
                {
                    if (node1.Neighbours.Count >= Grade)
                    {
                        break;
                    }
                    var nextNodeId = randomizer.Next(0, nodeCount);
                    while (nextNodeId == node1.Id || node1.IsNeigbourOf(nextNodeId))
                    {
                        nextNodeId = randomizer.Next(0, nodeCount);
                    }

                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, nodeHolder.GetNodeById(nextNodeId)));
                }
                //foreach (var node2 in nodeHolder.GetCopyList().Where(n => n != node1))
                //{
                //    if (randomizer.Next() % 100 > 10) continue;

                //    if (node1.Neighbours.ContainsKey(node2.Id)) continue;

                //    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                //}
            }
        }

        PointF PointOnCircle(float radius, float angleInDegrees, PointF origin)
        {
            // Convert from degrees to radians via multiplication by PI/180        
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + origin.Y;

            return new PointF(x, y);
        }
    }
}
