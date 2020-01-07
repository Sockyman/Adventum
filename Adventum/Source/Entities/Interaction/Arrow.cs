﻿using System;
using System.Collections.Generic;
using Adventum.Core.Collision;
using Adventum.Entities.Mobs;
using Adventum.Util;
using Adventum.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Adventum.Entities.Interaction
{
    class Arrow : Attack
    {
        protected int piercing = 1;
        public static int acuracy = 20;
        public int offset = GameWorld.random.Next(-acuracy, acuracy);


        public Arrow(Entity parent, Vector2 direction, float speed = 2000, int offset = 0) 
            : base(parent, new Point(16), new Angle(Angle.FromVector(direction).Degrees + offset, AngleType.Degree).ToUnitVector(), 2000, speed, false, "woodArrow", 1)
        {
            Solid = true;
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            if (Velocity.LengthSquared() == 0 || piercing <= 0)
            {
                Destroy();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position + new Vector2(0, 16), new Color(Color.Black, 100));
            base.Draw(spriteBatch);
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);

            if (collisionData.TestTypeCollision<Mob>())
            {
                Mob m = (Mob)collisionData.TypeCollision<Mob>();

                if (m.HitFrames <= 1 && m != parent)
                {
                    piercing--;
                }
            }
        }


        public override void OnInteract(Entity entity)
        {
            base.OnInteract(entity);


        }
    }
}
