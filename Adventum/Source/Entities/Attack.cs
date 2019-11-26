using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;
using Adventum.Source.Sprite;
using Adventum.Data;
using MonoGame.Extended;
using Adventum.Source.Core.Collision;

namespace Adventum.Source.Entities
{
    class Attack : Entity
    {
        public float lifespan;
        public TimeSpan life;

        public Attack(Entity parent, Point size, Vector2 direction, float lifespan, float speed) : base(parent.Position)
        {
            
            state.Facing = Utils.AngleToDirection(Angle.FromVector(direction));

            Sprite = new Animator("AttackSwish", "attackSwish");

            SetBounds(size);

            this.lifespan = lifespan;
            life = new TimeSpan();

            ApplyDirecionalVelocity(Angle.FromVector(direction), speed);
        }


        public override void Update(DeltaTime delta)
        {
            if (life.TotalSeconds > lifespan)
            {
                Destroy();
            }

            base.Update(delta);
            life += delta.ElapsedGameTime;
        }


        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);

            foreach (ICollidable collider in collisionData.colliders)
            {
                Console.WriteLine(collider.ToString());
                if (collider is Mob)
                {
                    ((Mob)collider).Hurt();
                }
            }
        }
    }
}
