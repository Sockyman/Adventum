using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Data
{
    public enum Direction
    {
        Down,
        Left,
        Up,
        Right
    }


    public class SpriteSheet
    {
        public string name = "";
        public string defaultAnimation = "";
        public Point frameSize = new Point();
        public Point origin = new Point();

        public Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    }


    public class Animation
    {
        public Point cellOfOrigin;
        public int frames;
        public int FPS;
    }
}
