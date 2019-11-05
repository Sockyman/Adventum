using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventum.Source.States
{
    public delegate bool TargetExpresion();

    public class Trigger<T>
    {
        public T Target { get; private set; }
        private TargetExpresion expresion;

        public Trigger(T target, TargetExpresion expresion)
        {
            Target = target;
            this.expresion = expresion;
        }


        public bool Evaluate()
        {
            return expresion.Invoke();
        }
    }
}
