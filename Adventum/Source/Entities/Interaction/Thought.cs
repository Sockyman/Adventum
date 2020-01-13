using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Interaction
{
	public class Thought : Sign
	{
		public Thought(Rectangle covering, string title, string text) : base(covering.Location.ToVector2(), title, text)
		{
			Rectangle rect = covering;
			rect.Location = new Point();
			BoundingBox = rect;
		}

	}
}
