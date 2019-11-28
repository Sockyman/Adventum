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


        bool Solid { get; set; }


        void OnCollision(CollisionData collisionData);
    }
}
