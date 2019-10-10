using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
    }
}
