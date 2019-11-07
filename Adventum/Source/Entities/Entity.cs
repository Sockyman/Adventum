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
        public Vector2 PreviousVelocity { get; set; }

        public Vector2 Motion { get; set; }
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

            InitalizeBehavior();

            Sprite = new Animator("HumanoidBase", ResourceManager.GetTexture("humanBase"));

            SetBounds(new Point(16));
        }


        protected virtual void InitalizeBehavior()
        {
            state = new EntityState(EState.Idle, Direction.Down);
        }


        protected void SetBounds(Point bounds)
        {
            Point origin = new Point(Sprite.Sprite.frameSize.X / 2, Sprite.Sprite.frameSize.Y);
            BoundingBox = new Rectangle(origin.X - bounds.X / 2, origin.Y - bounds.Y / 2, origin.X + bounds.X / 2, origin.Y + bounds.Y / 2);
        }


        public virtual void Update(DeltaTime delta)
        {
            Move(Motion * delta.Seconds);
            Utils.Dampen(Motion, 100 * delta.Seconds);

            Collisions.Clear();

            Sprite.Update(delta, state);

            state.Update(delta);
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

            Motion += direction;
        }


        public virtual void Move(Vector2 vector, bool changeDirection = false)
        {
            Velocity += vector;
            if (changeDirection && vector.LengthSquared() != 0)
            {
                state.Facing = Utils.AngleToDirection(Angle.FromVector(vector));
            }
        }
        public virtual void Move(Vector2 angle, float speed, bool changeDirection = false)
        {
            if (angle.LengthSquared()  != 0)
                Move(Angle.FromVector(angle).ToVector(speed), changeDirection);
        }
    }
}
