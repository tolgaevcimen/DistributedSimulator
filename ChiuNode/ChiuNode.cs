using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChiuDominatingSet
{
    public class ChiuNode : _Node
    {
        public ChiuState State { get; set; }

        int InNeighborCount
        {
            get
            {
                return GetNeighbours().Count(n => n.State == ChiuState.IN);
            }
        }

        bool NoDependentNeighbor
        {
            get
            {
                return !GetNeighbours().Any(n => n.State == ChiuState.OUT1);
            }
        }

        bool NoBetterNeighbor
        {
            get
            {
                return !GetNeighbours().Any(n => n.State == ChiuState.WAIT && n.Id < Id);
            }
        }

        IEnumerable<ChiuNode> GetNeighbours()
        {
            return Neighbours.Values/*.Where(v => v != null)*/.Select(n => (ChiuNode)n);
        }

        public ChiuNode(int id, NodeHolder nodeHolder, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id, nodeHolder)
        {
            State = GetState(initialState, randomizer);
        }

        public ChiuNode(int id, ChiuState state) : base(id, null)
        {
            State = state;
        }

        protected override void RunRules()
        {
            if (State == ChiuState.WAIT && InNeighborCount == 0 && NoBetterNeighbor)
            {
                SetState(ChiuState.IN);
            }
            else if (State == ChiuState.IN && InNeighborCount == 1 && NoDependentNeighbor)
            {
                SetState(ChiuState.OUT1);
            }
            else if (State == ChiuState.IN && InNeighborCount > 1 && NoDependentNeighbor)
            {
                SetState(ChiuState.OUT2);
            }
            else if (State == ChiuState.WAIT && InNeighborCount == 1)
            {
                SetState(ChiuState.OUT1);
            }
            else if ((State == ChiuState.OUT1 || State == ChiuState.WAIT) && InNeighborCount > 1)
            {
                SetState(ChiuState.OUT2);
            }
            else if ((State == ChiuState.OUT1 || State == ChiuState.OUT2) && InNeighborCount == 0)
            {
                SetState(ChiuState.WAIT);
            }
        }
        
        void SetState(ChiuState state)
        {
            MoveCount++;
            Visualizer.Log("I'm {0}. My state is {1}, was {2}", Id, state, State);
            
            State = state;
            Visualizer.Draw(State == ChiuState.IN);

            BroadcastState();
        }
        
        protected override void UpdateNeighbourInformation(_Node neighbour)
        {
            Neighbours[neighbour.Id] = new ChiuNode(neighbour.Id, ((ChiuNode)neighbour).State);
        }
        
        public override bool Selected()
        {
            return base.Selected() || State == ChiuState.IN;
        }
        
        ChiuState GetState(InitialState _is, Random randomizer)
        {
            switch (_is)
            {
                case InitialState.AllWait:
                    return ChiuState.WAIT;
                case InitialState.AllIn:
                    return ChiuState.IN;
                case InitialState.Random:
                    {
                        var states = Enum.GetNames(typeof(ChiuState));

                        var randIndex = randomizer.Next(0, states.Length);
                        var state = (ChiuState)Enum.Parse(typeof(ChiuState), states[randIndex]);

                        state = Id == 0 ? ChiuState.WAIT :
                                Id == 1 ? ChiuState.IN :
                                Id == 2 ? ChiuState.WAIT :
                                Id == 3 ? ChiuState.OUT1 :
                                Id == 4 ? ChiuState.WAIT :
                                Id == 5 ? ChiuState.OUT2 :
                                Id == 6 ? ChiuState.IN :
                                Id == 7 ? ChiuState.WAIT :
                                Id == 8 ? ChiuState.IN :
                                ChiuState.IN;
                        return state;
                    }
                default: return ChiuState.WAIT;
            }
        }

        public override bool IsValid()
        {
            if (State == ChiuState.WAIT && InNeighborCount == 0 && NoBetterNeighbor)
            {
                return false;
            }
            else if (State == ChiuState.IN && InNeighborCount == 1 && NoDependentNeighbor)
            {
                return false;
            }
            else if (State == ChiuState.IN && InNeighborCount > 1 && NoDependentNeighbor)
            {
                return false;
            }
            else if (State == ChiuState.WAIT && InNeighborCount == 1)
            {
                return false;
            }
            else if ((State == ChiuState.OUT1 || State == ChiuState.WAIT) && InNeighborCount > 1)
            {
                return false;
            }
            else if ((State == ChiuState.OUT1 || State == ChiuState.OUT2) && InNeighborCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Id, State);
        }
    }
}
