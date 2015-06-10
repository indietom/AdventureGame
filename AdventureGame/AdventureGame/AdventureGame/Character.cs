using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Character : GameObject
    {
        public string name;
        public string text;

        public Character() { }

        public Character(Vector2 pos2, string name2, string text2, Point spriteCoords2)
        {
            pos = pos2;

            name = name2;
            text = text2;

            SetSize(32);
            spriteCoords = spriteCoords2;
            scale = 1;
            color = Color.White;
        }

        public void PopTextBox()
        {
            Gui.textBoxes.Add(new TextBox(new Vector2(150, 150), name, text, 4));
        }

        public override void Update()
        {
            depth = ZOrder();
            base.Update();
        }
    }
}
