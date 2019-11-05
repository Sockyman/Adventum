using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;

namespace Adventum.Source.States
{
    public class State<T>
    {
        public T Name { get; private set; }
        private List<Trigger<T>> triggers;

        public State(T name)
        {
            Name = name;
            triggers = new List<Trigger<T>>();
        }


        public State<T> AddTrigger(T target, TargetExpresion expresion)
        {
            triggers.Add(new Trigger<T>(target, expresion));
            return this;
        }


        public T Update(DeltaTime delta)
        {
            foreach(Trigger<T> trigger in triggers)
            {
                if (trigger.Evaluate())
                {
                    return trigger.Target;
                }
            }

            return default;
        }
    }
}
