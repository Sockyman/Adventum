using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using Adventum.Item;
using Adventum.Entities.Mobs;
using Adventum.Core.Collision;
using Adventum.World;

namespace Adventum.Entities.Pickups
{
	public class CollectablePickup : Pickup
	{
		protected const int timeToFollow = 10;

		public override float MaxMovementSpeed => 2500;

		Collectable collectable;

		CountdownTimer timer;

		protected bool goToPlayer = false;

		public override CollisionType CollisionType
		{
			get
			{
				return goToPlayer ? CollisionType.NonSolid : CollisionType.Solid;
			}
		}

		public override string Sound => "pickupKey";

		public CollectablePickup(Vector2 position/*, Collectable collectable*/) : base(position, "key", "KeyDoor")
		{
			this.collectable = new KeyCollectable(); //collectable;
			Sprite = collectable.sprite;

			timer = new CountdownTimer(timeToFollow);
			timer.Completed += EnableFollow;
		}

		private void EnableFollow(object sender, EventArgs e)
		{
			goToPlayer = true;
		}

		public override void Update(Util.DeltaTime delta)
		{
			base.Update(delta);
			timer.Update(delta);

			if (goToPlayer && GameWorld.PlayerExists)
			{
				ApplyDirecionalVelocity(Angle.FromVector(GameWorld.PlayerMob.Position - Position), MaxMovementSpeed * delta.Seconds);
			}
		}


		protected override void DoGrab(Mob mob)
		{
			base.DoGrab(mob);

			if (mob is PlayerEntity p)
			{
				p.CollectCollectable(collectable);
			}
		}
	}
}
