using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Adventum.Source.Core.IO
{
    public class Input
    {
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;


        public Input()
        {
            keyboardState = new KeyboardState();
            oldKeyboardState = new KeyboardState();
        }


        public void Update()
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }



        public bool KeyCheck(Keys key)
        {
            return keyboardState.IsKeyDown(key);
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
