﻿using System;
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
        public CollisionData Collisions { get; set; }
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

        public bool Solid { get; set; }

        public Texture2D Sprite { get; protected set; }
        public Point Origin { get; protected set; }


        

        public Entity(Vector2 position)
        {
            random = new Random();

            Solid = true;

            Position = position;
            Collisions = new CollisionData();

            SetSprite("humanBase", new Point(16));
        }


        protected void SetSprite(string name, Point bounds)
        {
            Sprite = ResourceManager.GetTexture(name);

            Origin = new Point(Sprite.Width / 2, Sprite.Height);
            BoundingBox = new Rectangle(Origin.X - bounds.X / 2, Origin.Y - bounds.Y / 2, Origin.X + bounds.X / 2, Origin.Y + bounds.Y / 2);
        }


        public virtual void Update(DeltaTime delta)
        {
            Move(Velocity * delta.Seconds);
            Utils.Dampen(Velocity, 100 * delta.Seconds);

            Collisions.Clear();
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, color: Color.White, origin: Origin.ToVector2(), layerDepth: Position.Y / 360);

            //spriteBatch.Draw(ResourceManager.GetTexture("pixel"), Position, Color.White);
        }


        public virtual void OnCollision(CollisionData collisionData)
        {
            Collisions.Merge(collisionData);
        }


        public virtual void ApplyDirecionalVelocity(Angle angle, float velocity)
        {
            Vector2 direction = angle.ToVector(velocity);

            Velocity += direction;
        }


        public virtual void Move(Vector2 vector)
        {
            Position += vector;
        }
        public virtual void Move(Vector2 angle, float speed)
        {
            if (angle.LengthSquared() != 0)
                Move(Angle.FromVector(angle).ToVector(speed));
        }
    }
}
