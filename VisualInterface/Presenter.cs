using AsyncSimulator;
using NodeGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VisualInterface.GraphGenerator;

namespace VisualInterface
{
    internal partial class Presenter : Form
    {
        public EdgeHolder EdgeHolder { get; set; }
        public NodeHolder NodeHolder { get; set; }

        public DrawingPanelHelper DrawingPanelHelper { get; set; }

        /// <summary>
        /// Initiates the presenter form.
        /// </summary>
        public Presenter()
        {
            InitializeComponent();

            cb_choose_alg.Items.AddRange(Algorithms.ToArray());
            cb_choose_alg.SelectedIndex = 10;
            SelectedAlgorithm = "TurauMDS_rand";

            cb_graph_type.Items.AddRange(Enum.GetNames(typeof(GraphType)));
            cb_graph_type.SelectedIndex = 1;

            DrawingPanelHelper = new DrawingPanelHelper(this, drawing_panel, SelectedAlgorithm);
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();
        }

        #region mouse events for Creating Nodes and Edges
                
        /// <summary>
        /// Creates randomly positioned nodes and ties them to each other in a given percentage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_generate_nodes_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbNodeCount.Text, out int nodeCount)) return;

            var graphGenerator = GraphFactory.GetGraphGenerator((GraphType)Enum.Parse(typeof(GraphType), (string)cb_graph_type.SelectedItem), this, drawing_panel);

            graphGenerator.Generate(nodeCount, NodeHolder, EdgeHolder, SelectedAlgorithm);
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
            DrawingPanelHelper.ClearPanel();

            NodeHolder.EmptyAllNodes();
            EdgeHolder.EmptyAllEdges();

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
            var invalidNodes = NodeHolder.GetCopyList().Where(n => !n.IsValid());

            var runCountReport = String.Join("\n", NodeHolder.GetCopyList().Select(n => String.Format("node: {0}\t runCount: {1}", n.Id, n.MessageCount)));
            runCountReport += String.Format("\ntotal run count: {0}", NodeHolder.GetCopyList().Sum(n => n.MessageCount));

            if (!invalidNodes.Any())
            {
                MessageBox.Show(String.Format("{0}\nRun count report:\n{1}", "Each node is valid", runCountReport));
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
        private List<string> Algorithms = new List<string> { "Flooding", "FloodST", "UpdateBFS", "NeighDFS", "ChiuDS_allWait", "ChiuDS_allIn", "ChiuDS_rand", "GoddardMDS_allWait", "GoddardMDS_allIn", "GoddardMDS_allWait", "TurauMDS_rand", "TurauMDS_allIn", "TurauMDS_allWait" };

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
            var firstNode = NodeHolder.GetMinimumNode();
            if (firstNode == null) return;

            var initiator = NodeFactory.Create(SelectedAlgorithm, -1, null, cb_selfStab.Checked);
            firstNode.UserDefined_SingleInitiatorProcedure(firstNode);
        }
        
        public void DisableAlgorthmChange()
        {
            cb_choose_alg.Enabled = false;
        }
    }
}
