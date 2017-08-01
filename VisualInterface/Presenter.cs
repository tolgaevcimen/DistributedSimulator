using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;
using UpdateBfsNode;
using VisualInterface.GraphGenerator;
using System.Threading.Tasks;

namespace VisualInterface
{
    public partial class Presenter : Form
    {
        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        public List<VisualEdge> AllEdges
        {
            get
            {
                lock (AllEdgesLock)
                    return _AllEdges;
            }
        }

        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        public List<_Node> AllNodes
        {
            get
            {
                lock (AllNodesLock)
                    return _AllNodes;
            }
        }
        
        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        List<VisualEdge> _AllEdges { get; set; }

        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        List<_Node> _AllNodes { get; set; }

        public object AllNodesLock { get; set; }
        public object AllEdgesLock { get; set; }

        public DrawingPanelHelper DrawingPanelHelper { get; set; }

        /// <summary>
        /// Initiates the presenter form.
        /// </summary>
        public Presenter()
        {
            InitializeComponent();
            _AllNodes = new List<_Node>();
            _AllEdges = new List<VisualEdge>();
            AllEdgesLock = new object();
            AllNodesLock = new object();

            cb_choose_alg.Items.AddRange(Algorithms.ToArray());
            cb_choose_alg.SelectedIndex = 7;
            SelectedAlgorithm = "GoddardMDS";

            cb_graph_type.Items.AddRange(Enum.GetNames(typeof(GraphType)));
            cb_graph_type.SelectedIndex = 1;

            DrawingPanelHelper = new DrawingPanelHelper(this, drawing_panel, SelectedAlgorithm);
        }

        #region mouse events for Creating Nodes and Edges
        
        
        /// <summary>
        /// Creates randomly positioned nodes and ties them to each other in a given percentage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_random_nodes_Click(object sender, EventArgs e)
        {
            var nodeCount = 0;
            if (!int.TryParse(tbNodeCount.Text, out nodeCount)) return;

            var graphGenerator = GraphFactory.GetGraphGenerator((GraphType)Enum.Parse(typeof(GraphType), (string)cb_graph_type.SelectedItem));

            graphGenerator.Generate(nodeCount, this, drawing_panel, AllNodes, AllEdges, SelectedAlgorithm);
        }

        #endregion

        #region mouse events for clearing the canvas

        /// <summary>
        /// Clears the drawing panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());
            arg.Graphics.Clear(Color.White);
            
            AllNodes.Clear();
            AllEdges.Clear();

            tb_console.Clear();

            cb_choose_alg.Enabled = true;
            if (cb_choose_alg.SelectedIndex == -1) cb_choose_alg.SelectedIndex = 1;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_proof_Click(object sender, EventArgs e)
        {
            var invalidNodes = AllNodes.Where(n => !n.IsValid());

            if (!invalidNodes.Any())
            {
                MessageBox.Show("Each node is valid");
            }
            else
            {
                MessageBox.Show(string.Format("Some nodes are invalid: {0}", string.Join(",", invalidNodes.Select(n => n.Id.ToString()))));
            }
        }

        #endregion

        #region algorithms

        /// <summary>
        /// Holds the list of algorithms. When new ones are added here, please add them to the factory implementation too.
        /// </summary>
        private List<string> Algorithms = new List<string> { "Flooding", "FloodST", "UpdateBFS", "NeighDFS", "ChiuDS_allWait", "ChiuDS_allIn", "ChiuDS_rand", "GoddardMDS" };

        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        private string SelectedAlgorithm { get; set; }

        /// <summary>
        /// Event for setting the current algorithm. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_choose_alg_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAlgorithm = cb_choose_alg.SelectedItem.ToString();
        }

        #endregion

        /// <summary>
        /// Starts the simulation of the algorithm run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            var firstNode = AllNodes.FirstOrDefault(fn => fn.Id == AllNodes.Min(n => n.Id));
            if (firstNode == null) return;

            var initiator = NodeFactory.Create(SelectedAlgorithm, -1, null);
            firstNode.UserDefined_SingleInitiatorProcedure(firstNode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllEdges.FirstOrDefault().Delete();
        }
        
        private void cb_selfStab_CheckedChanged(object sender, EventArgs e)
        {
            DrawingPanelHelper.SelfStabModeEnabled = cb_selfStab.Checked;
        }

        public void DisableAlgorthmChange()
        {
            cb_choose_alg.Enabled = false;
        }
    }
}
