using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventum.Source.States
{
    public delegate bool TriggerExpresion();
    public delegate void TargetExpresion();

    public class Trigger<T>
    {
        public TargetExpresion target;
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
                ExcecuteTarget();
                return true;
            }
            return false;
        }


        public void ExcecuteTarget()
        {
            target.Invoke();
        }
    }
}
