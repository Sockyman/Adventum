using System;
using System.Collections.Generic;
using System.Text;
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
                if (activeState != null)
                    return activeState.Name;
                return default;
            }
        }


        public Fsm()
        {
            states = new List<State<T>>();
        }


        public State<T> AddState(T stateName)
        {
            State<T> state = GetLegalState(stateName);
            if (state == null)
            {
                state = new State<T>(stateName, this);
                states.Add(state);
            }
            
            return state;
        }


        public virtual void Update(DeltaTime delta)
        {
            activeState.Update(delta);

            //Console.WriteLine(ToString());
        }


        public void SetActiveState(T key)
        {
            State<T> state = GetLegalState(key);

            if (state != null)
            {
                state.clock.Restart();
                activeState = state;
                state.OnEnter.ExcecuteTarget();
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


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("State: ");
            sb.Append(typeof(T).ToString());
            sb.Append(".");
            sb.Append(ActiveState.ToString());
            return sb.ToString();
        }
    }
}
