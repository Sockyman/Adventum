using System;
using Microsoft.Xna.Framework;

namespace Adventum.Source.Util
{
    public class DeltaTime : GameTime
    {
        public const int standardFps = 60;


        public float Seconds
        {
            get
            {
                return (float)ElapsedGameTime.TotalSeconds;
            }
        }

        public float Frames
        {
            get
            {
                return Seconds * standardFps;
            }
        }



        public DeltaTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime) : base(totalGameTime, elapsedGameTime)
        {

        }
    }
}
