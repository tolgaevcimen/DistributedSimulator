using AsyncSimulator;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    public class VisualEdge
    {
        public PointF End1 { get; set; }
        public PointF End2 { get; set; }

        public Color EdgeColor { get; set; }
        public Color SelectedEdgeColor { get; set; }
        public Color RevertedEdgeColor { get; set; }

        public int Thickness { get; set; }

        public _Node Node1 { get; set; }
        public _Node Node2 { get; set; }

        public GraphicsPath Path { get; set; }
        public Pen SolidPen { get; set; }

        public Pen LastUsedPen { get; set; }

        private PaintEventArgs PaintArgs { get; set; }

        public VisualEdge(PaintEventArgs e, PointF end1, PointF end2, _Node node1, bool ghost = false, int thickness = 7)
        {
            EdgeColor = Color.Pink;
            SelectedEdgeColor = Color.LightBlue;
            RevertedEdgeColor = Color.Black;

            Thickness = thickness;

            SolidPen = new Pen(EdgeColor, Thickness);
            LastUsedPen = SolidPen;

            End1 = end1;
            End2 = end2;

            Node1 = node1;
            PaintArgs = e;

            if (!ghost)
                ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);
        }

        public void Refresh(PaintEventArgs e, Point end2)
        {
            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);

            End2 = end2;

            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);
        }

        public void Vanish(PaintEventArgs e)
        {
            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);
        }

        public void Solidify(PointF end1, PointF end2, _Node node2, bool firstTime)
        {
            Node2 = node2;

            LastUsedPen = SolidPen;
            Draw(SolidPen);

            Path = new GraphicsPath();

            Path.AddLine(Node1.Visualizer.Location, Node2.Visualizer.Location);

            /// set neighbourhood
            Node1.Neighbours.Add(Node2);
            Node2.Neighbours.Add(Node1);

            /// redraw the nodes
            Node1.Visualizer.Draw(Node1.Selected());
            Node2.Visualizer.Draw(Node2.Selected());

            if (Program.Presenter.cb_selfStab.Checked)
            {
                Node1.UserDefined_SingleInitiatorProcedure(Node1);
                Node2.UserDefined_SingleInitiatorProcedure(Node2);
            }
        }

        public void Colorify(PaintEventArgs e, bool reverted = false)
        {
            if (!reverted)
                LastUsedPen = new Pen(SelectedEdgeColor, Thickness);
            else
                LastUsedPen = new Pen(RevertedEdgeColor, Thickness);

            Draw(null);
        }

        public void Delete(bool onlyEdgeDeleted)
        {
            Draw(new Pen(Color.White, Thickness + 1));

            // set neighbourhood
            Node1.Neighbours.Remove(Node2);
            Node2.Neighbours.Remove(Node1);

            // redraw the nodes
            Node1.Visualizer.Draw(Node1.Selected());
            Node2.Visualizer.Draw(Node2.Selected());

            Program.Presenter.EdgeHolder.RemoveEdge(this);
            Program.Presenter.EdgeHolder.RedrawAllEdges();
            Program.Presenter.NodeHolder.RedrawAllNodes();

            if (Program.Presenter.cb_selfStab.Checked && onlyEdgeDeleted)
            {
                Task.Run(() => Node1.UserDefined_SingleInitiatorProcedure(Node1));
                Task.Run(() => Node2.UserDefined_SingleInitiatorProcedure(Node2));
            }
        }

        public void Draw(Pen pen)
        {
            if (pen == null)
                pen = LastUsedPen;

            bool drawn = false;
            while (!drawn)
                try
                {
                    /// solidify the edge
                    PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    PaintArgs.Graphics.DrawLine(pen, Node1.Visualizer.Location, Node2.Visualizer.Location);

                    drawn = true;
                }
                catch { }
        }

        public void Restore()
        {
            /// solidify the edge
            PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            PaintArgs.Graphics.DrawLine(new Pen(EdgeColor, Thickness), Node1.Visualizer.Location, Node2.Visualizer.Location);
        }
    }
}
