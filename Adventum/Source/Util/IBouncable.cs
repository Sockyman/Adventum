using System;
using Microsoft.Xna.Framework;
using Adventum.Entities;

namespace Adventum.Util
{
	public class Bouncer
	{
		public float ZGravity { get; set; }

		public float Z { get; set; }

		public float ZVelocity { get; set; }

		public float Bouncines { get; set; }

		public Bouncer(float z = 0, float zGravity = 2500, float zVelocity = 250, float bouncines = 0.8f)
		{
			Z = z;
			ZGravity = zGravity;
			ZVelocity = zVelocity;
			Bouncines = bouncines;
		}


		public void Update(DeltaTime delta)
		{
			ZVelocity -= ZGravity * delta.Seconds;
			Z += ZVelocity * delta.Seconds;

			if (Z <= 0)
			{
				Z = 0;

				ZVelocity = Math.Abs(ZVelocity) * Bouncines;
			}
		}
	}
}
