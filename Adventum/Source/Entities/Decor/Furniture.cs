using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Adventum.Data;
using Adventum.Core.Collision;

namespace Adventum.Entities.Decor
{
    class Furniture : Entity, ILightEmiter
    {
        public int LightRadius { get; set; } = 0;
        public Color LightColor { get; set; } = Color.White;

        public override CollisionType CollisionType
        {
            get
            {
                return collisionType;
            }
        }
        public CollisionType collisionType;


        public Furniture(Vector2 position, string texture, string spriteSheet = "Furniture2", int size = 16, Direction facing = Direction.Down, int lightRadius = 0) : base(position)
        {
            Sprite = new Sprite.Animator(spriteSheet, texture);
            state.Facing = facing;
            LightRadius = lightRadius;

            collisionType = CollisionType.Solid;

            SetBounds(new Point(size));
        }
    }
}
