using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Interaction
{
    public class Attack : Interact
    {
        public Attack(Entity parent, Point size, Vector2 direction, float speed, bool lockToParent, string textureName, int damage) : base(parent, size, direction, speed, lockToParent)
        {

        }
    }
}
