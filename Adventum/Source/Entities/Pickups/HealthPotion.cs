using System;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Pickups
{
	public class HealthPotion : Pickup
	{
		public override string Sound => "pickupPotion";

		public HealthPotion(Vector2 position) : base(position, "healthPotion", "Coin")
		{

		}


		protected override void DoGrab(Mobs.Mob mob)
		{
			if (mob.Health < mob.MaxHealth)
			{
				base.DoGrab(mob);

				mob.Health++;
			}
		}
	}
}
