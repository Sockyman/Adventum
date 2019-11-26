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
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            {
                Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, data, null);
            }
        }

        static void SerializeSpriteSheet()
        {
            SpriteSheet data = new SpriteSheet();


            data.name = "AttackSwish";
            data.origin = new Point(16, 32);
            data.frameSize = new Point(32);
            data.animations = new Dictionary<string, Animation>();
            data.defaultAnimation = "base";

            Animation ani = new Animation();
            ani.cellOfOrigin = new Point(0);
            ani.frames = 1;
            ani.FPS = 15;
            ani.directionMap = DirectionMap.standardMobMap;

            data.animations["base"] = ani;

            Serialize(data);
        }
    }
}
