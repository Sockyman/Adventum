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
        public static float Wrap(float min, float max, float value)
        {
            return (float)(value - (max - min) * Math.Floor(value / (max - min)));
        }
        public static int Wrap(int min, int max, int value)
        {
            return value - (max - min) * (int)Math.Floor((float)value / ((float)max - (float)min));
        }


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

            for (int x = 0; x < rectangle.Width; x++)
                for (int y = 0; y < rectangle.Height; y++)
                {
                    Color c = texture[x + rectangle.X + (y + rectangle.Y) * textureWidth];
                    data[x + y * rectangle.Width] = c;
                }
            Texture2D final = new Texture2D(Main.graphics.GraphicsDevice, rectangle.Width, rectangle.Height);
            final.SetData<Color>(data);
            return final;
        }



		public static Direction VectorToDirection(Vector2 angle, bool cardinalLock = false)
		{
			return AngleToDirection(Angle.FromVector(angle), cardinalLock);
		}
		public static Direction AngleToDirection(Angle angle, bool cardinalLock = false)
        {
            Vector2 vec = angle.ToUnitVector();
            float rad = (float)Math.Atan2(vec.X, -vec.Y);

            int directions = Enum.GetNames(typeof(Direction)).Length;


            float j = (float)(rad / Math.PI / 2 * directions);
            int i = (int)Math.Round(j) + directions / 2;
            int direction = Wrap(0, directions, i);

            if (cardinalLock)
            {
                direction = (direction + 1) / 2;
            }

            return (Direction)direction;
        }


        public static Vector2 DirectionToVector(Direction direction)
        {
            float i = (float)direction;

            float rad = (float)(i / Enum.GetNames(typeof(Direction)).Length * Math.PI * 2) + (float)Math.PI;
            return new Vector2((float)Math.Sin(rad), -(float)Math.Cos(rad));
        }


        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, float layerDepth = 0f)
        {
            spriteBatch.Draw(ResourceManager.GetTexture("pixel"), destinationRectangle: rectangle, color: color, layerDepth: layerDepth);
        }


        public static void DrawHealthBar(SpriteBatch spriteBatch, Vector2 center, Point size, int maxValue, int value, Color backgroundColor, Color foregroundColor, float layerDepth = 0f)
        {
            Rectangle bar = new Rectangle((center - size.ToVector2() / 2).ToPoint(), size);
            DrawRectangle(spriteBatch, bar, backgroundColor, layerDepth - 0.00001f);
            bar.Width = bar.Width * value / maxValue;
            DrawRectangle(spriteBatch, bar, foregroundColor, layerDepth);
        }


        public static Vector2 ScaleToScreen(Vector2 toScale)
        {
            Vector2 ratio = toScale / Main.windowSize.ToVector2();

            return ratio * new Vector2(Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight);
        }
    }
}
