using System;
using Adventum.Data;
using Adventum.Util;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Interaction
{
    public class MeleeAttack : Attack
    {
        public MeleeAttack(Entity parent, Direction direction, int damage, int size = 32, float lifeSpan = 0.1f, float speed = 500f ) 
            : base(parent, new Point(size), Utils.DirectionToVector(direction), lifeSpan, speed, true, "attackSwish", damage)
        {

        }
    }
}
