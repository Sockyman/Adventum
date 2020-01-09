using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Adventum.Data;

namespace Adventum.Entities.Decor
{
    class Furniture : Entity, ILightEmiter
    {
        public int LightRadius { get; set; } = 0;
        public Color LightColor { get; set; } = Color.White;


        public Furniture(Vector2 position, string texture, string spriteSheet = "Furniture2", int size = 16, Direction facing = Direction.Down, int lightRadius = 0) : base(position)
        {
            Solid = true;
            Sprite = new Sprite.Animator(spriteSheet, texture);
            state.Facing = facing;
            LightRadius = lightRadius;

            SetBounds(new Point(size));
        }
    }
}
