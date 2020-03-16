using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Adventum.Util;
using Adventum.Core;

namespace Adventum.UI
{
	class SimpleButton : Button
	{
		private static readonly Vector2 buttonScale = Utils.ScaleToScreen(new Vector2(100, 14));

		public SimpleButton(string text, EventCallback onClick, ButtonSkin buttonSkin = ButtonSkin.Default, int width = 100, int height = 14) : base(text, buttonSkin, Anchor.AutoCenter, Utils.ScaleToScreen(new Vector2(width, height)))
		{
			OnClick += onClick;
			OnMouseEnter += OnMouseHover;
			OnClick += OnMouseClick;
		}


		private void OnMouseHover(Entity entity)
		{
			Audio.Play("buttonHover");
		}


		private void OnMouseClick(Entity entity)
		{
			Audio.Play("buttonClick");
		}
	}
}
