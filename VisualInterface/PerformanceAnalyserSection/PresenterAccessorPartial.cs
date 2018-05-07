﻿using System.Windows.Forms;

namespace VisualInterface.PerformanceAnalyserSection
{
    public partial class PerformanceAnalyserFormOperations
    {
        private readonly Presenter presenter;

        private ProgressBar progressBar;
        private TextBox tb_sessionName;
        private TextBox tb_numberToIncreaseNodeCount;
        private TextBox tb_nodeCountFold;
        private TextBox tb_topologyCount;
        private CheckedListBox clb_algorithmTypes;
        private CheckedListBox clb_graphTypes;
        private Label lbl_currentTopologyIndex;
        private Label lbl_currentAlgorithm;
        private Label lbl_currentNodeCount;
        private Label lbl_currentTopologyType;
        private Button btn_runPerformanceAnalysis;

        private void InitializeFormAccessors()
        {
            progressBar = presenter.progressBar;
            tb_sessionName = presenter.tb_sessionName;
            tb_numberToIncreaseNodeCount = presenter.tb_numberToIncreaseNodeCount;
            tb_nodeCountFold = presenter.tb_nodeCountFold;
            tb_topologyCount = presenter.tb_topologyCount;
            clb_algorithmTypes = presenter.clb_algorithmTypes;
            clb_graphTypes = presenter.clb_graphTypes;
            lbl_currentAlgorithm = presenter.lbl_currentAlgorithm;
            lbl_currentNodeCount = presenter.lbl_currentNodeCount;
            lbl_currentTopologyIndex = presenter.lbl_currentTopologyIndex;
            lbl_currentTopologyType = presenter.lbl_currentTopologyType;
            btn_runPerformanceAnalysis = presenter.btn_runPerformanceAnalysis;
        }
    }
}
