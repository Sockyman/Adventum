using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;
using Adventum.Source.States.Triggers;

namespace Adventum.Source.States
{
    public class State<T>
    {
        public T Name { get; private set; }
        private List<Trigger<T>> triggers;

        private Fsm<T> parent;


        public State(T name, Fsm<T> parent)
        {
            Name = name;
            triggers = new List<Trigger<T>>();

            this.parent = parent;
        }


        public State<T> AddTrigger(TargetExpresion target, TriggerExpresion expresion)
        {
            triggers.Add(new Trigger<T>(this, target, expresion));
            return this;
        }
        public State<T> AddStateTrigger(T target, TriggerExpresion expression)
        {
            return AddTrigger(() => parent.SetActiveState(target), expression);
        }
        public State<T> AddUpdateTrigger(TargetExpresion target)
        {
            return AddTrigger(target, () => true);
        }


        public T Update(DeltaTime delta)
        {
            foreach(Trigger<T> trigger in triggers)
            {
                if (trigger.Evaluate())
                {
                    
                }
            }

            return default;
        }
    }
}
