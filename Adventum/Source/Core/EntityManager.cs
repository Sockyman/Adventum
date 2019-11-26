using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Source.Entities;
using Adventum.Source.Util;
using Adventum.Source.World;

namespace Adventum.Source.Core
{
    public class EntityManager
    {
        private List<Entity> entities;
        private List<Entity> entitiesToAdd;
        private List<Entity> entitiesToRemove;

        public EntityManager()
        {
            entities = new List<Entity>();
            entitiesToAdd = new List<Entity>();
            entitiesToRemove = new List<Entity>();
        }


        public Entity CreateEntity(Entity entity)
        {
            entitiesToAdd.Add(entity);
            GameWorld.collisionManager.AddCollider(entity);
            return entity;
        }


        public void RemoveEntity(Entity entity)
        {
            if (EntityExists(entity))
            {
                
            }
            entitiesToRemove.Add(entity);
            GameWorld.collisionManager.RemoveCollider(entity);
        }


        public bool EntityExists(Entity entity)
        {
            if (entity == null)
                return false;

            return entities.Contains(entity);
        }


        public void Update(DeltaTime delta)
        {
            foreach (Entity e in entities)
            {
                e.Update(delta);
            }

            Utils.SyncLists<Entity>(entities, entitiesToAdd, entitiesToRemove);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity e in entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
