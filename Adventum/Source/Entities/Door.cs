using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
	public class Door : Entity
	{
		public string level;

		public Door(Vector2 position, string level) : base(position)
		{
			this.level = level;

			Solid = false;

			visible = false;

			Sprite = new Sprite.Animator("Furniture2", "chair");
		}
	}
}
