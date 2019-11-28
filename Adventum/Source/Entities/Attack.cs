using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;
using Adventum.Source.Sprite;
using Adventum.Source.Entities.Mobs;
using Adventum.Data;
using MonoGame.Extended;
using Adventum.Source.Core.Collision;

namespace Adventum.Source.Entities
{
    class Attack : Entity
    {
        public Entity parent;
        public float lifespan;
        public TimeSpan life;

        public Attack(Entity parent, Point size, Vector2 direction, float lifespan, float speed) : base(parent.Position)
        {
            Position += new Vector2(0, 0);

            this.parent = parent;

            state.Facing = Utils.AngleToDirection(Angle.FromVector(direction));

            Sprite = new Animator("AttackSwish", "attackSwish");

            SetBounds(size);

            this.lifespan = lifespan;
            life = new TimeSpan();

            ApplyDirecionalVelocity(Angle.FromVector(direction), speed);
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);

            if (life.TotalSeconds > lifespan)
            {
                Destroy();
            }

            //Position -= parent.PreviousVelocity;
            Position += parent.Velocity * delta.Seconds;

            life += delta.ElapsedGameTime;
        }


        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);

            foreach (ICollidable collider in collisionData.colliders)
            {
                if (collider is Mob && collider != parent)
                {
                    ((Mob)collider).Hurt(1, Angle.FromVector(Utils.DirectionToVector(state.Facing)));
                }
            }
        }
    }
}
