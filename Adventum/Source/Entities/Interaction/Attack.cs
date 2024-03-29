﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Adventum.Sprite;
using Adventum.Entities.Mobs;
using Adventum.Util;
using MonoGame.Extended;

namespace Adventum.Entities.Interaction
{
    public class Attack : Interact
    {
        public int damage = 1;


        public Attack(Entity parent, Point size, Vector2 direction, float lifeSpan, float speed, bool lockToParent, string textureName, int damage) 
            : base(parent, size, direction, lifeSpan, speed, lockToParent)
        {
            Sprite = new Animator("AttackSwish", textureName);
            this.damage = damage;
        }


        public override void OnInteract(Entity entity)
        {
            base.OnInteract(entity);

            if (entity is Mob && ((Mob)entity).HitFrames < 1 && (parent is Mob ? ((Mob)parent).alignment != ((Mob)entity).alignment : true))
            {
                ((Mob)entity).TryHurt(damage, Angle.FromVector(Utils.DirectionToVector(state.Facing)));
            }
        }
    }
}
