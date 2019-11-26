using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Util;

namespace Adventum.Source.Core.Collision
{
    public class CollisionManager
    {
        public static bool showMasks = false;


        private List<ICollidable> colliders;
        private List<ICollidable> collidersToAdd;
        private List<ICollidable> collidersToRemove;


        public CollisionManager()
        {
            colliders = new List<ICollidable>();
            collidersToAdd = new List<ICollidable>();
            collidersToRemove = new List<ICollidable>();
        }


        public void Update(DeltaTime delta)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    if (colliders[i].CollisionMask.Intersects(colliders[j].CollisionMask) && colliders[j] != colliders[i])
                    {
                        colliders[i].OnCollision(new CollisionData(colliders[j]));
                        colliders[j].OnCollision(new CollisionData(colliders[i]));
                    }
                }

                colliders[i].Position += colliders[i].PreviousVelocity * delta.Seconds;
                colliders[i].PreviousVelocity = colliders[i].Velocity;
                colliders[i].Velocity = Vector2.Zero;
            }


            Utils.SyncLists<ICollidable>(colliders, collidersToAdd, collidersToRemove);
        }


        public void AddCollider(ICollidable collider)
        {
            collidersToAdd.Add(collider);
        }


        public void RemoveCollider(ICollidable collider)
        {
            collidersToRemove.Remove(collider);
        }
    }
}
