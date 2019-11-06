using System;
using System.Collections.Generic;
using Adventum.Data;
using Adventum.Source.Util;

namespace Adventum.Source.States
{
    public class EntityState : Fsm<EState>
    {
        public Direction Facing { get; set; }


        public EntityState(EState state, Direction facing = Direction.Down) : base()
        {

            Facing = facing;

            AddState(EState.Idle).AddTrigger(EState.Walk, () =>
                Facing == Direction.Down
            );

            AddState(EState.Walk).AddTrigger(EState.Idle, () => 
                Facing == Direction.Up
            );

            SetActiveState(EState.Idle);
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
        }
    }
}
