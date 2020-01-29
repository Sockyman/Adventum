using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Data
{
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
        [ContentSerializer(Optional = true)]
        public DirectionMap directionMap = new DirectionMap();
        [ContentSerializer(Optional = true)]
        public int[] flipMap = new int[Enum.GetValues(typeof(Direction)).Length];
    }


    public class DirectionMap
    {
        public static DirectionMap standardMobMap = DirectionMap.Four(new Point(), new Point(0, 1), new Point(0, 2), new Point(0, 3));

        [ContentSerializer(FlattenContent = true)]
        public Point[] map = new Point[Enum.GetValues(typeof(Direction)).Length];


        public DirectionMap()
        {
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Point();
            }
        }
        public DirectionMap(Point down, Point downLeft, Point left, Point upLeft, Point up, Point upRight, Point right, Point downRight)
        {
            SetDirection(Direction.Down, down);
            SetDirection(Direction.Left, left);
            SetDirection(Direction.Up, up);
            SetDirection(Direction.Right, right);
            SetDirection(Direction.DownRight, downRight);
            SetDirection(Direction.DownLeft, downLeft);
            SetDirection(Direction.UpLeft, upLeft);
            SetDirection(Direction.UpRight, upRight);
        }

        public static DirectionMap Four(Point down, Point left, Point up, Point right)
        {
            return new DirectionMap(down, down, left, left, up, right, right, down);
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
