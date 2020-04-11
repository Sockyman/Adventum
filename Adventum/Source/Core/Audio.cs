using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Adventum.Core.Resource;
using MonoGame.Extended;

namespace Adventum.Core
{
    public static class Audio
    {
        public static Random random = new Random();

        public static void Play(string soundName, float pitchOffset = 0, bool randomOffset = false)
        {
            if (soundName != "" && Properties.Settings.Default.enableAudio)
            {
                float offset = MathHelper.Clamp(randomOffset ? random.NextSingle(-pitchOffset, pitchOffset) : pitchOffset, -1, 1);
                
                ResourceManager.GetSound(soundName).Play(1, offset, 0);
            }
        }

        public static void Play(float pitchOffset, bool randomOffset, params string[] soundNames)
        {
            if (soundNames.Length > 0)
            {
                string sound = soundNames[random.Next(0, soundNames.Length)];
                Play(sound, pitchOffset, randomOffset);
            }
        }
    }
}
