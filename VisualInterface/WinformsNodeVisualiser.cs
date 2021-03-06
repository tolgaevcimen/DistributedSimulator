﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface
{
    internal class WinformsNodeVisualiser : IVisualizer
    {
        private int Id { get; set; }
        private PaintEventArgs PaintArgs { get; set; }

        private int Diameter = 40;
        private Brush WaitNodeColor = Brushes.LightSkyBlue;
        private Brush OutNodeColor = Brushes.DarkGray;
        private Brush InNodeColor = Brushes.Black;

        private Font DefaultFontForId = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);

        public PointF Location { get; set; }
        public Presenter ParentForm { get; set; }

        public bool Deleted { get; set; }

        public WinformsNodeVisualiser(PaintEventArgs e, float x, float y, int id, Presenter parentForm)
        {
            Id = id;
            
            Location = new PointF(x, y);
            
            PaintArgs = e;
            ParentForm = parentForm;

            Draw(NodeState.WAIT);
        }

        public void Delete()
        {
            Deleted = true;
            bool drawn = false;
            while (!drawn)
                try
                {
                    PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    PaintArgs.Graphics.FillEllipse(Brushes.White, (Location.X - Diameter / 2) - 1, (Location.Y - Diameter / 2) - 1, Diameter + 2, Diameter + 2);

                    drawn = true;
                }
                catch { }
        }

        ///// <summary>
        ///// Changes nodes color. Default is Red, if changeColor parameter is set to true, then it will be blue.
        ///// </summary>
        ///// <param name="changeColor"></param>
        //public void Draw(bool changeColor = false)
        //{
        //    if (Deleted) return;

        //    bool drawn = false;
        //    while (!drawn)
        //        try
        //        {
        //            PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //            PaintArgs.Graphics.FillEllipse(changeColor ? CoveredNodeColor : NodeColor, Location.X - Diameter / 2, Location.Y - Diameter / 2, Diameter, Diameter);

        //            var offSet = Id < 10 ? 8 : 0;
        //            PaintArgs.Graphics.DrawString(Id.ToString(), DefaultFontForId, Brushes.White, Location.X - Diameter / 2 + offSet, Location.Y - Diameter / 2 + 4);

        //            drawn = true;
        //        }
        //        catch { }
        //}

        /// <summary>
        /// Changes nodes color. Default is Red, if changeColor parameter is set to true, then it will be blue.
        /// </summary>
        /// <param name="changeColor"></param>
        public void Draw(NodeState nodeColor)
        {
            if (Deleted) return;

            var color = nodeColor == NodeState.WAIT ? WaitNodeColor : nodeColor == NodeState.OUT ? OutNodeColor : InNodeColor;

            bool drawn = false;
            while (!drawn)
                try
                {
                    PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    PaintArgs.Graphics.FillEllipse(color, Location.X - Diameter / 2, Location.Y - Diameter / 2, Diameter, Diameter);

                    var offSet = Id < 10 ? 8 : 0;
                    PaintArgs.Graphics.DrawString(Id.ToString(), DefaultFontForId, Brushes.White, Location.X - Diameter / 2 + offSet, Location.Y - Diameter / 2 + 4);

                    drawn = true;
                }
                catch { }
        }


        public IEdge GetEdgeTo(_Node n2)
        {
            foreach (var edge in ParentForm.EdgeHolder.GetCopyList())
            {
                if ((edge.GetNode1().Id == Id && edge.GetNode2().Id == n2.Id) ||
                    (edge.GetNode2().Id == Id && edge.GetNode1().Id == n2.Id))
                {
                    return edge;
                }
            }

            return null;
        }

        public bool Intersects(PointF p)
        {
            var dist = GetDistance(Location, p);

            return dist < Diameter;
        }

        public void Log(string l, params object[] args)
        {
            ParentForm.Invoke(new MethodInvoker(delegate () { ParentForm.tb_console.AppendText(string.Format(l, args) + Environment.NewLine); }));
        }

        public bool OnIt(PointF p)
        {
            var dist = GetDistance(Location, p);
            var radius = (double)Diameter / 2;

            return dist < radius;
        }

        public void VisualizeMessage(AsyncSimulator.Message m)
        {
            foreach (var edge in ParentForm.EdgeHolder.GetCopyList())
            {
                if ((edge.GetNode1().Id == m.Source.Id && edge.GetNode2().Id == m.DestinationId) ||
                    (edge.GetNode2().Id == m.Source.Id && edge.GetNode1().Id == m.DestinationId))
                {
                    edge.Colorify(false);

                    edge.GetNode1().Visualizer.Draw(NodeState.IN);
                    edge.GetNode2().Visualizer.Draw(NodeState.IN);
                    break;
                }
            }
        }
        
        private static double GetDistance(PointF point1, PointF point2)
        {
            //pythagorean theorem c^2 = a^2 + b^2
            //thus c = square root(a^2 + b^2)
            double a = (double)(point2.X - point1.X);
            double b = (double)(point2.Y - point1.Y);

            return Math.Sqrt(a * a + b * b);
        }        
    }
}
