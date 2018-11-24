using System.Drawing;

namespace AsyncSimulator
{
    public abstract class AbstractEdge : IEdge
    {
        public _Node Node1 { get; set; }
        public _Node Node2 { get; set; }

        public AbstractEdge(_Node node1, _Node node2)
        {
            Node1 = node1;
            Node2 = node2;

            HandleNeighbourhood();
        }

        protected abstract void HandleVisualization();

        public abstract void Colorify(bool reverted);

        /// <summary>
        /// Handles neighbourhood between nodes
        /// </summary>
        /// <param name="onlyEdgeDeleted"></param>
        public virtual void Delete(bool onlyEdgeDeleted)
        {
            // set neighbourhood
            Node1.RemoveNeighbour(Node2.Id);
            Node2.RemoveNeighbour(Node1.Id);
        }

        public abstract void Draw();

        public _Node GetNode1()
        {
            return Node1;
        }

        public _Node GetNode2()
        {
            return Node2;
        }

        public abstract bool OnPoint(Point location);

        void HandleNeighbourhood()
        {
            /// set neighbourhood
            Node1.AddNeighbour(Node2);
            Node2.AddNeighbour(Node1);
        }        
    }
}
