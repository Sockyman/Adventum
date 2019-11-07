using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventum.Source.States.Triggers
{
    public delegate bool TriggerExpresion();
    public delegate void TargetExpresion();

    public class Trigger<T>
    {
        private TargetExpresion target;
        private TriggerExpresion expresion;
        private State<T> parentState;

        public Trigger(State<T> parentState, TargetExpresion target, TriggerExpresion expresion)
        {
            this.parentState = parentState;

            this.target = target;
            this.expresion = expresion;
        }


        public bool Evaluate()
        {
            if (expresion.Invoke())
            {
                target.Invoke();
                return true;
            }
            return false;
        }
    }
}
