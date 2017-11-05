using AsyncSimulator;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    public class WinformsEdge : AbstractEdge 
    {
        Color EdgeColor { get; set; }
        Color SelectedEdgeColor { get; set; }
        Color RevertedEdgeColor { get; set; }

        int Thickness = 7;
        GraphicsPath Path { get; set; }
        Pen SolidPen { get; set; }

        Pen LastUsedPen { get; set; }

        PaintEventArgs PaintArgs { get; set; }

        public WinformsEdge(PaintEventArgs e, _Node node1, _Node node2) : base(node1, node2)
        {
            EdgeColor = Color.Pink;
            SelectedEdgeColor = Color.LightBlue;
            RevertedEdgeColor = Color.Black;
            
            SolidPen = new Pen(EdgeColor, Thickness);
            LastUsedPen = SolidPen;
            
            PaintArgs = e;

            HandleVisualization();
            HandleSelfStabilization();
        }

        protected override void HandleVisualization()
        {
            _Draw(SolidPen);

            Path = new GraphicsPath();
            Path.AddLine(Node1.Visualizer.Location, Node2.Visualizer.Location);

            /// redraw the nodes
            Node1.Visualizer.Draw(Node1.Selected());
            Node2.Visualizer.Draw(Node2.Selected());
        }
        
        void HandleSelfStabilization()
        {
            if (Program.Presenter.cb_selfStab.Checked)
            {
                Node1.UserDefined_SingleInitiatorProcedure(Node1);
                Node2.UserDefined_SingleInitiatorProcedure(Node2);
            }
        }

        public override void Colorify(bool reverted)
        {
            if (!reverted)
                LastUsedPen = new Pen(SelectedEdgeColor, Thickness);
            else
                LastUsedPen = new Pen(RevertedEdgeColor, Thickness);

            _Draw(null);
        }

        public override void Delete(bool onlyEdgeDeleted)
        {
            _Draw(new Pen(Color.White, Thickness + 1));

            base.Delete(onlyEdgeDeleted);

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

        public override void Draw()
        {
            _Draw(null);
        }

        void _Draw(Pen pen)
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

        public override bool OnPoint(Point location)
        {
            return Path.IsOutlineVisible(location, SolidPen);
        }
    }
}
