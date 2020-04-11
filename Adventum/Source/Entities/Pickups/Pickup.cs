using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Util;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using Adventum.Core;

namespace Adventum.Entities.Pickups
{
	public abstract class Pickup : Entity
	{
		private Bouncer bouncer;

		public virtual string Sound => "";

		public override int Dampening => 1000;

		protected CountdownTimer grabTimer;

		protected bool grabable = false;


		public Pickup(Vector2 position, string texture, string sprite) : base(position)
		{
			Sprite = new Sprite.Animator(sprite, texture);

			grabTimer = new CountdownTimer(0.25);
			grabTimer.Completed += (object sender, EventArgs args) => { grabable = true; };

			bouncer = new Bouncer(16, 3000);
			SetBounds(new Point(8));

			ApplyDirecionalVelocity(new Angle((float)random.NextDouble(), AngleType.Revolution), 1);
		}

		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			grabTimer.Update(delta);

			bouncer.Update(delta);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (visible)
			{
				Vector2 dp = Position;
				dp.Y -= bouncer.Z;

				Sprite.Draw(spriteBatch, dp, Color.White, Position.Y / World.GameWorld.level.Map.HeightInPixels );
				Sprite.Draw(spriteBatch, Position, new Color(Color.Black, 50));
			}
		}

		/// <summary>
		/// Called externally to pick up this object.
		/// </summary>
		/// <param name="mob"></param>
		public void Grab(Mobs.Mob mob)
		{
			if (grabable)
			{
				DoGrab(mob);
			}
		}

		/// <summary>
		/// Called by Grab.
		/// </summary>
		/// <param name="mob"></param>
		protected virtual void DoGrab(Mobs.Mob mob)
		{
			Audio.Play(Sound, 0.1f, true);
			Die();
		}
	}
}
