using Adventum.Data;
using Adventum.Core.Collision;
using Adventum.Core.Resource;
using Adventum.Sprite;
using Adventum.States;
using Adventum.Util;
using Adventum.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Adventum.Entities
{
    public abstract class Entity : ICollidable
    {
        public virtual float MaxMovementSpeed => 200;

        protected Random random;

        public Vector2 Position { get; set; }
        /// <summary>
        /// The number of distance the entity will move next framw in pixels per second.
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Used only by EntityManager. Used so the velocity can be checked properly.
        /// </summary>
        public Vector2 PreviousVelocity { get; set; }

        /// <summary>
        /// Works like inertia. Dampens each frame.
        /// </summary>
        public Vector2 Motion { get; set; }
        public CollisionData Collisions { get; set; }
        /// <summary>
        /// Determines the CollisionMask. Centered around 0,0.
        /// </summary>
        public Rectangle BoundingBox { get; set; }
        /// <summary>
        /// The actual recatangle which covers the entity.
        /// </summary>
        public Rectangle CollisionMask
        {
            get
            {
                Rectangle mask = BoundingBox;
                Point pos = new Point((int)Position.X, (int)Position.Y);
                mask.Location += pos;
                return mask;
            }
        }
        public EntityState state;

        public virtual bool Immovable => false;
        public virtual bool CheckCollisions => true;
        public virtual bool ReactToCollisions => true;
        public bool Solid { get; set; }
        public Animator Sprite { get; set; }

		public Color drawColor = Color.White;


        public bool visible = true;



        public Entity(Vector2 position)
        {
            random = new Random(World.GameWorld.random.Next(int.MinValue, int.MaxValue));

            Solid = true;

            Position = position;
            Collisions = new CollisionData();

            InitalizeBehavior();
            

            //Sprite = new Animator();
        }


        /// <summary>
        /// Ran in constructor to build state machine.
        /// </summary>
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
            
            if (visible)
                Sprite.Draw(spriteBatch, Position, drawColor, Position.Y / GameWorld.level.Map.HeightInPixels);

            /*Texture2D rect = new Texture2D(Main.graphics.GraphicsDevice, CollisionMask.Width, CollisionMask.Height);
            Color[] data = new Color[CollisionMask.Width * CollisionMask.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);
            spriteBatch.Draw(rect, CollisionMask.Location.ToVector2(), Color.White);*/
            
        }


        public virtual void OnCollision(CollisionData collisionData)
        {
            Collisions.Merge(collisionData);
            if (collisionData.Other.Solid && Solid)
            {
                ApplyDirecionalVelocity(Angle.FromVector(Position - collisionData.Other.Position), 5000 * GameWorld.deltaTime.Seconds);
            }
        }


        /// <summary>
        /// Changes the inertia.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="velocity"></param>
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


        /// <summary>
        /// Called when the entity is destroyed. Can be used to create particles, drops, ect.
        /// </summary>
        public virtual void Die()
        {
            Destroy();
        }
        /// <summary>
        /// Called when the entity is to be removed from the game (eg. being unloaded).
        /// </summary>
        public void Destroy()
        {
            GameWorld.EntityManager.RemoveEntity(this);
        }
    }
}
