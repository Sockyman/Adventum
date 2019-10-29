using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Adventum.Data
{
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
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
        public Point cellOfOrigin = new Point();
        public int frames = 1;
        public int FPS = 0;
        public DirectionMap directionMap = new DirectionMap();
    }


    public class DirectionMap
    {
        public static DirectionMap standardMobMap = new DirectionMap(new Point(), new Point(0, 1), new Point(0, 2), new Point(0, 3));

        [ContentSerializer(FlattenContent = true)]
        public Point[] map = new Point[Enum.GetValues(typeof(Direction)).Length];


        public DirectionMap()
        {
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Point();
            }
        }
        public DirectionMap(Point down, Point left, Point up, Point right)
        {
            SetDirection(Direction.Down, down);
            SetDirection(Direction.Left, left);
            SetDirection(Direction.Up, up);
            SetDirection(Direction.Right, right);
        }


        public Point GetDirection(Direction direction)
        {
            return map[(int)direction];
        }
        public void SetDirection(Direction direction, Point position)
        {
            map[(int)direction] = position;
        }
    }
}
