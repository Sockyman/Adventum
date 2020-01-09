using System;
using System.Collections.Generic;
using Adventum.Core.Collision;
using Microsoft.Xna.Framework;

namespace Adventum.Entities.Interaction
{
    public class Examine : Interact
    {
        public Examine(Entity parent, Vector2 direction) : base(parent, new Point(16), direction, 0.1f, 500f, true)
        {

        }


        public override void OnInteract(Entity entity)
        {
            base.OnInteract(entity);

            if (entity is IInteractable)
            {
                ((IInteractable)entity).OnExamine();

                Destroy();
            }
        }
    }
}
