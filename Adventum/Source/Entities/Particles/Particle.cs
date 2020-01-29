using System;
using Adventum.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using Adventum.Data;

namespace Adventum.Entities.Particles
{
	public class Particle : Entity
	{

		CountdownTimer life;


		public Particle(Vector2 position, string texture, string sprite, Color color, Angle direction, float speed, float lifeSpan) : base(position)
		{
			Sprite = new Sprite.Animator(sprite, texture);

			ApplyDirecionalVelocity(direction, speed);

			life = new CountdownTimer(lifeSpan);

			drawColor = color;

			Solid = false;
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			life.Update(delta);


			if (life.CurrentTime >= life.Interval)
			{
				Die();
			}
		}


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}


		public static Particle GenerateFromEffect(ParticleEffect effect, Vector2 position)
		{
			Particle particle = new Particle(position, effect.texture, effect.sprite, Color.Lerp(effect.color, effect.mixColor, (float)Utils.random.NextDouble()),
				new Angle(Utils.RandomRange(effect.direction), AngleType.Revolution), Utils.RandomRange(effect.speed), Utils.RandomRange(effect.lifeSpan));

			return particle;
		}
	}
}
