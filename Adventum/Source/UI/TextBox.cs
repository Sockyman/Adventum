using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Adventum.UI
{
    public class TextBox : Panel
    {
        private Header head;


        public Color TitleColor
        {
            get
            {
                return head.FillColor;
            }
            set
            {
                head.FillColor = value;
            }
        }


        public TextBox(string title, string text, PanelSkin skin = PanelSkin.Default) : base(new Vector2(500, 200), skin, Anchor.BottomCenter)
        {
            if (title != "")
            {
                head = new Header(title);
                AddChild(head);

                AddChild(new HorizontalLine());
            }
            
            Paragraph p = new Paragraph(text);
            AddChild(p);
        }
    }
}
