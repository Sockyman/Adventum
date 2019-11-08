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
        public Trigger<T> OnEnter { get; private set; }
        

        protected Fsm<T> parent;


        public State(T name, Fsm<T> parent)
        {
            

            Name = name;
            triggers = new List<Trigger<T>>();

            OnEnter = new Trigger<T>(this, () => { }, () => false);

            this.parent = parent;
        }


        public Trigger<T> AttachTrigger(Trigger<T> trigger)
        {
            triggers.Add(trigger);
            return trigger;
        }

        public State<T> AddEntranceTrigger(TargetExpresion target)
        {
            OnEnter.target += target;
            return this;
        }
        public State<T> AddTrigger(TargetExpresion target, TriggerExpresion expresion)
        {
            AttachTrigger(new Trigger<T>(this, target, expresion));
            return this;
        }
        public State<T> AddStateTrigger(T target, TriggerExpresion expression)
        {
            return AddTrigger(() => ChangeState(target), expression);
        }
        public State<T> AddUpdateTrigger(TargetExpresion target)
        {
            return AddTrigger(target, () => true);
        }
        public State<T> AddCountdownTrigger(TargetExpresion target, float seconds)
        {
            return AddTrigger(target, () => parent.clock.CurrentTime.TotalSeconds > seconds);
        }
        public State<T> AddCountdownStateTrigger(T target, float seconds)
        {
            return AddCountdownTrigger(() => ChangeState(target), seconds);
        }

        public Trigger<T> RecentTrigger()
        {
            return triggers[triggers.Count - 1];
        }


        protected void ChangeState(T state)
        {
            parent.SetActiveState(state);
        }


        public void Update(DeltaTime delta)
        {
            foreach(Trigger<T> trigger in triggers)
            {
                trigger.Evaluate();
            }
        }
    }
}
