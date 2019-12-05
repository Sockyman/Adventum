using System;
using System.Collections.Generic;
using Adventum.Core.Collision;
using Adventum.Entities.Mobs;
using Adventum.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Adventum.Entities
{
    class Arrow : Attack
    {
        protected int piercing = 1;
        public int acuracy = 2000;


        public Arrow(Entity parent, Vector2 direction, float speed) : base(parent, new Point(16), direction, 2000, speed)
        {
            Sprite = new Sprite.Animator("AttackSwish", "woodArrow");

            Velocity += new Vector2(random.Next(-acuracy, acuracy), random.Next(-acuracy, acuracy)) / 10;
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
    }
}
