using System;
using Microsoft.Xna.Framework;

namespace Adventum.Source.Core.Collision
{
    public interface ICollidable
    {
        Vector2 Position { get; set; }


        Rectangle BoundingBox { get; set; }
        

        Rectangle CollisionMask { get; }


        void OnCollision(ICollidable other);
    }
}
