﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Data;
using Adventum.Source.States;
using Adventum.Source.Sprite;
using MonoGame.Extended;
using Adventum.Source.Core;
using Adventum.Source.Core.Collision;
using Adventum.Source.Util;
using Adventum.Source.World;
using Adventum.Source.Core.Resource;

namespace Adventum.Source.Entities.Mobs
{
    public class Mob : Entity
    {
        public int MaxHealth { get; private set; } = 2;
        public int Health { get; set; }
        public float HitFrames { get; private set; }


        public Mob(Vector2 position) : base(position)
        {
            Sprite = new Animator("HumanoidBase", "humanBase");
            SetBounds(new Point(16));

            Health = MaxHealth;
        }

        public Mob(Vector2 position, string texture, string spriteSheet, int boundingSize = 16, int maxHealth = 10) : base(position)
        {
            Sprite = new Animator(spriteSheet, texture);
            SetBounds(new Point(boundingSize));

            MaxHealth = maxHealth;
            Health = MaxHealth;
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
            if (Health < 1)
            {
                Die();
            }

            HitFrames -= delta.Seconds;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HitFrames < 0 || random.Next(2) != 0)
            {
                base.Draw(spriteBatch);
            }
        }


        public override void Move(Vector2 angle, bool changeDirection = false)
        {
            base.Move(angle, changeDirection);
        }

        public override void OnCollision(CollisionData collisionData)
        {
            base.OnCollision(collisionData);
        }


        public void Hurt(int damage, Angle direction)
        {
            if (HitFrames <= 0)
            {
                Health -= damage;

                HitFrames = 0.6f;

                direction.Revolutions += random.Next(-1, 1) / 10;
                ApplyDirecionalVelocity(direction, 500);

                Audio.Play("enemyHit0", 0.4f);
            }
        }


        public void Die()
        {
            Audio.Play("enemyDeath0", 0.4f);
            GameWorld.entityManager.RemoveEntity(this);
        }
    }
}