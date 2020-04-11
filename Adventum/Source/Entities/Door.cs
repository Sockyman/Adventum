using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Core.Collision;

namespace Adventum.Entities
{
	public class Door : Entity
	{
		public string level;
		public bool cacheLevel;

		public override CollisionType CollisionType => CollisionType.NonSolid;

		public Door(Vector2 position, string level, bool cacheLevel) : base(position)
		{
			this.level = level;
			this.cacheLevel = cacheLevel;

			SetBounds(new Point(16, 8));

			Rectangle bb = BoundingBox;
			bb.Y -= 10;
			BoundingBox = bb;

			visible = false;

			Sprite = new Sprite.Animator("KeyDoor", "doorPadlock");
		}


		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (collisionData.Other is Mobs.PlayerEntity e)
			{
				UseDoor(e);
			}
		}


		public virtual void UseDoor(params Entity[] toAdd)
		{
			Core.Audio.Play("levelChange");
			World.GameWorld.LoadLevel(level, cacheLevel, toAdd);
		}
	}
}
