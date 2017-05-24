using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoddardMDSNode
{
    public class GoddardNode : _Node
    {
        public int x { get; set; }
        public int c { get; set; }

        bool FirstTime { get; set; }

        object Lock { get; set; }

        public GoddardNode(int id) : base(id)
        {
            Lock = new object();
        }

        public void RunRules()
        {
            lock (Lock)
            {
                Thread.Sleep(500);

                var realNeigborCount = GiveNeighborCount();

                if (x == 0 && c != realNeigborCount)
                {
                    Visualizer.Log("D1: I'm {0}.Rectifying neighbor count: {1}. ", Id, realNeigborCount);

                    c = realNeigborCount;
                    PokeNeighbors();
                }
                else if (realNeigborCount == 0 && x == 0 && c == 0 && !Neighbours.Any(n => n.Id < Id && ((GoddardNode)n).c == 0))
                {
                    Visualizer.Log("D2: I'm {0}. Entering set. ", Id);

                    x = 1;
                    PokeNeighbors();
                    Visualizer.Draw(true);
                }
                else if (realNeigborCount > 0 && x == 1 && Neighbours.Where(n => ((GoddardNode)n).x == 0).All(n => ((GoddardNode)n).c == 2))
                {
                    Visualizer.Log("D3: I'm {0}. Leaving set. ", Id);

                    x = 0;
                    c = realNeigborCount == 1 ? 1 : 2;
                    PokeNeighbors();
                    Visualizer.Draw(false);
                }
                else
                {
                    if (FirstTime)
                    {
                        FirstTime = false;
                        PokeNeighbors();
                    }
                }
            }
        }

        int GiveNeighborCount()
        {
            if (Neighbours.All(n => ((GoddardNode)n).x == 0)) return 0;

            if (Neighbours.Count(n => ((GoddardNode)n).x == 1) == 1) return 1;

            return 2;
        }

        void PokeNeighbors()
        {
            foreach (var neighbor in Neighbours.AsParallel())
            {
                Underlying_Send(new Message
                {
                    Source = this,
                    Destination = neighbor
                });
            }
        }

        public override bool Selected()
        {
            return base.Selected() || x == 1;
        }

        protected override void UserDefined_ReceiveMessageProcedure(Message m)
        {
            RunRules();
        }
        
        public override void UserDefined_SingleInitiatorProcedure(_Node root)
        {
            var initialNode = (GoddardNode)root;

            initialNode.FirstTime = true;
            initialNode.RunRules();
            initialNode.Start();
        }

        public override void UserDefined_ConcurrentInitiatorProcedure(List<_Node> allNodes)
        {
            throw new NotImplementedException();
        }
    }
}
