using System;
using Adventum.Data;
using Adventum.Sprite;
using Adventum.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Item
{
	public abstract class Collectable
	{
		public virtual string ID { get; }
		public Animator sprite;

		public Collectable(string texture, string spriteSheet)
		{
			sprite = new Animator(spriteSheet, texture);
		}


		public virtual void Update(DeltaTime delta)
		{
			sprite.Update(delta);
		}
	}
}
