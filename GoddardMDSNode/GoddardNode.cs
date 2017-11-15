using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoddardMDSNode
{
    public class GoddardNode : _Node
    {
        int x { get; set; }
        int c { get; set; }

        IEnumerable<GoddardNode> GetNeighbours()
        {
            return GetCopyOfNeigbours().Where(v => v != null).Select(n => (GoddardNode)n);
        }

        public GoddardNode(int id, NodeHolder nodeHolder, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id, nodeHolder)
        {
            x = GetState(initialState, randomizer);
        }

        public GoddardNode(int id, int x, int c) : base(id, null)
        {
            this.x = x;
            this.c = c;
        }

        protected override void RunRules()
        {
            var realNeigborCount = GiveNeighborCount();

            if (x == 0 && c != realNeigborCount)
            {
                MoveCount++;
                Visualizer.Log("D1: I'm {0}.Rectifying neighbor count: {1}. ", Id, realNeigborCount);

                c = realNeigborCount;

                BroadcastState();
            }
            else if (realNeigborCount == 0 && x == 0 && c == 0 && !GetNeighbours().Any(n => n.Id < Id && ((GoddardNode)n).c == 0))
            {
                MoveCount++;
                Visualizer.Log("D2: I'm {0}. Entering set. ", Id);

                x = 1;
                Visualizer.Draw(true);

                BroadcastState();
            }
            else if (realNeigborCount > 0 && x == 1 && GetNeighbours().
                Where(n => ((GoddardNode)n).x == 0).All(n => ((GoddardNode)n).c == 2))
            {
                MoveCount++;
                Visualizer.Log("D3: I'm {0}. Leaving set. ", Id);

                x = 0;
                c = realNeigborCount == 1 ? 1 : 2;
                Visualizer.Draw(false);

                BroadcastState();
            }
        }

        int GiveNeighborCount()
        {
            if (GetNeighbours().All(n => n.x == 0)) return 0;

            if (GetNeighbours().Count(n => n.x == 1) == 1) return 1;

            return 2;
        }
        
        public override bool Selected()
        {
            return base.Selected() || x == 1;
        }
        
        protected override void UpdateNeighbourInformation(_Node neighbour)
        {
            var goddardNode = (GoddardNode)neighbour;
            UpdateNeighbour(new GoddardNode(neighbour.Id, goddardNode.x, goddardNode.c));
        }
        
        public override bool IsValid()
        {
            var realNeigborCount = GiveNeighborCount();
            if (x == 0 && c != realNeigborCount)
            {
                return false;
            }
            else if (realNeigborCount == 0 && x == 0 && c == 0 && !GetNeighbours().Any(n => n.Id < Id && n.c == 0))
            {
                return false;
            }
            else if (realNeigborCount > 0 && x == 1 && GetNeighbours().Where(n => n.x == 0).All(n => n.c == 2))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        int GetState(InitialState _is, Random randomizer)
        {
            switch (_is)
            {
                case InitialState.AllWait:
                    return 0;
                case InitialState.AllIn:
                    return 1;
                case InitialState.Random:
                    {
                        var randIndex = randomizer.Next(0, 2);

                        return randIndex;
                    }
                default: return 0;
            }
        }
    }
}
