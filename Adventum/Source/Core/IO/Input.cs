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
            }
        }


        public Input()
        {
            keyboardState = new KeyboardState();
            oldKeyboardState = new KeyboardState();

            mouseState = new MouseState();
            oldMouseState = new MouseState();
        }


        public void Update()
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            Main.DebugAdd(MousePosition.ToPoint().ToString(), "MousePosition:");
            Main.DebugAdd(MouseWorldPosition.ToPoint().ToString(), "MouseWorldPosition:");
        }



        public bool KeyCheck(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public bool KeyCheck(MouseButton key)
        {
            return GetMouseButtonState(key) == ButtonState.Pressed;
        }

        public ButtonState GetMouseButtonState(MouseButton key)
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


        public int CheckAxis(Keys negativeKey, Keys positiveKey)
        {
            return (KeyCheck(negativeKey) ? -1 : 0) + (KeyCheck(positiveKey) ? 1 : 0);
        }
    }
}
