using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Adventum.Source.Core.Resource;
using Adventum.Source.Util;
using Adventum.Source.Core.Collision;

namespace Adventum.Source.Entities
{
    public class Entity : ICollidable
    {
        public const float MaxMovementSpeed = 200;

        protected Random random;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Rectangle CollisionMask
        {
            get
            {
                Rectangle mask = BoundingBox;
                mask.Location += Position.ToPoint();
                return mask;
            }
        }

        public Texture2D Sprite { get; protected set; }
        public Point Origin { get; protected set; }


        int tempTest;
        

        public Entity(Vector2 position)
        {
            random = new Random();

            Position = position;

            SetSprite("humanBase", new Point(16));

            tempTest = random.Next(1000);
        }


        protected void SetSprite(string name, Point bounds)
        {
            Sprite = ResourceManager.GetTexture(name);

            Origin = new Point(Sprite.Width / 2, Sprite.Height);
            BoundingBox = new Rectangle(Origin.X - bounds.X / 2, Origin.Y - bounds.Y / 2, Origin.X + bounds.X / 2, Origin.Y + bounds.Y / 2);
        }


        public virtual void Update(DeltaTime delta)
        {
            Position += Velocity * delta.Seconds;
            Utils.Dampen(Velocity, 100 * delta.Seconds);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, color: Color.White, origin: Origin.ToVector2(), layerDepth: Position.Y / 360);

            //spriteBatch.Draw(ResourceManager.GetTexture("pixel"), Position, Color.White);
        }


        public virtual void OnCollision(ICollidable other)
        {
            Console.WriteLine(tempTest);
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
