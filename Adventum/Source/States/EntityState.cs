using System;
using System.Collections.Generic;
using Adventum.Data;
using Adventum.Source.Util;

namespace Adventum.Source.States
{
    public class EntityState : Fsm<EState>
    {
        public Direction Facing { get; set; }
        Random random = new Random();
        int randval;


        public EntityState(EState state, Direction facing = Direction.Down) : base()
        {
            randval = 27;

            Facing = facing;

            CreateState(new State<EState>(EState.Idle).AddTrigger(EState.Walk, () =>
                randval == 27
            ));

            CreateState(new State<EState>(EState.Walk).AddTrigger(EState.Idle, () => 
                randval == 301
            ));

            SetActiveState(EState.Idle);
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            randval = 301;
        }
    }
}
