using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Adventum.Core.IO
{
	public class Input
	{
		private KeyboardState keyboardState;
		private KeyboardState oldKeyboardState;

		private MouseState mouseState;
		private MouseState oldMouseState;

		private Dictionary<Control, int> controls;

		public Vector2 MousePosition
		{
			get
			{
				Vector2 mousePosition = mouseState.Position.ToVector2();
				mousePosition.X = mousePosition.X / Main.graphics.PreferredBackBufferWidth * Main.renderTarget.Width;
				mousePosition.Y = mousePosition.Y / Main.graphics.PreferredBackBufferHeight * Main.renderTarget.Height;
				return mousePosition;
			}
		}

		public Vector2 MouseWorldPosition
		{
			get
			{
				return Main.Camera.ScreenToWorld(mouseState.Position.ToVector2());
				//return Main.Camera.Position + mouseState.Position.ToVector2();
			}
		}
		public Vector2 MouseWorldPosition2
		{
			get
			{
				//return Main.Camera.ScreenToWorld(mouseState.Position.ToVector2());
				return Main.Camera.Position + mouseState.Position.ToVector2();
			}
		}


		public Input()
		{
			keyboardState = new KeyboardState();
			oldKeyboardState = new KeyboardState();

			mouseState = new MouseState();
			oldMouseState = new MouseState();

			BindKeys();
		}


		private void BindKeys()
		{
			controls = new Dictionary<Control, int>();

			AddControl(Control.Up, Keys.W);
			AddControl(Control.Down, Keys.S);
			AddControl(Control.Left, Keys.A);
			AddControl(Control.Right, Keys.D);
			AddControl(Control.Attack, MouseButton.Left);
			AddControl(Control.Interact, MouseButton.Right);
		}


		public void Update(bool disable)
		{
			oldMouseState = mouseState;
			oldKeyboardState = keyboardState;

			if (disable)
			{
				keyboardState = new KeyboardState();
				mouseState = new MouseState(oldMouseState.X, oldMouseState.Y, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);

				return;
			}

			keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();
		}



		public bool KeyCheck(Keys key)
		{
			return keyboardState.IsKeyDown(key);
		}

		public bool KeyCheck(MouseButton key)
		{
			return GetMouseButtonState(key) == ButtonState.Pressed;
		}

		public bool KeyCheck(Control control)
		{
			int keyIndex = controls[control];
			if (keyIndex < 0)
			{
				return KeyCheck((MouseButton)keyIndex);
			}
			return KeyCheck((Keys)keyIndex);
		}


		public ButtonState GetMouseButtonState(MouseButton key)
		{
			return GetMouseButtonState(key, mouseState);
		}
		public ButtonState GetMouseButtonState(MouseButton key, MouseState mouseState)
		{
			switch (key)
			{
				case MouseButton.Left:
					return mouseState.LeftButton;
				case MouseButton.Right:
					return mouseState.RightButton;
				case MouseButton.Middle:
					return mouseState.MiddleButton;
				default:
					return new ButtonState();
			}
		}


		public bool KeyCheckPressed(Keys key)
		{
			return KeyCheck(key) && !oldKeyboardState.IsKeyDown(key);
		}

		public bool KeyCheckPressed(MouseButton key)
		{
			return KeyCheck(key) && !(GetMouseButtonState(key, oldMouseState) == ButtonState.Pressed);
		}

		public bool KeyCheckPressed(Control control)
		{
			int keyIndex = controls[control];
			if (keyIndex < 0)
			{
				return KeyCheckPressed((MouseButton)keyIndex);
			}
			return KeyCheckPressed((Keys)keyIndex);
		}


		public int CheckAxis(Keys negativeKey, Keys positiveKey)
		{
			return (KeyCheck(negativeKey) ? -1 : 0) + (KeyCheck(positiveKey) ? 1 : 0);
		}

		public int CheckAxis(Control negative, Control positive)
		{
			return (KeyCheck(negative) ? -1 : 0) + (KeyCheck(positive) ? 1 : 0);
		}


		public void AddControl(Control control, Keys key)
		{
			controls.Add(control, (int)key);
		}

		public void AddControl(Control control, MouseButton button)
		{
			controls.Add(control, (int)button);
		}
	}
}
