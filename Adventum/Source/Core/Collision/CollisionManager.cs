using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Util;
using Adventum.Entities;
using Adventum.Entities.Particles;

namespace Adventum.Core.Collision
{
    public class CollisionManager
    {
        public static bool showMasks = false;


        private List<ICollidable> colliders;
        private List<ICollidable> collidersToAdd;
        private List<ICollidable> collidersToRemove;


        public CollisionManager()
        {
            colliders = new List<ICollidable>();
            collidersToAdd = new List<ICollidable>();
            collidersToRemove = new List<ICollidable>();
        }


        public void Update(DeltaTime delta)
        {
            for (int i = 0; i < colliders.Count; i++)
            {

				ICollidable source = colliders[i];


				Vector2 tempVelocity = source.PreviousVelocity * delta.Seconds;
                source.Position += tempVelocity;
                source.PreviousVelocity = source.Velocity;
                source.Velocity = Vector2.Zero;

				if (!(source is WallColider) && !(source is Tree) && !(source is Particle))
					for (int j = 0; j < colliders.Count; j++)
					{
						ICollidable other = colliders[j];

						Rectangle sourceMask = source.CollisionMask;
						Rectangle otherMask = other.CollisionMask;

						if (source.CollisionMask.Intersects(other.CollisionMask) && other != source)
						{



							source.OnCollision(new CollisionData(other));

							if (!source.Immovable && other.Immovable && source.Solid)
							{
								source.Position -= tempVelocity;


								if ((tempVelocity.X > 0 && source.CollisionMask.Width + source.CollisionMask.X <= other.CollisionMask.X) || (tempVelocity.X < 0 && source.CollisionMask.X >= other.CollisionMask.X + other.CollisionMask.Width))
								{
									source.Position = new Vector2(source.Position.X, source.Position.Y + tempVelocity.Y);
									if (source is Entity)
									{
										Entity e = (Entity)source;
										e.Motion = new Vector2(0, e.Motion.Y);
									}
								}
								else if ((tempVelocity.Y > 0 && source.CollisionMask.Height + source.CollisionMask.Y <= other.CollisionMask.Y) || (tempVelocity.Y < 0 && source.CollisionMask.Y >= other.CollisionMask.Y + other.CollisionMask.Height))
								{
									source.Position = new Vector2(source.Position.X + tempVelocity.X, source.Position.Y);
									if (source is Entity)
									{
										Entity e = (Entity)source;
										e.Motion = new Vector2(e.Motion.X, 0);
									}
								}
							}




						}
					}
            }


            Utils.SyncLists(colliders, collidersToAdd, collidersToRemove);
        }


        public void AddCollider(ICollidable collider)
        {
            collidersToAdd.Add(collider);
        }


        public void RemoveCollider(ICollidable collider)
        {
            collidersToRemove.Add(collider);
        }
    }
}
