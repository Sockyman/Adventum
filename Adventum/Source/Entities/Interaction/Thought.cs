using System;
using System.Collections.Generic;
using Adventum.Core.Collision;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Interaction
{
	public class Thought : Sign
	{
		public override bool ShowAlert => false;

		private bool used = false;

		public Thought(Rectangle covering, string title, string text, int skin = 0) : base(covering.Location.ToVector2(), title, text, skin)
		{
			Rectangle rect = covering;
			rect.Location = new Point();
			BoundingBox = rect;
		}


		public override void OnExamine()
		{
			
		}


		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (collisionData.Other is Mobs.PlayerEntity && !used)
			{
				Show();
				used = true;
			}
		}

	}
}
