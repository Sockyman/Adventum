using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Adventum.Source.Util;

namespace Adventum.Source.Entities
{
    public class Entity : IActorTarget
    {
        public const float MaxMovementSpeed = 200;


        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public RectangleF BoundingBox { get; set; }
        public Texture2D Sprite { get; protected set; }
        

        public Entity(Vector2 position)
        {
            Position = position;

            Sprite = Core.Resource.ResourceManager.GetTexture("humanBase");
        }


        public virtual void Update(DeltaTime delta)
        {
            Position += Velocity * delta.Seconds;
            Utils.Dampen(Velocity, 100 * delta.Seconds);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }


        public virtual void OnCollision(CollisionInfo collisionInfo)
        {
            
        }


        public virtual void ApplyDirecionalVelocity(Angle angle, float velocity)
        {
            Vector2 direction = angle.ToVector(velocity);

            Velocity += direction;
        }


        public virtual void Move(Vector2 angle, float speed)
        {
            if (angle.LengthSquared() != 0)
                Position += Angle.FromVector(angle).ToVector(speed);
        }
    }
}
