using System;
using System.Collections.Generic;
using Adventum.Util;
using Adventum.Core.Collision;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
    class Torch : Entity, ILightEmiter
    {
        public int LightRadius { get; set; }
        public Color LightColor { get; set; }

        public bool flicker = true;

        public int startingRadius;

        public override CollisionType CollisionType => CollisionType.NonSolid;

        public Torch(Vector2 position, Color lightColor, int lightRadius = 150, bool visible = true) : base(position)
        {
            this.visible = visible;

            LightRadius = lightRadius;
            startingRadius = LightRadius;
            LightColor = lightColor;

            Sprite = new Sprite.Animator("Torch", "torch");
        }

        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            if (flicker && random.Next(10) == 0)
            {
                LightRadius += random.Next(-2, 3);
                LightRadius = MathHelper.Clamp(LightRadius, startingRadius - 10, startingRadius + 10);
            }
        }
    }
}
