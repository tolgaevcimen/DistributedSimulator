using AsyncSimulator;
using System;
using System.Linq;

namespace ChiuDominatingSet
{
    public class ChiuNode : _Node
    {
        public ChiuState State { get; set; }

        int InNeighborCount
        {
            get
            {
                return Neighbours.Select(n => (ChiuNode)n).Count(n => n.State == ChiuState.IN);
            }
        }

        bool NoDependentNeighbor
        {
            get
            {
                return !Neighbours.Select(n => (ChiuNode)n).Any(n => n.State == ChiuState.OUT1);
            }
        }

        bool NoBetterNeighbor
        {
            get
            {
                return !Neighbours.Select(n => (ChiuNode)n).Any(n => n.State == ChiuState.WAIT && n.Id < Id);
            }
        }
        
        public ChiuNode(int id, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id)
        {
            State = GetState(initialState, randomizer);
        }

        public void RunRules()
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
            else
            {
                Visualizer.Log("I'm {0}. My state is {1}, and does not change.", Id, State);
                if (FirstTime)
                {
                    FirstTime = false;
                    PokeNeighbors();
                }
            }
        }
        
        void SetState(ChiuState state)
        {
            MoveCount++;
            Visualizer.Log("I'm {0}. My state is {1}, was {2}", Id, state, State);
            
            State = state;
            Visualizer.Draw(State == ChiuState.IN);

            PokeNeighbors();
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

        protected override void UserDefined_ReceiveMessageProcedure(Message m)
        {
            base.UserDefined_ReceiveMessageProcedure(null);
            RunRules();
        }

        public override void UserDefined_SingleInitiatorProcedure(_Node root)
        {
            var initialNode = (ChiuNode)root;

            initialNode.FirstTime = true;
            initialNode.RunRules();
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
    }
}
