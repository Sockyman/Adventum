using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Adventum.Data;

namespace Adventum.Source.Util
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

            Color[] data = new Color[rectangle.Width * rectangle.Height];
            int i = 0;

            for (int y = 0; y < rectangle.Height; y++)
                for (int x = 0; x < rectangle.Width; x++)
                    data[x + y * rectangle.Height] = imageData[x + rectangle.X + (y + rectangle.Y) * texture.Width];

            Texture2D final = new Texture2D(Main.graphics.GraphicsDevice, rectangle.Width, rectangle.Height);
            final.SetData<Color>(data);
            return final;
        }


        public static Direction AngleToDirection(Angle angle)
        {
            angle.Degrees += 90;
            return (Direction)(int)((angle.Revolutions * Enum.GetValues(typeof(Direction)).Length) - 1);
        }
    }
}
