using AsyncSimulator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChiuDominatingSet
{
    public class ChiuMDS : _Node
    {
        public ChiuMDS() : base(0, null)
        {

        }

        [JsonProperty]
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

        IEnumerable<ChiuMDS> GetNeighbours()
        {
            return GetCopyOfNeigbours().Select(n => (ChiuMDS)n);
        }

        internal ChiuMDS(int id, NodeHolder nodeHolder, InitialState initialState, int predefinedState) : base(id, nodeHolder)
        {
            State = GenerateState(initialState, predefinedState);
        }
        
        private ChiuMDS(int id, ChiuState state) : base(id, null)
        {
            State = state;
        }

        protected override void RunRules()
        {
            // I'm WAITING, all my neigbours are OUT, I have no WAITING neigbour whose Id is smaller then mine,
            // I'm going IN
            if (State == ChiuState.WAIT && InNeighborCount == 0 && NoBetterNeighbor)
            {
                Visualizer.Log("I'm {0}. Rule-1", Id);
                SetState(ChiuState.IN);
            }
            // I'm IN, none of my neighbours depend on me, I have exactly 1 IN neighobur
            // I'm going OUT-1
            else if (State == ChiuState.IN && InNeighborCount == 1 && NoDependentNeighbor)
            {
                Visualizer.Log("I'm {0}. Rule-2", Id);
                SetState(ChiuState.OUT1);
            }
            // I'm IN, none of my neighbours depend on me, I have more then 1 IN neighobur
            // I'm going OUT-2
            else if (State == ChiuState.IN && InNeighborCount > 1 && NoDependentNeighbor)
            {
                Visualizer.Log("I'm {0}. Rule-3", Id);
                SetState(ChiuState.OUT2);
            }
            // I'm WAITING, I have 1 IN neighbour (correcting)
            // I'm still OUT-1
            else if (State == ChiuState.WAIT && InNeighborCount == 1)
            {
                Visualizer.Log("I'm {0}. Rule-4", Id);
                SetState(ChiuState.OUT1);
            }
            // I'm OUT, I have more then 1 IN neighbour (correcting)
            // I'm still OUT-2
            else if ((State == ChiuState.OUT1 || State == ChiuState.WAIT) && InNeighborCount > 1)
            {
                Visualizer.Log("I'm {0}. Rule-5", Id);
                SetState(ChiuState.OUT2);
            }
            // I'm OUT, I have no IN neighbour (correcting)
            // I start WAITING
            else if ((State == ChiuState.OUT1 || State == ChiuState.OUT2) && InNeighborCount == 0)
            {
                Visualizer.Log("I'm {0}. Rule-6", Id);
                SetState(ChiuState.WAIT);
            }
        }
        
        void SetState(ChiuState state)
        {
            MoveCount++;
            //Visualizer.Log("I'm {0}. My state is {1}, was {2}", Id, state, State);
            
            State = state;
            Visualizer.Draw(GetState());

            BroadcastState();
        }
        
        protected override void UpdateNeighbourInformation(_Node neighbour)
        {
            UpdateNeighbour(new ChiuMDS(neighbour.Id, ((ChiuMDS)neighbour).State));
        }
        
        public override bool Selected()
        {
            return base.Selected() || State == ChiuState.IN;
        }
        
        ChiuState GenerateState(InitialState _is, int predefinedState)
        {
            switch (_is)
            {
                case InitialState.AllWait:
                    return ChiuState.WAIT;
                case InitialState.AllIn:
                    return ChiuState.IN;
                case InitialState.Random:
                    return predefinedState == 0 ? ChiuState.WAIT : predefinedState == 1 ? ChiuState.OUT1 : ChiuState.IN;
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

        public override NodeState GetState()
        {
            return State == ChiuState.WAIT ? NodeState.WAIT : State == ChiuState.OUT1 || State == ChiuState.OUT2 ? NodeState.OUT : NodeState.IN;
        }
    }
}
