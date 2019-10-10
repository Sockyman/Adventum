using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Source.Entities;
using Adventum.Source.Util;

namespace Adventum.Source.Core
{
    public class Player
    {
        public Entity charcter;
        private World world;


        public Player(World world)
        {
            this.world = world;
        }


        public void Update(DeltaTime delta)
        {

        }
    }
}
