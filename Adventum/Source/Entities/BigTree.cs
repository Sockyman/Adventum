using System;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
	public class BigTree : Tree
	{
		public BigTree(Vector2 position, bool collides) : base(position, collides)
		{
			Sprite = new Sprite.Animator("BigTree", "bigTree");
			SetBounds(new Point(64, 16));
		}
	}
}
