using System;
using System.Collections.Generic;
using Adventum.Util;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
    class Torch : Entity, ILightEmiter
    {
        public int LightRadius { get; set; }
        public Color LightColor { get; set; }

        public bool flicker = true;

        public Torch(Vector2 position, Color lightColor, int lightRadius = 150, bool visible = true) : base(position)
        {
            Solid = false;

            this.visible = visible;

            LightRadius = lightRadius;
            LightColor = lightColor;

            Sprite = new Sprite.Animator("Torch", "torch");
        }

        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            if (flicker && random.Next(10) == 0)
            {
                LightRadius += random.Next(-2, 3);
            }
        }
    }
}
