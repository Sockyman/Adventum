using System;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Mobs
{
	public class Boss : Enemy
	{
		public override bool ShowHealthbar => true;

		public Boss(Vector2 position, string texture, string spriteSheet, Point bounds, int maxHealth) : base(position, texture, spriteSheet, maxHealth: maxHealth)
		{
			SetBounds(bounds);
		}

		
	}
}
