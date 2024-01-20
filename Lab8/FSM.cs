using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    internal class FSM <S,E>
        where S : Enum
        where E : Enum
    {
        State<S> currentState = null;

        private readonly Dictionary<S, State<S>> states = new Dictionary<S, State<S>>();

        private List<Transition<S, E>> transitions;

        public FSM(S initialState)
        {
            foreach (S item in Enum.GetValues(typeof(S)))
            {
                State<S> state = new State<S>() { Name = item };
                states.Add(state.Name, state);
            }

            currentState = GetState(initialState);
        }

        private State<S> GetState(S st)
        {
            if (!states.ContainsKey(st)) return null;
            return states[st];
        }

        public void RegisterTransitions(Transition<S, E>[] trans)
        {
            transitions = new List<Transition<S, E>>(trans);
        }

        private bool SetState(S stateName)
        {
            if (!states.ContainsKey(stateName)) return false;

            currentState.OnExit?.Invoke();

            currentState = states[stateName];

            currentState.OnEnter?.Invoke();

            return true;
        }

        public void Tick()
        {
            currentState.OnUpdate?.Invoke();
        }

        public void OnEvent(E Event)
        {
            var res = transitions.Where(
                e => (e.FromState == null || e.FromState.Equals(currentState.Name) && e.Event.Equals(Event))
                );
            if (res.Any()) SetState(res.First().ToState);
        }

        public State<S> this[S stateName]
        {
            get 
            {
                if (!states.ContainsKey(stateName)) return null;
                return states[stateName];
            }
        }
    }
}
