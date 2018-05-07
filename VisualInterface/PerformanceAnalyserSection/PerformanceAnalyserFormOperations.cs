using PerformanceAnalyserLibrary;
using SupportedAlgorithmAndGraphTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface.PerformanceAnalyserSection
{
    public partial class PerformanceAnalyserFormOperations
    {
        public PerformanceAnalyserFormOperations(Presenter presenter)
        {
            this.presenter = presenter;
        }

        public void btn_runPerformanceAnalysis_Click(object sender, EventArgs e)
        {
            if (!FormValid())
            {
                return;
            }

            var graphTypes = new List<GraphType>();
            var algorithmTypes = new List<AlgorithmType>();

            foreach (string graphType in presenter.clb_graphTypes.CheckedItems)
            {
                graphTypes.Add((GraphType)Enum.Parse(typeof(GraphType), graphType));
            }

            foreach (string algorithmType in presenter.clb_algorithmTypes.CheckedItems)
            {
                algorithmTypes.Add((AlgorithmType)Enum.Parse(typeof(AlgorithmType), algorithmType));
            }

            var topologyCount = int.Parse(presenter.tb_topologyCount.Text);
            var numberToIncreaseNodeCount = int.Parse(presenter.tb_numberToIncreaseNodeCount.Text);
            var nodeCountFold = int.Parse(presenter.tb_nodeCountFold.Text);

            var performanceAnalyser = new PerformanceAnalyser(topologyCount, numberToIncreaseNodeCount, nodeCountFold, graphTypes, algorithmTypes);

            progressBar.Maximum = performanceAnalyser.TotalStepCount;
            performanceAnalyser.StepDone += PerformanceAnalyser_StepDone;

            OnPerformanceAnalyserStarting();
            Task.Run(() => performanceAnalyser.Run(tb_sessionName.Text));
        }

        public void btn_cancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private bool FormValid()
        {
            if (!TextboxesValid(new List<TextBox>() { tb_sessionName, tb_numberToIncreaseNodeCount, tb_nodeCountFold, tb_topologyCount }))
            {
                return false;
            }

            if (clb_algorithmTypes.CheckedItems.Count == 0 || clb_graphTypes.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one algorithm and graph type");
                return false;
            }

            return true;
        }

        private bool TextboxesValid(List<TextBox> textboxes)
        {
            foreach (var tb in textboxes)
            {
                if (tb.Text == string.Empty)
                {
                    MessageBox.Show(string.Format("Please input a {0}", tb.AccessibleName));
                    return false;
                }
            }

            return true;
        }

        private void OnPerformanceAnalyserStarting()
        {
            ToggleFields(new List<Control>() { tb_sessionName, tb_nodeCountFold, tb_numberToIncreaseNodeCount, tb_topologyCount, clb_algorithmTypes, clb_graphTypes, btn_runPerformanceAnalysis }, false);
        }

        private void PerformanceAnalyser_StepDone(object sender, EventArgs e)
        {
            var nodeCountFold = int.Parse(tb_nodeCountFold.GetPropertyThreadSafe(() => tb_nodeCountFold.Text));
            var stepDoneArgs = (StepDoneArgs)e;

            progressBar.SetPropertyThreadSafe(() => progressBar.Value, stepDoneArgs.CurrentStepNumber());
            lbl_currentTopologyType.SetPropertyThreadSafe(() => lbl_currentTopologyType.Text, stepDoneArgs.GraphType.ToString());
            lbl_currentAlgorithm.SetPropertyThreadSafe(() => lbl_currentAlgorithm.Text, stepDoneArgs.AlgorithmType.ToString());
            lbl_currentNodeCount.SetPropertyThreadSafe(() => lbl_currentNodeCount.Text, string.Format("{0} ({1}/{2})", (stepDoneArgs.NodeCount * nodeCountFold).ToString(), stepDoneArgs.NodeCountIndex, stepDoneArgs.NumberToIncreaseNodeCount));
            lbl_currentTopologyIndex.SetPropertyThreadSafe(() => lbl_currentTopologyIndex.Text, stepDoneArgs.TopologyIndex.ToString());

            if (progressBar.Value == progressBar.Maximum)
            {
                MessageBox.Show("Session finished");
                progressBar.SetPropertyThreadSafe(() => progressBar.Value, 0);
                OnPerformanceAnalyserFinished();
            }
        }

        private void OnPerformanceAnalyserFinished()
        {
            ToggleFields(new List<Control>() { tb_sessionName, tb_nodeCountFold, tb_numberToIncreaseNodeCount, tb_topologyCount, clb_algorithmTypes, clb_graphTypes, btn_runPerformanceAnalysis }, true);
        }

        private void ToggleFields(List<Control> controls, bool enabled)
        {
            foreach (var control in controls)
            {
                control.SetPropertyThreadSafe(() => control.Enabled, enabled);
            }
        }
    }
}
