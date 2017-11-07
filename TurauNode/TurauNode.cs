using AsyncSimulator;
using System;
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
                return Neighbours.Select(n => (TurauNode)n).Count(n => n.State == TurauState.IN);
            }
        }

        bool UniqueInNeighbour(out _Node w)
        {
            var inNeighbours = Neighbours.Select(n => (TurauNode)n).Where(n => n.State == TurauState.IN);
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
                return !Neighbours.Select(n => (TurauNode)n).Any(n => n.DependentUpon != null && n.DependentUpon.Id == Id);
            }
        }

        _Node DependentUpon { get; set; }

        bool NoBetterNeighbor
        {
            get
            {
                return !Neighbours.Select(n => (TurauNode)n).Any(n => n.State == TurauState.WAIT && n.Id < Id);
            }
        }
        
        bool FirstTime { get; set; }

        object Lock { get; set; }

        public TurauNode(int id, InitialState initialState = InitialState.AllWait, Random randomizer = null) : base(id)
        {
            State = GetState(initialState, randomizer);
            Lock = new object();
            FirstTime = true;
        }

        public void RunRules()
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
                PokeNeighbors();
            }
            else if (State == TurauState.OUT && UniqueInNeighbour(out _Node w) && ((DependentUpon != null && DependentUpon.Id != w.Id) || DependentUpon == null && w != null) )
            {
                MoveCount++;
                DependentUpon = w;
                PokeNeighbors();
            }
            else if (State == TurauState.OUT && InNeighborCount > 1 && DependentUpon != null)
            {
                MoveCount++;
                DependentUpon = null;
                PokeNeighbors();
            }
            else
            {
                if (FirstTime)
                {
                    Visualizer.Log("I'm {0}. My state is {1}, and does not change. Will poke({2}).", Id, State, string.Join(", ", Neighbours.Select(n=>n.Id)));
                    FirstTime = false;
                    PokeNeighbors();
                }
                else
                {
                    Visualizer.Log("I'm {0}. My state is {1}, and does not change. Will Not poke.", Id, State);
                }
            }
        }

        public void AddNeighbor(TurauNode node)
        {
            Neighbours.Add(node);
        }

        void SetState(TurauState state)
        {
            Visualizer.Log("I'm {0}. My state is {1}, was {2}", Id, state, State);

            State = state;
            Visualizer.Draw(State == TurauState.IN);

            PokeNeighbors();
        }

        void PokeNeighbors()
        {
            foreach (var neighbor in Neighbours)
            {
                Task.Run(() =>
                {
                    Underlying_Send(new Message
                    {
                        Source = this,
                        Destination = neighbor
                    });
                });
            }

            Task.Run(() => RunRules());
        }

        protected override void UserDefined_ReceiveMessageProcedure(Message m)
        {
            base.UserDefined_ReceiveMessageProcedure(null);
            RunRules();
        }

        public override void UserDefined_SingleInitiatorProcedure(_Node root)
        {
            var initialNode = (TurauNode)root;

            initialNode.FirstTime = true;
            initialNode.RunRules();
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

        TurauState GetState(InitialState _is, Random randomizer)
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
    }
}
