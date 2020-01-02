﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.States;
using Adventum.Util;
using Adventum.Sprite;
using Adventum.Data;
using Adventum.World;
using MonoGame.Extended;

namespace Adventum.Entities.Mobs
{
    class Enemy : Mob
    {
        public Enemy(Vector2 position, string texture, string spriteSheet = "HumanoidBase", int boundingSize = 16, int maxHealth = 10) : base(position, texture, spriteSheet, boundingSize, maxHealth)
        {
            
            
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Walk).AddUpdateTrigger(() =>
            {
                Move(Utils.DirectionToVector(state.Facing), MaxMovementSpeed / 2, true);
            });
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
        }
    }
}
