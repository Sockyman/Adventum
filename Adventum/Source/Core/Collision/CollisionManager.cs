using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Util;

namespace Adventum.Core.Collision
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
            foreach (ICollidable source in colliders)
            {

                Vector2 tempVelocity = source.PreviousVelocity * delta.Seconds;
                source.Position += tempVelocity;
                source.PreviousVelocity = source.Velocity;
                source.Velocity = Vector2.Zero;

                foreach (ICollidable other in colliders)
                {
                    if (source.CollisionMask.Intersects(other.CollisionMask) && other != source) // && colliders[j] != colliders[i])
                    {
                        source.OnCollision(new CollisionData(other));

                        if (!source.Immovable && other.Immovable && source.Solid)
                            source.Position -= tempVelocity;
                    }
                }
            }


            Utils.SyncLists<ICollidable>(colliders, collidersToAdd, collidersToRemove);
        }


        public void AddCollider(ICollidable collider)
        {
            collidersToAdd.Add(collider);
        }


        public void RemoveCollider(ICollidable collider)
        {
            collidersToRemove.Add(collider);
        }
    }
}
