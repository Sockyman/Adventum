using Adventum.Data;
using Adventum.Source.Core.Collision;
using Adventum.Source.Core.Resource;
using Adventum.Source.Sprite;
using Adventum.Source.States;
using Adventum.Source.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

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
        public EntityState state;
        public bool Solid { get; set; }
        public Animator Sprite { get; set; }



        public Entity(Vector2 position)
        {
            random = new Random();

            Solid = true;

            Position = position;
            Collisions = new CollisionData();
            state = new EntityState("walk", Direction.Down);

            Sprite = new Animator("HumanoidBase", ResourceManager.GetTexture("humanBase"));

            SetBounds(new Point(16));
        }



        protected void SetBounds(Point bounds)
        {
            Point origin = new Point(Sprite.Sprite.frameSize.X / 2, Sprite.Sprite.frameSize.Y);
            BoundingBox = new Rectangle(origin.X - bounds.X / 2, origin.Y - bounds.Y / 2, origin.X + bounds.X / 2, origin.Y + bounds.Y / 2);
        }


        public virtual void Update(DeltaTime delta)
        {
            Move(Velocity * delta.Seconds);
            Utils.Dampen(Velocity, 100 * delta.Seconds);

            Collisions.Clear();

            Sprite.Update(delta, state);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite.GetTexture(), Position, color: Color.White, layerDepth: Position.Y / 360);
            Sprite.Draw(spriteBatch, Position);

            spriteBatch.Draw(ResourceManager.GetTexture("pixel"), Position, Color.White);
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


        public virtual void Move(Vector2 vector, bool changeDirection = false)
        {
            Position += vector;
            if (changeDirection)
            {
                state.Facing = Utils.AngleToDirection(Angle.FromVector(vector));
            }
        }
        public virtual void Move(Vector2 angle, float speed, bool changeDirection = false)
        {
            if (angle.LengthSquared() != 0)
                Move(Angle.FromVector(angle).ToVector(speed), changeDirection);
        }
    }
}
