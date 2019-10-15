﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Adventum.Source.Core.IO;
using Adventum.Source.Entities;
using Adventum.Source.Util;

namespace Adventum.Source.Core
{
    public class Player
    {
        public Entity player;
        private World world;
        private Input input;


        public Player(World world, Input input)
        {
            this.world = world;
            this.input = input;
        }


        public void Update(DeltaTime delta)
        {
            player.jitter = false;

            Vector2 movement = new Vector2();
            movement.X += input.CheckAxis(Keys.A, Keys.D);
            movement.Y += input.CheckAxis(Keys.W, Keys.S);

            player.Move(movement, Entity.MaxMovementSpeed * delta.Seconds);
        }
    }
}
