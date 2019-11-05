using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;

namespace Adventum.Source.States
{
    public class Fsm<T>
    {
        protected List<State<T>> states;
        protected State<T> activeState;

        public T ActiveState
        {
            get
            {
                return activeState.Name;
            }
        }


        public Fsm()
        {
            states = new List<State<T>>();
        }


        public Fsm<T> CreateState(State<T> state)
        {
            states.Add(state);
            return this;
        }


        public void Update(DeltaTime delta)
        {
            T stateName = activeState.Update(delta);

            SetActiveState(stateName);
        }


        public void SetActiveState(T key)
        {
            State<T> state = GetLegalState(key);

            if (state != null)
            {
                activeState = state;
            }
        }


        public State<T> GetLegalState(T key)
        {
            foreach(State<T> state in states)
            {
                if (state.Name.Equals(key))
                    return state;
            }

            return null;
        }
    }
}
