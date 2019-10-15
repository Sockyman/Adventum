using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Adventum.Source.Core.Resource
{
    public static class ResourceManager
    {
        public static ContentManager content;

        private static string[] texturesToLoad = { };
        private static Dictionary<string, Texture2D> texturesLoaded;


        public static void LoadContent(ContentManager content)
        {
            ResourceManager.content = content;

            texturesLoaded = LoadContentType<Texture2D>("Texture", texturesToLoad);
        }


        private static Dictionary<string, T> LoadContentType<T>(string rootFolder, string[] filesToLoad)
        {
            Dictionary<string, T> resourcesLoaded = new Dictionary<string, T>();

            foreach (string name in filesToLoad)
            {
                LoadItem<T>(rootFolder, name, resourcesLoaded);
            }

            return resourcesLoaded;
        }

        private static void LoadItem<T>(string rootFolder, string name, Dictionary<string, T> resourceMap)
        {
            try
            {
                T resource = content.Load<T>(rootFolder + "/" + name);
                resourceMap[name] = resource;
            }
            catch (Exception)
            {
                Console.WriteLine("Resource: " + rootFolder + @"\" + name + ", failed to load.");
            }
        }



        public static Texture2D GetTexture(string name)
        {
            if (texturesLoaded.ContainsKey(name))
            {
                return texturesLoaded[name];
            }
            else
            {
                LoadItem<Texture2D>("Texture", name, texturesLoaded);
                if (texturesLoaded.ContainsKey(name))
                {
                    return texturesLoaded[name];
                }
                return new Texture2D(Main.graphics.GraphicsDevice, 32, 32);
            }
        }
    }
}
