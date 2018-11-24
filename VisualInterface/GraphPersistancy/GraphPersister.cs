using AsyncSimulator;
using ChiuDominatingSet;
using GoddardMDSNode;
using Newtonsoft.Json;
using NodeGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurauDominatingSet;
using VisualInterface.GraphGenerator;

namespace VisualInterface.GraphPersistancy
{
    public class GraphPersister
    {
        public Presenter Presenter { get; set; }
        public GraphPersister(Presenter presenter)
        {
            Presenter = presenter;
        }

        public void SaveTopology(object sender, EventArgs e)
        {
            if (Presenter.cb_selfStab.Checked)
            {
                MessageBox.Show("Please disble self-stab mode, and try again.");
                return;
            }

            if (Presenter.NodeHolder.DetectingTermination)
            {
                MessageBox.Show("Please wait for the run to complete, and try again.");
                return;
            }

            if (Presenter.NodeHolder.NodeCount == 0)
            {
                MessageBox.Show("Please add some nodes and edges, and try again.");
                return;
            }

            var filePath = string.Empty;
            using (var fileDialog = new SaveFileDialog() { Filter = "Json files(*.json) | *.json" })
            {
                var fileSelected = fileDialog.ShowDialog();
                if (fileSelected != DialogResult.OK) return;

                var serializedList = SerializeTopology();
                using (var streamWriter = new StreamWriter(fileDialog.OpenFile()))
                {
                    streamWriter.Write(serializedList);
                }

                filePath = fileDialog.FileName;
            }

            var response = MessageBox.Show("Current topology successfully saved. Do you want to open the directory the file is saved?", "Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (response == DialogResult.Yes)
            {
                Process.Start(Path.GetDirectoryName(filePath));
            }
        }

        public void ImportTopology(object sender, EventArgs e)
        {
            if (Presenter.NodeHolder.NodeCount != 0)
            {
                MessageBox.Show("Please clear all nodes and edges, and try again.");
                return;
            }

            using (var fileDialog = new OpenFileDialog() { Filter = "Json files(*.json) | *.json" })
            {
                var fileSelected = fileDialog.ShowDialog();
                if (fileSelected != DialogResult.OK) return;

                var json = string.Empty;
                using (var streamReader = new StreamReader(fileDialog.OpenFile()))
                {
                    json = streamReader.ReadToEnd();
                }

                var algorithm = DeserializeAlgorithmType(json).AlgorithmType;

                var algorithmType = NodeFactory.GetAlgorithmType(algorithm);

                if (algorithmType == typeof(TurauMDS))
                {
                    var deserializationContext = DeserializeTopology<TurauMDS>(json);

                    var importedGraphGenerator = new ImportedGraphGenerator<TurauMDS>(Presenter, Presenter.drawing_panel, deserializationContext);
                    importedGraphGenerator.Generate(0, Presenter.NodeHolder, Presenter.EdgeHolder, algorithm);

                    return;
                }
                else if (algorithmType == typeof(ChiuMDS))
                {
                    var deserializationContext = DeserializeTopology<ChiuMDS>(json);

                    var importedGraphGenerator = new ImportedGraphGenerator<ChiuMDS>(Presenter, Presenter.drawing_panel, deserializationContext);
                    importedGraphGenerator.Generate(0, Presenter.NodeHolder, Presenter.EdgeHolder, algorithm);

                    return;
                }
                else if (algorithmType == typeof(GoddardMDS))
                {
                    var deserializationContext = DeserializeTopology<GoddardMDS>(json);

                    var importedGraphGenerator = new ImportedGraphGenerator<GoddardMDS>(Presenter, Presenter.drawing_panel, deserializationContext);
                    importedGraphGenerator.Generate(0, Presenter.NodeHolder, Presenter.EdgeHolder, algorithm);

                    return;
                }
            }
        }

        public string SerializeTopology()
        {
            var nodes = Presenter.NodeHolder.GetCopyList();

            nodes.ForEach(_n => _n._Neighbours = _n.GetCopyOfNeigbours().Select(n => n.Id).ToList());
            nodes.ForEach(_n => _n._Position = _n.Visualizer.Location);
            nodes.ForEach(_n => _n._PredefinedState = (int)_n.GetState());

            return JsonConvert.SerializeObject(new SerializationContext() { AlgorithmType = Presenter.SelectedAlgorithm, Nodes = nodes });
        }

        public DeserializationContext<T> DeserializeTopology<T>(string json) where T : _Node
        {
            return JsonConvert.DeserializeObject<DeserializationContext<T>>(json);
        }

        public DeserializationContext DeserializeAlgorithmType(string json)
        {
            var nh = JsonConvert.DeserializeObject<DeserializationContext>(json);

            return nh;
        }

        public class SerializationContext
        {
            public string AlgorithmType { get; set; }
            public List<_Node> Nodes { get; set; }
        }

        public class DeserializationContext
        {
            public string AlgorithmType { get; set; }
        }

        public class DeserializationContext<T> where T : _Node
        {
            public string AlgorithmType { get; set; }
            public List<T> Nodes { get; set; }
        }

    }
}
