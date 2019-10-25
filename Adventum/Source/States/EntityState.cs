using System;
using System.Collections.Generic;
using Adventum.Data;

namespace Adventum.Source.States
{
    public class EntityState
    {
        public string State { get; set; }
        public Direction Facing { get; set; }


        public EntityState(string state = "", Direction facing = Direction.Down)
        {
            State = state;
            Facing = facing;
        }
    }
}
