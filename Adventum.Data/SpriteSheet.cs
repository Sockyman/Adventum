using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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


    public class DirectionMap
    {
        public static DirectionMap standardDirectionMap = new DirectionMap();

        [ContentSerializer(FlattenContent = true)]
        public Dictionary<Direction, Point> map = new Dictionary<Direction, Point>();


        public DirectionMap()
        {
            foreach (Direction d in Enum.GetValues(typeof(Direction)))
                map[d] = Point.Zero;
        }
        public DirectionMap(Point down, Point left, Point up, Point right)
        {
            map[Direction.Down] = down;
            map[Direction.Left] = left;
            map[Direction.Up] = up;
            map[Direction.Right] = right;
        }
    }
}
