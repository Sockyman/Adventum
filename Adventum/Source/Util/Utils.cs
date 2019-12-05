using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Adventum.Data;
using Adventum.Core.Resource;

namespace Adventum.Util
{
    public static class Utils
    {
        public static Vector2 Dampen(Vector2 vector, float dampening)
        {
            vector.X = Dampen(vector.X, dampening);
            vector.Y = Dampen(vector.Y, dampening);

            return vector;
        }
        public static float Dampen(float toDampen, float dampening)
        {
            int sign = Math.Sign(toDampen);
            toDampen = Math.Abs(toDampen);

            toDampen = Math.Max(0, toDampen - dampening);

            return toDampen * sign;
        }


        public static void SyncLists<T>(List<T> objects, List<T> toAdd, List<T> toRemove)
        {
            foreach (T t in toAdd)
            {
                objects.Add(t);
            }

            foreach (T t in toRemove)
            {
                objects.Remove(t);
            }

            toAdd.Clear();
            toRemove.Clear();
        }


        public static Texture2D GetTexturePart(Texture2D texture, Rectangle rectangle)
        {
            Color[] imageData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(imageData);
            return GetTexturePart(imageData, texture.Width, rectangle);
        }
        public static Texture2D GetTexturePart(Color[] texture, int textureWidth, Rectangle rectangle)
        {
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            for (int y = 0; y < rectangle.Height; y++)
                for (int x = 0; x < rectangle.Width; x++)
                    data[x + y * rectangle.Height] = texture[x + rectangle.X + (y + rectangle.Y) * textureWidth];

            Texture2D final = new Texture2D(Main.graphics.GraphicsDevice, rectangle.Width, rectangle.Height);
            final.SetData<Color>(data);
            return final;
        }


        public static Direction AngleToDirection(Angle angle)
        {
            Vector2 vector = angle.ToVector(1);
            vector.X = (float)Math.Round(vector.X);
            vector.Y = (float)Math.Round(vector.Y);

            if (vector.Y > 0)
                return Direction.Down;
            if (vector.Y < 0)
                return Direction.Up;
            if (vector.X > 0)
                return Direction.Right;
            return Direction.Left;
        }


        public static Vector2 DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.Right:
                    return new Vector2(1, 0);
                default:
                    return new Vector2(-1, 0);
            }
        }


        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(ResourceManager.GetTexture("pixel"), rectangle, color);
        }


        public static void DrawHealthBar(SpriteBatch spriteBatch, Vector2 center, Point size, int maxValue, int value, Color backgroundColor, Color foregroundColor)
        {
            Rectangle bar = new Rectangle((center - size.ToVector2() / 2).ToPoint(), size);
            DrawRectangle(spriteBatch, bar, backgroundColor);
            bar.Width = bar.Width * value / maxValue;
            DrawRectangle(spriteBatch, bar, foregroundColor);
        }
    }
}
