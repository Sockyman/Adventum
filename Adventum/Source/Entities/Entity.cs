using Adventum.Data;
using Adventum.Core.Collision;
using Adventum.Core.Resource;
using Adventum.Sprite;
using Adventum.States;
using Adventum.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Adventum.Entities
{
    public abstract class Entity : ICollidable
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
            random = new Random(World.GameWorld.random.Next(int.MinValue, int.MaxValue));

            Solid = true;

            Position = position;
            Collisions = new CollisionData();

            InitalizeBehavior();
            

            //Sprite = new Animator();
        }


        protected virtual void InitalizeBehavior()
        {
            state = new EntityState(EState.Idle, Direction.Down);
        }


        protected virtual void SetBounds(Point bounds)
        {
            BoundingBox = new Rectangle(-bounds.X / 2, -bounds.Y / 2, bounds.X, bounds.Y);
        }



        public virtual void Update(DeltaTime delta)
        {
            Move(Motion);
            Motion = Utils.Dampen(Motion, 3000 * delta.Seconds);

            Collisions.Clear();

            Sprite.Update(delta, state);

            state.Update(delta);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite.GetTexture(), Position, color: Color.White, layerDepth: Position.Y / 360);
            Sprite.Draw(spriteBatch, Position);

            //spriteBatch.Draw(ResourceManager.GetTexture("pixel"), Position, Color.White);

            /*
            Texture2D rect = new Texture2D(Main.graphics.GraphicsDevice, CollisionMask.Width, CollisionMask.Height);
            Color[] data = new Color[CollisionMask.Width * CollisionMask.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);
            spriteBatch.Draw(rect, CollisionMask.Location.ToVector2(), Color.White);
            */
        }


        public virtual void OnCollision(CollisionData collisionData)
        {
            Collisions.Merge(collisionData);

            foreach (ICollidable c in collisionData.colliders)
            {
                if (c.Solid && Solid)
                {
                    ApplyDirecionalVelocity(Angle.FromVector(Position - c.Position), 5);
                }
            }
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


        public virtual void Destroy()
        {
            World.GameWorld.entityManager.RemoveEntity(this);
        }
    }
}
