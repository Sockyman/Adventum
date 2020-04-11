using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Core.Collision
{
    class WallColider : ICollidable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 PreviousVelocity { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Rectangle CollisionMask
        {
            get
            {
                Rectangle mask = BoundingBox;
                mask.Location = Position.ToPoint();
                return mask;
            }
        }
        public CollisionType CollisionType => CollisionType.Immovable;
        public bool CheckCollisions => true;
        public bool ReactToCollisions => false;


        public WallColider(Point position, Point size)
        {
            Position = position.ToVector2();
            BoundingBox = new Rectangle(new Point(), size);
        }

        public void OnCollision(CollisionData collisionData)
        {
            
        }

    }
}
