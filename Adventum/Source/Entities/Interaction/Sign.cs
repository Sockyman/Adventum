using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GeonBit.UI;
using Adventum.UI;

namespace Adventum.Entities.Interaction
{
    public class Sign : Entity, IInteractable
    {
        public string title;
        public string text;

        public Sign(Vector2 position, string title, string text) : base(position)
        {
            visible = false;

            Sprite = new Sprite.Animator("Furniture2", "chair");

			Solid = false;

            this.text = text;
            this.title = title;
        }


        public virtual void OnExamine()
        {
			Show();
        }

		public void Show()
		{
			World.GameWorld.gameplayScreen.AddChild(new TextBox(title, text));
		}
    }
}
