using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdventureGame
{
    class Gui
    {
        internal static List<TextBox> textBoxes = new List<TextBox>();
        internal static Inventory inventory = new Inventory();

        public void Update()
        {
            foreach (TextBox t in textBoxes)
            {
                t.Update();
            }

            for (int i = textBoxes.Count - 1; i >= 0; i--)
            {
                if (textBoxes[i].destroy) textBoxes.RemoveAt(i);
            }
            inventory.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            inventory.Draw(spriteBatch);
            foreach(TextBox t in textBoxes)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
