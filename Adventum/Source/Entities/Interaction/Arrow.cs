using System;
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

        public override int Dampening => 100;

        private Bouncer bouncer;
        public override float Z
        {
            get { return bouncer.Z; }
        }

        public override CollisionType CollisionType => CollisionType.Solid;


        public Arrow(Entity parent, Vector2 direction, float speed = 1000, int offset = 0) 
            : base(parent, new Point(8), new Angle(Angle.FromVector(direction).Degrees + offset, AngleType.Degree).ToUnitVector(), 2000, speed, false, "woodArrow", 1)
        {
            bouncer = new Bouncer(16, zVelocity: 300, bouncines: 0);
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            bouncer.Update(delta);

            if (Velocity.LengthSquared() == 0 || piercing <= 0 || Math.Round(bouncer.Z * 10) == 0)
            {
                Die();
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

            if (collisionData.Other.CollisionType == CollisionType.Immovable)
            {
                Die();
            }
        }


        public override void OnInteract(Entity entity)
        {
            base.OnInteract(entity);


        }


        public override void Die()
        {
            base.Die();

            GameWorld.DisolveToParticles(this);
        }
    }
}
