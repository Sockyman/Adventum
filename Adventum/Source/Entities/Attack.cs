using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Util;
using Adventum.Sprite;
using Adventum.Entities.Mobs;
using Adventum.Data;
using MonoGame.Extended;
using Adventum.Core.Collision;

namespace Adventum.Entities
{
    class Attack : Entity
    {
        public Entity parent;
        public float lifespan;
        public TimeSpan life;
        public bool lockToParent = true;

        public Attack(Entity parent, Point size, Vector2 direction, float lifespan, float speed) : base(parent.Position)
        {
            Solid = false;

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

            if (lockToParent)
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
