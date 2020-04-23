using System;
using Microsoft.Xna.Framework;

namespace Adventum.Core.Collision
{
    public interface ICollidable
    {
        Vector2 Position { get; set; }


        Vector2 Velocity { get; set; }
        Vector2 PreviousVelocity { get; set; }

        Rectangle BoundingBox { get; set; }
        

        Rectangle CollisionMask { get; }

        CollisionType CollisionType { get; }


        /// <summary>
        /// Whether or not the collider should be added to the list of colliders to collide with stuff.
        /// </summary>
        bool CheckCollisions { get; }
        /// <summary>
        /// Whether or not the collder runs any logic on a collision.
        /// </summary>
        bool ReactToCollisions { get; }


        void OnCollision(CollisionData collisionData);
    }

    public enum CollisionType : byte
    {
        NonSolid,
        NoPush,
        Stationary,
        Solid,
        Immovable
    }
}
