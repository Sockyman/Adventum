using System;
using Microsoft.Xna.Framework;
using Adventum.Core.Collision;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Entities.Interaction;

namespace Adventum.Entities
{
	public class KeyDoor : Door, IInteractable
	{
		public virtual string KeyID => "base";


		public bool Locked { get; private set; } = true;

		public override CollisionType CollisionType
		{
			get
			{
				return Locked ? CollisionType.Immovable : CollisionType.NonSolid;
			}
		}

		public KeyDoor(Vector2 position, string level, bool cacheLevel) : base(position, level, cacheLevel)
		{
			visible = true;
		}

		public void UnLock()
		{
			Locked = false;
			visible = false;
		}

		public override void UseDoor(params Entity[] toAdd)
		{
			if (!Locked)
			{
				base.UseDoor(toAdd);
			}
		}

		public void OnExamine()
		{
			if (World.GameWorld.PlayerMob.RemoveCollectable("key_" + KeyID))
			{
				World.GameWorld.DisolveToParticles(this);
				UnLock();
				Core.Audio.Play("doorUnlock");
			}
			else
			{
				Main.gameState.screen.AddChild(new UI.TextBox("", "The door is locked"));
			}
		}


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}
