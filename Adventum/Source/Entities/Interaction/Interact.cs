﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Util;
using Adventum.Sprite;
using Adventum.Entities.Mobs;
using Adventum.Data;
using MonoGame.Extended;
using Adventum.Core.Collision;

namespace Adventum.Entities.Interaction
{
    public class Interact : Entity
    {
        public Entity parent;
        public float lifespan;
        public TimeSpan life;
        public bool lockToParent;
        protected CollisionData previousCollisions;

        public override CollisionType CollisionType => CollisionType.NonSolid;

        public Interact(Entity parent, Point size, Vector2 direction, float lifespan, float speed, bool lockToParent) : base(parent.Position)
        {
            Position += new Vector2(0, 0);

            this.parent = parent;
            this.lockToParent = lockToParent;

            state.Facing = Utils.AngleToDirection(Angle.FromVector(direction));

            Sprite = new Animator("None", "");

            SetBounds(size);

            this.lifespan = lifespan;
            life = new TimeSpan();

            ApplyDirecionalVelocity(Angle.FromVector(direction), speed);

            previousCollisions = new CollisionData();
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

            ICollidable other = collisionData.Other;

            if (!previousCollisions.colliders.Contains(other) && other is Entity && other != parent)
            {
                OnInteract((Entity)other);
            }


            previousCollisions.Merge(collisionData);
        }


        public virtual void OnInteract(Entity entity)
        {

        }
    }
}
