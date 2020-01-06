using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Adventum.Data.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            //DirectionMap data = DirectionMap.standardMobMap;


            SerializeSpriteSheet();
        }


        static void Serialize<T>(T data)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true
            };

            using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            {
                Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, data, null);
            }
        }

        static void SerializeSpriteSheet()
        {
            SpriteSheet data = new SpriteSheet
            {
                name = "Tree",
                origin = new Point(32, 128),
                frameSize = new Point(64, 128),
                animations = new Dictionary<string, Animation>(),
                defaultAnimation = "base"
            };

            Animation ani = new Animation
            {
                cellOfOrigin = new Point(0),
                frames = 1,
                FPS = 0,
                directionMap = new DirectionMap()
            };

            data.animations["base"] = ani;

            Serialize(data);
        }
    }
}
