using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Core.IO;
using Adventum.Entities;
using Adventum.Entities.Mobs;
using Adventum.Util;
using Adventum.World;

namespace Adventum.Core
{
    public class Player
    {
        public PlayerEntity player;
        private GameWorld world;
        private Input input;


        public Player(GameWorld world, Input input)
        {
            this.world = world;
            this.input = input;
        }


        public void Update(DeltaTime delta)
        {
            
        }
    }
}
