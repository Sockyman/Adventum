using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Adventum.Source.Util;

namespace Adventum.Source.Entities
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; private set; }
        public Texture2D Sprite { get; protected set; }
        

        public Entity(Vector2 position)
        {
            Position = position;

            Sprite = Core.Resource.ResourceManager.GetTexture("test");
        }


        public virtual void Update(DeltaTime delta)
        {
            Position += Velocity * delta.Seconds;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }


        public virtual void ApplyDirecionalVelocity(Angle angle, float velocity)
        {
            Vector2 direction = angle.ToVector(velocity);

            Velocity += direction;
        }
    }
}
