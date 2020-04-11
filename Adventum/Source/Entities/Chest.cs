using System;
using Microsoft.Xna.Framework;
using Adventum.Core.Collision;
using Adventum.Entities.Pickups;
using Adventum.Data;

namespace Adventum.Entities
{
	public class Chest : Entity
	{
		public bool Open
		{
			get
			{
				return open;
			}
			set
			{
				if (value)
					OpenChest();
			}
		}
		private bool open = false;

		public string lootTable;

		public override CollisionType CollisionType => CollisionType.Immovable;

		public Chest(Vector2 position, string lootTable = "Chest") : base(position)
		{
			Sprite = new Sprite.Animator("Chest", "chest");

			this.lootTable = lootTable;

			SetBounds(new Point(16));
		}

		public override void OnCollision(CollisionData collisionData)
		{
			base.OnCollision(collisionData);

			if (collisionData.Other is Interaction.Attack a && a.parent is Mobs.Mob e && e.alignment == Alignment.Good)
			{
				OpenChest();
			}
		}

		public void OpenChest()
		{
			if (!Open)
			{
				Core.Audio.Play("openChest");

				open = true;

				Sprite.TryChangeAnimation("open");

				Vector2 spawnPosition = Position;

				spawnPosition.Y += BoundingBox.Height / 2 + 8;

				World.GameWorld.RollLootTable(spawnPosition, lootTable);
			}
		}
	}
}
