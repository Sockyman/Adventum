using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Source.Core.IO;
using Adventum.Source.Entities;
using Adventum.Source.Entities.Mobs;
using Adventum.Source.Util;
using Adventum.Source.World;

namespace Adventum.Source.Core
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
