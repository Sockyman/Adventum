using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Core.Collision
{
    public class CollisionData
    {
        public List<ICollidable> colliders;

        public ICollidable Other
        {
            get
            {
                return colliders[0];
            }
        }


        public CollisionData()
        {
            colliders = new List<ICollidable>();
        }
        public CollisionData(ICollidable collider)
        {
            colliders = new List<ICollidable>();

            AddCollision(collider);
        }


        public void AddCollision(ICollidable collider)
        {
            colliders.Add(collider);
        }


        public void Clear()
        {
            colliders.Clear();
        }


        public void Merge(CollisionData collisionData)
        {
            foreach (ICollidable collider in collisionData.colliders)
            {
                AddCollision(collider);
            }
        }


        public bool Colliding(ICollidable collider)
        {
            return colliders.Contains(collider);
        }


        public ICollidable TypeCollision<T>() where T : ICollidable
        {
            foreach (ICollidable collider in colliders)
            {
                if (typeof(T).IsAssignableFrom(collider.GetType()))
                {
                    return collider;
                }
            }

            return null;
        }


        public bool TestTypeCollision<T>() where T : ICollidable
        {
            return TypeCollision<T>() != null;
        }
    }
}
