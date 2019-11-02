using System;
using Microsoft.Xna.Framework;
using Adventum.Data;
using MonoGame.Extended;

namespace Adventum.Source.Entities
{
    public class Mob : Entity
    {
        public Mob(Vector2 position) : base(position)
        {

        }



        public override void Move(Vector2 angle, float speed, bool changeDirection = false)
        {
            base.Move(angle, speed, changeDirection);
            if (changeDirection && speed != 0)
                state.State = "walk";
            else
                state.State = "idle";
        }
    }
}
