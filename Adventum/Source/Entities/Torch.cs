using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
    class Torch : Entity, ILightEmiter
    {
        public int LightRadius { get; set; }
        public Color LightColor { get; set; }

        public Torch(Vector2 position, Color lightColor, int lightRadius = 150, bool visible = true) : base(position)
        {
            Solid = false;

            this.visible = visible;

            LightRadius = lightRadius;
            LightColor = lightColor;

            Sprite = new Sprite.Animator("Torch", "torch");
        }
    }
}
