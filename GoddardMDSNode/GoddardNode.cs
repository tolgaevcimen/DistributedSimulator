using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoddardMDSNode
{
    public class GoddardNode : _Node
    {
        int In_X { get; set; }
        int OutputState_C { get; set; }

        IEnumerable<GoddardNode> GetNeighbours()
        {
            return GetCopyOfNeigbours().Where(v => v != null).Select(n => (GoddardNode)n);
        }

        public GoddardNode(int id, NodeHolder nodeHolder, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id, nodeHolder)
        {
            In_X = GetState(initialState, randomizer);
        }

        public GoddardNode(int id, NodeHolder nodeHolder, int predefinedState) : base(id, nodeHolder)
        {
            In_X = predefinedState;
        }

        public GoddardNode(int id, int x, int c) : base(id, null)
        {
            this.In_X = x;
            this.OutputState_C = c;
        }

        protected override void RunRules()
        {
            var stateAccordingToNeighbours = GiveCurrentState();

            // current state not correct,
            // correcting
            if (stateAccordingToNeighbours != OutputState_C && In_X == 0)
            {
                MoveCount++;
                Visualizer.Log("D1: I'm {0}.Rectifying neighbor count: {1}. ", Id, stateAccordingToNeighbours);

                OutputState_C = stateAccordingToNeighbours;

                BroadcastState();
            }
            // all my neighoburs are OUT, I'm OUT, I have no neighbour whose id is smaller then me, they have no IN neighbours
            // I'm going IN
            else if (stateAccordingToNeighbours == 0 && OutputState_C == 0 && In_X == 0 && !GetNeighbours().
                Any(n => n.Id < Id && n.OutputState_C == 0))
            {
                MoveCount++;
                Visualizer.Log("D2: I'm {0}. Entering set. ", Id);

                In_X = 1;
                //Visualizer.Draw(true);
                Visualizer.Draw(GetState());

                BroadcastState();
            }
            // I'm IN, but none of my neighbours depend on me,
            // I'm going OUT
            else if (stateAccordingToNeighbours > 0 && In_X == 1 && GetNeighbours().
                Where(n => n.In_X == 0).All(n => n.OutputState_C == 2))
            {
                MoveCount++;
                Visualizer.Log("D3: I'm {0}. Leaving set. ", Id);

                In_X = 0;
                OutputState_C = stateAccordingToNeighbours == 1 ? 1 : 2;
                //Visualizer.Draw(false);
                Visualizer.Draw(GetState());

                BroadcastState();
            }
        }

        int GiveCurrentState()
        {
            // all neighbours are OUT
            if (GetNeighbours().All(n => n.In_X == 0)) return 0;

            // there is exactly 1 IN neighbour
            if (GetNeighbours().Count(n => n.In_X == 1) == 1) return 1;

            // there are more than 1 IN neigbour
            return 2;
        }
        
        public override bool Selected()
        {
            return base.Selected() || In_X == 1;
        }
        
        protected override void UpdateNeighbourInformation(_Node neighbour)
        {
            var goddardNode = (GoddardNode)neighbour;
            UpdateNeighbour(new GoddardNode(neighbour.Id, goddardNode.In_X, goddardNode.OutputState_C));
        }
        
        public override bool IsValid()
        {
            var realNeigborCount = GiveCurrentState();
            if (In_X == 0 && OutputState_C != realNeigborCount)
            {
                return false;
            }
            else if (realNeigborCount == 0 && In_X == 0 && OutputState_C == 0 && !GetNeighbours().Any(n => n.Id < Id && n.OutputState_C == 0))
            {
                return false;
            }
            else if (realNeigborCount > 0 && In_X == 1 && GetNeighbours().Where(n => n.In_X == 0).All(n => n.OutputState_C == 2))
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

        public override NodeState GetState()
        {
            return In_X == 0 ? NodeState.WAIT : NodeState.IN;
        }
    }
}
