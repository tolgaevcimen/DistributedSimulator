using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurauDominatingSet
{
    public class TurauNode : _Node
    {
        public TurauState State { get; set; }

        int InNeighborCount
        {
            get
            {
                return GetNeighbours().Count(n => n.State == TurauState.IN);
            }
        }

        bool UniqueInNeighbour(out _Node w)
        {
            var inNeighbours = GetNeighbours().Where(n => n.State == TurauState.IN);
            if (inNeighbours.Count() != 1)
            {
                w = null;
                return false;
            }

            w = inNeighbours.FirstOrDefault();
            return true;
        }

        bool NoDependentNeighbor
        {
            get
            {
                return !GetNeighbours().Any(n => n.DependentUpon != null && n.DependentUpon.Id == Id);
            }
        }

        _Node DependentUpon { get; set; }

        bool NoBetterNeighbor
        {
            get
            {
                return !GetNeighbours().Any(n => n.State == TurauState.WAIT && n.Id < Id);
            }
        }


        IEnumerable<TurauNode> GetNeighbours()
        {
            return GetCopyOfNeigbours().Where(v => v != null).Select(n => (TurauNode)n);
        }

        public TurauNode(int id, NodeHolder nodeHolder, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id, nodeHolder)
        {
            State = GenerateState(initialState, randomizer);
        }

        public TurauNode(int id, NodeHolder nodeHolder, int predefinedState) : base(id, nodeHolder)
        {
            State = predefinedState == 0 ? TurauState.WAIT : TurauState.IN;
        }

        public TurauNode(int id, TurauState state) : base(id, null)
        {
            State = state;
        }

        protected override void RunRules()
        {
            if (State == TurauState.OUT && InNeighborCount == 0)
            {
                MoveCount++;
                SetState(TurauState.WAIT);
            }
            else if (State == TurauState.WAIT && InNeighborCount != 0)
            {
                MoveCount++;
                SetState(TurauState.OUT);
            }
            else if (State == TurauState.WAIT && InNeighborCount == 0 && NoBetterNeighbor)
            {
                MoveCount++;
                SetState(TurauState.IN);
                DependentUpon = null;
            }
            else if (State == TurauState.IN && InNeighborCount != 0 && NoDependentNeighbor)
            {
                MoveCount++;
                SetState(TurauState.OUT);
            }
            else if (State == TurauState.IN && DependentUpon != null)
            {
                MoveCount++;
                DependentUpon = null;

                BroadcastState();
            }
            else if (State == TurauState.OUT && UniqueInNeighbour(out _Node w) && ((DependentUpon != null && DependentUpon.Id != w.Id) || DependentUpon == null && w != null))
            {
                MoveCount++;
                DependentUpon = w;

                BroadcastState();
            }
            else if (State == TurauState.OUT && InNeighborCount > 1 && DependentUpon != null)
            {
                MoveCount++;
                DependentUpon = null;

                BroadcastState();
            }
            else
            {
                Visualizer.Log("I'm {0}. My state does not change.", Id);
            }
        }

        void SetState(TurauState state)
        {
            Visualizer.Log("I'm {0}. My state is {1}, was {2}", Id, state, State);

            State = state;
            Visualizer.Draw(GetState());

            BroadcastState();
        }
        
        protected override void UpdateNeighbourInformation(_Node neighbour)
        {
            UpdateNeighbour(new TurauNode(neighbour.Id, ((TurauNode)neighbour).State));
        }
        
        public override bool Selected()
        {
            return base.Selected() || State == TurauState.IN;
        }

        public override bool IsValid()
        {
            if (State == TurauState.OUT && InNeighborCount == 0)
            {
                return false;
            }
            else if (State == TurauState.WAIT && InNeighborCount != 0)
            {
                return false;
            }
            else if (State == TurauState.WAIT && InNeighborCount == 0 && NoBetterNeighbor)
            {
                return false;
            }
            else if (State == TurauState.IN && InNeighborCount != 0 && NoDependentNeighbor)
            {
                return false;
            }
            else if (State == TurauState.IN && DependentUpon != null)
            {
                return false;
            }
            else if (State == TurauState.OUT && UniqueInNeighbour(out _Node w) && ((DependentUpon != null && DependentUpon.Id != w.Id) || DependentUpon == null && w != null))
            {
                return false;
            }
            else if (State == TurauState.OUT && InNeighborCount > 1 && DependentUpon != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        TurauState GenerateState(InitialState _is, Random randomizer)
        {
            switch (_is)
            {
                case InitialState.AllWait:
                    return TurauState.WAIT;
                case InitialState.AllIn:
                    return TurauState.IN;
                case InitialState.Random:
                    {
                        var states = Enum.GetNames(typeof(TurauState));

                        var randIndex = randomizer.Next(0, states.Length);
                        var state = (TurauState)Enum.Parse(typeof(TurauState), states[randIndex]);

                        return state;
                    }
                default: return TurauState.WAIT;
            }
        }

        public override NodeState GetState()
        {
            return State == TurauState.WAIT ? NodeState.WAIT : State == TurauState.OUT ? NodeState.OUT : NodeState.IN;
        }
    }
}
