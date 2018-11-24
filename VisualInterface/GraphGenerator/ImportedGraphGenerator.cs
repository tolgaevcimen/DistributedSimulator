using System.Drawing;
using System.Windows.Forms;
using AsyncSimulator;
using NodeGenerator;
using static VisualInterface.GraphPersistancy.GraphPersister;

namespace VisualInterface.GraphGenerator
{
    class ImportedGraphGenerator<T> : AbstractGraphGenerator where T : _Node
    {
        public DeserializationContext<T> DeserializationContext { get; set; }
        public ImportedGraphGenerator(Presenter parentForm, Panel drawing_panel, DeserializationContext<T> deserializationContext) : base(parentForm, drawing_panel)
        {
            DeserializationContext = deserializationContext;
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(Drawing_panel.CreateGraphics(), new Rectangle());
            
            nodeCount = DeserializationContext.Nodes.Count;

            for (int i = 0; i < nodeCount; i++)
            {
                var _node = DeserializationContext.Nodes[i];
                
                var node = NodeFactory.Create(SelectedAlgorithm, _node.Id, new WinformsNodeVisualiser(arg, _node._Position.X, _node._Position.Y, nodeHolder.NodeCount, ParentForm), ParentForm.cb_selfStab.Checked, nodeHolder, _node._PredefinedState);

                nodeHolder.AddNode(node);
            };

            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = i; j < nodeCount; j++)
                {
                    if (i == j) continue;

                    if (!DeserializationContext.Nodes[i]._Neighbours.Contains(j)) continue;

                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(j);

                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
            }
        }
    }
}
