using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Adventum.Source.Entities;
using Adventum.Source.Util;

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
            return entity;
        }


        public void RemoveEntity(Entity entity)
        {
            entitiesToRemove.Add(entity);
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

            foreach (Entity e in entitiesToAdd)
            {
                entities.Add(e);
            }

            foreach (Entity e in entitiesToRemove)
            {
                entities.Remove(e);
            }

            entitiesToAdd.Clear();
            entitiesToRemove.Clear();
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
