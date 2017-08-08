using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    internal class DrawingPanelHelper
    {
        Presenter Presenter { get; set; }
        Panel DrawingPanel { get; set; }

        /// <summary>
        /// Previous location that mouse was clicked.
        /// </summary>
        Point MouseStartPos { get; set; }
        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        string SelectedAlgorithm { get; set; }
        /// <summary>
        /// Holds the edge that just has been started to be drawn.
        /// </summary>
        VisualEdge EdgeBeingDrawn { get; set; }

        public bool SelfStabModeEnabled
        {
            get
            {
                return Presenter.cb_selfStab.Checked;
            }
        }

        public DrawingPanelHelper(Presenter presenterForm, Panel drawingPanel, string selectedAlgorithm)
        {
            Presenter = presenterForm;
            DrawingPanel = drawingPanel;
            SelectedAlgorithm = selectedAlgorithm;

            BindEvents();
        }

        public void ClearPanel()
        {
            GetNewPaintEventArgs().Graphics.Clear(Color.White);
        }

        void BindEvents()
        {
            DrawingPanel.MouseDown += DrawingPanel_MouseDown;
            DrawingPanel.MouseMove += DrawingPanel_MouseMove;
            DrawingPanel.MouseUp += DrawingPanel_MouseUp;
            DrawingPanel.Resize += DrawingPanel_Resize;
        }

        void DrawingPanel_Resize(object sender, EventArgs e)
        {
            //AllEdges.ForEach(ed => ed.Restore());
            Presenter.EdgeHolder.RedrawAllEdges();
            Presenter.NodeHolder.RedrawAllNodes();
        }

        /// <summary>
        /// Mouse down event. Tries to decide whether the mouse was clicked on a node or not, and behaves accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            MouseStartPos = GetCurrentMousePosition(e);
            var startingNode = GetNodeAt(e);

            if (startingNode != null)
                EdgeBeingDrawn = new VisualEdge(GetNewPaintEventArgs(), MouseStartPos, MouseStartPos, startingNode);
        }

        /// <summary>
        /// Gives the feel of creating an edge interactively.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (EdgeBeingDrawn == null) return;

            var newPos = GetCurrentMousePosition(e);
            EdgeBeingDrawn.Refresh(GetNewPaintEventArgs(), newPos);
        }

        /// <summary>
        /// Mouse up event. Checks if a node should be drawn or an edge (or nothing).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HandleNodeOrEdgeCreation(e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                HandleNodeRemoval(e);
                HandleEdgeRemoval(e);
            }
        }

        void HandleNodeOrEdgeCreation(MouseEventArgs e)
        {
            var pea = GetNewPaintEventArgs();
            var currentMousePosition = GetCurrentMousePosition(e);

            if (MouseStartPos == currentMousePosition)
            {
                if (!Presenter.NodeHolder.AnyIntersecting(e.Location))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, Presenter.NodeHolder.NodeCount, GetNewNodeVisualizer(e, pea));
                    node.Visualizer.Draw(node.Selected());
                    Presenter.NodeHolder.AddNode(node);

                    Presenter.DisableAlgorthmChange();
                }
            }
            else if (EdgeBeingDrawn != null)
            {
                var endingNode = Presenter.NodeHolder.GetNodeAt(e.Location);
                if (endingNode != null && endingNode != EdgeBeingDrawn.Node1)
                {
                    EdgeBeingDrawn.Solidify(DrawingPanel.PointToClient(MouseStartPos), DrawingPanel.PointToClient(currentMousePosition), endingNode, true);
                    Presenter.EdgeHolder.AddEgde(EdgeBeingDrawn);
                }
                else
                {
                    EdgeBeingDrawn.Vanish(pea);
                }
            }

            EdgeBeingDrawn = null;
        }

        void HandleNodeRemoval(MouseEventArgs e)
        {
            var clickedNode = GetNodeAt(e);
            if (clickedNode == null) return;

            var edgesToBeRemoved = Presenter.EdgeHolder.GetRelatedEdges(clickedNode, out List<_Node> nodesToBePoked);

            // delete each edge one by one
            edgesToBeRemoved.ForEach(edge => edge.Delete(false));

            clickedNode.Visualizer.Delete();
            Presenter.EdgeHolder.RedrawAllEdges();

            if (SelfStabModeEnabled)
            {
                foreach (var node in nodesToBePoked)
                {
                    Task.Run(() =>
                    {
                        node.UserDefined_SingleInitiatorProcedure(node);
                    });
                }
            }

            Presenter.NodeHolder.RemoveNode(clickedNode);
            Presenter.NodeHolder.RedrawAllNodes();
        }

        void HandleEdgeRemoval(MouseEventArgs e)
        {
            var clickedEdge = Presenter.EdgeHolder.FindEdge(e.Location);
            if (clickedEdge != null)
            {
                clickedEdge.Delete(true);
            }
        }

        NodeVisualizer GetNewNodeVisualizer(MouseEventArgs e, PaintEventArgs pea)
        {
            return new NodeVisualizer(pea, e.X, e.Y, Presenter.NodeHolder.NodeCount, Presenter);
        }

        PaintEventArgs GetNewPaintEventArgs()
        {
            return new PaintEventArgs(DrawingPanel.CreateGraphics(), new Rectangle());
        }

        Point GetCurrentMousePosition(MouseEventArgs e)
        {
            return DrawingPanel.PointToScreen(e.Location);
        }

        _Node GetNodeAt(MouseEventArgs e)
        {
            return Presenter.NodeHolder.GetNodeAt(e.Location);
        }
    }
}
