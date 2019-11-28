using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Adventum.Core.Resource;

namespace Adventum.Core
{
    public static class Audio
    {
        public static Random random = new Random();

        public static void Play(string soundName, float pitchOffset = 0)
        {
            ResourceManager.GetSound(soundName).Play(1, random.Next((int)(100 * -pitchOffset), (int)(100 * pitchOffset)) / 100, 0);
        }
    }
}
