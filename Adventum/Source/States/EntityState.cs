using System;
using System.Collections.Generic;
using Adventum.Data;

namespace Adventum.Source.States
{
    public class EntityState : Fsm<EState>
    {
        public Direction Facing { get; set; }


        public EntityState(EState state, Direction facing = Direction.Down) : base()
        {
            Facing = facing;

            CreateState(new State<EState>(EState.Idle).AddTrigger(EState.Walk, () =>
                facing == Direction.Up
            ));

            CreateState(new State<EState>(EState.Walk));

            SetActiveState(EState.Idle);
        }
    }
}
