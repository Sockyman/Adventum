using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Util;
using Adventum.Data;
using Adventum.World;
using MonoGame.Extended;
using Adventum.Entities.Interaction;
using Adventum.Core.Collision;

namespace Adventum.Entities.Mobs
{
	public class SlimeBoss : Boss
	{
		private const float slimeChance = 0.5f;

		public override string Loot => "SimpleBoss";
		public override float MaxMovementSpeed => base.MaxMovementSpeed;
		public Bouncer bouncer;
		public override float Z
		{
			get { return bouncer.Z; }
		}

		public override CollisionType CollisionType
		{
			get
			{
				return state.ActiveState == "Jump" ? CollisionType.NoPush : CollisionType.Solid;
			}
		}


		protected bool moving = false;
		protected Angle angle;


		public SlimeBoss(Vector2 position) : base(position, "slimeBoss", "SlimeBoss", new Point(64, 32), 25)
		{
			bouncer = new Bouncer(0, 1000, 0, 0);
		}


		protected override void InitalizeBehavior()
		{
			base.InitalizeBehavior();

			state.AddState("Idle").AddCountdownStateTrigger("Jump", 1f).AddEntranceTrigger(() =>
			{
				Sprite.TryChangeAnimation("deCompress");

				Core.Audio.Play("heavyObjectFall");

				if (GameWorld.PlayerExists && Utils.Odds(slimeChance) && Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 256)
				{
					for (int i = random.Next(1, 3); i > 0; i--)
					{
						GameWorld.EntityManager.CreateEntity(new Slime(Position + new Vector2(random.Next(-2, 3), random.Next(1, 3))));
					}
				}
			}).AddCountdownTrigger(() =>
			{
				Sprite.TryChangeAnimation("idle");
			}, 0.24f);

			state.AddState("Jump").AddEntranceTrigger(() =>
			{
				Sprite.TryChangeAnimation("jump");
				bouncer.ZVelocity = 500;

				moving = true;
				
				if (GameWorld.PlayerExists && Utils.Odds(0.8))
				{
					angle = Angle.FromVector(GameWorld.PlayerMob.Position - Position);
				}
				else
				{
					angle = new Angle(random.Next(0, 8) / 7, AngleType.Revolution);
				}
			}).AddStateTrigger("Idle", () =>
			{
				return state.clock.CurrentTime.TotalSeconds > 1 && bouncer.Z <= 0;
			}).AddUpdateTrigger(() =>
			{
				if (moving)
					Move(angle.ToUnitVector(), MaxMovementSpeed);

				if (GameWorld.PlayerExists && (Vector2.Distance(Position, GameWorld.PlayerMob.Position) < 4))
				{
					moving = false;
				}
			});
		}


		public override void TryHurt(int damage, Angle direction)
		{
			if (bouncer.Z <= 0)
			{
				base.TryHurt(damage, direction);
			}
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			bouncer.Update(delta);
		}


		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (bouncer.Z == 0 && collisionData.Other is PlayerEntity player)
			{
				MeleeAttack attack = new MeleeAttack(this, state.Facing, 1);
				attack.visible = false;

				GameWorld.EntityManager.CreateEntity(attack);
			}
		}


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.Draw(Core.Resource.ResourceManager.GetTexture("shadowMega"), Position - Sprite.Sprite.origin.ToVector2(), new Color(Color.Black, 70));
		}
	}
}
