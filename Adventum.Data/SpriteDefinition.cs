using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Adventum.Data
{
    public class SpriteDefinition
    {
        public string name = "Hi";
        public Point frameSize;
        public Point origin;

        public List<AnimationDefinition> animations;
    }


    public class AnimationDefinition
    {
        public string name;
        public Point cellOfOrigin;
        public int frames;
        public int FPS;
    }
}
