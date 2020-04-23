using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Adventum.Core.IO;
using Adventum.Util;
using Adventum.Item;
using Adventum.States;
using Adventum.Core.Collision;
using Adventum.World;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Core.Resource;

namespace Adventum.Entities.Mobs
{
	public class PlayerEntity : Mob, ILightEmiter
	{
		public Input input;

		public int LightRadius => 75;
		public Color LightColor => Color.White;

		public override string[] DeathSound => new string[] { "playerDeath0" };

		public List<Collectable> collectedItems;


		public PlayerEntity(Vector2 position) : base(position, "playerBase", "HumanoidBase", maxHealth: 5)
		{
			input = new Input();

			alignment = Alignment.Good;

			collectedItems = new List<Collectable>();
		}


		protected override void InitalizeBehavior()
		{
			base.InitalizeBehavior();

			state.AddState("Idle").AddStateTrigger("Attack", () => input.KeyCheck(Control.Attack)).AddStateTrigger("Walk", () =>
			{
				return PreviousVelocity != Vector2.Zero;
			}
			).AddStateTrigger("Interact", () => input.KeyCheck(Control.Interact));

			Trigger<string> t = state.AddState("Idle").RecentTrigger();

			state.AddState("Walk").AttachTrigger(t).AddStateTrigger("Idle", () =>
			{
				return PreviousVelocity == Vector2.Zero;
			}
			).AddStateTrigger("Attack", () => input.KeyCheck(Control.Attack)).AddStateTrigger("Interact", () => input.KeyCheck(Control.Interact));
			//.AddStateTrigger("Dodge", () => input.KeyCheck(Control.Dodge));


			state.AddState("Sleep").AddEntranceTrigger(() => { Sprite.TryChangeAnimation("lie"); }).AddCountdownTrigger(() =>
			{ 
				Core.Audio.Play("getUp");
				state.SetActiveState("Idle");
			}, 2f);

			state.AddState("Dodge").AddEntranceTrigger(() => { Sprite.TryChangeAnimation("roll"); }).AddCountdownStateTrigger("Idle", 0.4f);
		}


		public override void Update(DeltaTime delta)
		{
			base.Update(delta);

			maxHitFrames = 0.6f;

			Vector2 movement = new Vector2();
			movement.X += input.CheckAxis(Control.Left, Control.Right);
			movement.Y += input.CheckAxis(Control.Up, Control.Down);

			Move(movement, MaxMovementSpeed, false);

			Vector2 centerPosition = Position;
			centerPosition.Y -= Sprite.Sprite.frameSize.Y / 2;
			state.Facing = Utils.AngleToDirection(Angle.FromVector(input.MouseWorldPosition - centerPosition));
		}

		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (collisionData.Other is Pickups.Pickup p)
			{
				p.Grab(this);
			}
		}


		public override void UseMain()
		{
			base.UseMain();
			//GameWorld.EntityManager.CreateEntity(new Interaction.Arrow(this, input.MouseWorldPosition - Position));
		}


		public override void Die()
		{
			base.Die();

			GameWorld.SpawnParticles(random.Next(20, 30), "blood", Position);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			/*if (state.ActiveState == "Attack")
			{
				spriteBatch.Draw(ResourceManager.GetTexture("Item/starterSword"), Position - new Vector2(0, 16), Color.White);
			}*/
		}


		public override void Damage(int damage, Angle direction)
		{
			base.Damage(damage, direction);

			Core.Audio.Play("playerHit0");
		}


		public override void UseSecondary()
		{
			base.UseSecondary();

			World.GameWorld.EntityManager.CreateEntity(new Interaction.Examine(this, Utils.DirectionToVector(state.Facing)));
		}


		public void CollectCollectable(Collectable collectable)
		{
			collectedItems.Add(collectable);
		}


		public Collectable CheckCollectables(string id)
		{
			foreach (Collectable c in collectedItems)
			{
				if (c.ID == id)
				{
					return c;
				}
			}

			return null;
		}


		public bool RemoveCollectable(string id)
		{
			Collectable c = CheckCollectables(id);
			if (c != null)
			{
				collectedItems.Remove(c);
			}
			return c != null;
		}
	}
}
