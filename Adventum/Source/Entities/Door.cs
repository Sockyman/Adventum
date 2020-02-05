using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
	public class Door : Entity
	{
		public string level;
		public bool cacheLevel;

		public Door(Vector2 position, string level, bool cacheLevel) : base(position)
		{
			this.level = level;
			this.cacheLevel = cacheLevel;

			SetBounds(new Point(16));

			Solid = false;

			visible = false;

			Sprite = new Sprite.Animator("Furniture2", "chair");
		}
	}
}
