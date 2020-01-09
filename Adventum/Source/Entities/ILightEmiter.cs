using System;
using Microsoft.Xna.Framework;

namespace Adventum.Entities
{
    interface ILightEmiter
    {
        Vector2 Position { get; set; }

        int LightRadius { get; }

        Color LightColor { get; }
    }
}
