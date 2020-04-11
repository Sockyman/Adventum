using System;
using Adventum.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using Adventum.Data;
using Adventum.Core.Collision;

namespace Adventum.Entities.Particles
{
	public class Particle : Entity
	{
		public override int Dampening => 1000;


		private CountdownTimer life;

		public override bool CheckCollisions => false;
		public override CollisionType CollisionType => CollisionType.NonSolid;

		private Bouncer bouncer;
		public override float Z
		{
			get { return bouncer.Z; }
		}


		public Particle(Vector2 position, string texture, string sprite, Color color, Angle direction, float speed, float lifeSpan, Direction facing = Direction.Down, float z = 0) : base(position)
		{
			Sprite = new Sprite.Animator(sprite, texture);

			ApplyDirecionalVelocity(direction, speed);

			life = new CountdownTimer(lifeSpan);

			bouncer = new Bouncer(z: z);

			drawColor = color;

			state.Facing = facing;
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			life.Update(delta);

			bouncer.Update(delta);

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
			Sprite.Draw(spriteBatch, Position, new Color(Color.Black, 30));
		}


		public static Particle GenerateFromEffect(ParticleEffect effect, Vector2 position)
		{

			Particle particle = new Particle(position, effect.texture, effect.sprite, Color.Lerp(effect.color, effect.mixColor, (float)Utils.random.NextDouble()),
				new Angle(Utils.RandomRange(effect.direction), AngleType.Revolution), Utils.RandomRange(effect.speed), Utils.RandomRange(effect.lifeSpan), (Direction)(Utils.random.Next(8)));

			return particle;
		}



	}
}
