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

		private CountdownTimer life;

		// TODO: This should be false as the particles don't need to be collided but velocity and position is updated in the collision manager so it has to be true for now
		public override bool CheckCollisions => false;
		public override bool ReactToCollisions => false;


		public Particle(Vector2 position, string texture, string sprite, Color color, Angle direction, float speed, float lifeSpan, Direction facing = Direction.Down) : base(position)
		{
			Sprite = new Sprite.Animator(sprite, texture);

			ApplyDirecionalVelocity(direction, speed);

			life = new CountdownTimer(lifeSpan);

			drawColor = color;

			state.Facing = facing;

			Solid = false;
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			life.Update(delta);


			Position += PreviousVelocity * delta.Seconds;
			PreviousVelocity = Velocity;
			Velocity = Vector2.Zero;


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
				new Angle(Utils.RandomRange(effect.direction), AngleType.Revolution), Utils.RandomRange(effect.speed), Utils.RandomRange(effect.lifeSpan), (Direction)(Utils.random.Next(8)));

			return particle;
		}



	}
}
