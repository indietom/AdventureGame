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
        internal static Shop shop = new Shop();

        Vector2 topBar;

        public Gui()
        {
            topBar = new Vector2(10, 10);
        }

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

            shop.Update();
            inventory.Update();
        }

        public void DrawPlayerUi(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(-5, -5), new Rectangle(490, 28, 75, 44), Color.White);

            spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(96-5, -5), new Rectangle(490, 28, 75, 44), Color.White);

            spriteBatch.DrawString(AssetManager.bigFont, "X - ", topBar + new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(AssetManager.bigFont, "Z - ", topBar + new Vector2(96, 0), Color.Black);
            foreach(Player p in Game1.gameObjects.Where(item => item is Player))
            {
                spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(32, 0), new Rectangle(p.equipedItems[0].IconSpirteCoords.X, p.equipedItems[0].IconSpirteCoords.Y, 32, 32), Color.White);
                spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(96 + 32, 0), new Rectangle(p.equipedItems[0].IconSpirteCoords.X, p.equipedItems[1].IconSpirteCoords.Y, 32, 32), Color.White);

                for (int i = 0; i < p.maxHealth; i++)
                {
                    spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(96 + 75 + 16 + i * 32, 0), new Rectangle(232, 67, 24, 24), new Color(100, 100, 100));
                }

                for (int i = 0; i < p.health; i++)
                {
                    spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2(96 + 75 + 16 + i * 32, 0), new Rectangle(232, 67, 24, 24), Color.White);
                }
                spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2((187 + 32 * p.maxHealth) + 16, 0), new Rectangle(207, 67, 24, 24), Color.White);
                spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2((187 + 32 * p.maxHealth) + 16 + 64+16, 0), new Rectangle(182, 67, 24, 24), Color.White);
                spriteBatch.Draw(AssetManager.spritesheet, topBar + new Vector2((187 + 32 * p.maxHealth) + 16, 32), new Rectangle(182, 92, 24, 24), Color.White);

                spriteBatch.DrawString(AssetManager.bigFont, "x" + p.amountOfArrows.ToString(), topBar + new Vector2((187 + 32 * p.maxHealth) + 40, 0), Color.White);
                spriteBatch.DrawString(AssetManager.bigFont, "x" + p.amountOfBombs.ToString(), topBar + new Vector2((187 + 32 * p.maxHealth) + 40, 32), Color.White);
                spriteBatch.DrawString(AssetManager.bigFont, "x" + p.amountOfKeys.ToString(), topBar + new Vector2((187 + 32 * p.maxHealth) + 40 + 64+16, 0), Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (inventory.active || shop.active)
            {
                spriteBatch.Draw(AssetManager.spritesheet, new Rectangle(0, 0, 320, 480), new Rectangle(1, 464, 130, 136), Color.Black);
            }
            inventory.Draw(spriteBatch);
            shop.Draw(spriteBatch);
            foreach(TextBox t in textBoxes)
            {
                t.Draw(spriteBatch);
            }
            DrawPlayerUi(spriteBatch);
        }
    }
}
