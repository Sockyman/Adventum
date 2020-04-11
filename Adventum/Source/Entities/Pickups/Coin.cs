using System;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Pickups
{
	public class Coin : Pickup
	{
		public override string Sound => "pickupCoin";

		public Coin(Vector2 position) : base(position, "coin", "Coin")
		{

		}

		protected override void DoGrab(Mobs.Mob mob)
		{
			base.DoGrab(mob);

			mob.Gold++;
		}
	}
}
