﻿using System;
using Microsoft.Xna.Framework;
using Adventum.Data;
using Adventum.Source.States;
using Adventum.Source.Sprite;
using MonoGame.Extended;
using Adventum.Source.Core.Collision;
using Adventum.Source.Util;
using Adventum.Source.World;

namespace Adventum.Source.Entities.Mobs
{
    public class Mob : Entity
    {
        public int MaxHealth { get; private set; }
        public int Health { get; set; }



        public Mob(Vector2 position) : base(position)
        {
            Sprite = new Animator("HumanoidBase", "humanBase");
            SetBounds(new Point(16));
        }


        protected override void InitalizeBehavior()
        {
            base.InitalizeBehavior();

            state.AddState(EState.Idle).AddEntranceTrigger(() => Sprite.TryChangeAnimation("idle"));

            state.AddState(EState.Walk).AddEntranceTrigger(() => Sprite.TryChangeAnimation("walk"));

            state.AddState(EState.Attack).AddCountdownStateTrigger(EState.Idle, 0.25f);
        }


        public override void Update(DeltaTime delta)
        {
            base.Update(delta);
            if (Health < 0)
            {
                GameWorld.entityManager.RemoveEntity(this);
            }
        }


        public override void Move(Vector2 angle, bool changeDirection = false)
        {
            base.Move(angle, changeDirection);
            if (changeDirection && angle != Vector2.Zero)
            {

            }
                
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);
        }


        public void Hurt(int damage, Angle direction)
        {
            Health -= damage;
        }
    }
}
