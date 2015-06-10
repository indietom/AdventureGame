using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Shop
    {
        ShopItem[,] shopItems = new ShopItem[4,4];

        public bool active;

        public short delay;

        Vector2 pos;

        Point currentSelect;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Shop()
        {
            pos = new Vector2(16, 70);
            shopItems[0, 0] = new ShopItem(new EquipableItem("test.txt"), 0);
        }

        public void Input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Left) && !prevKeyboard.IsKeyDown(Keys.Left) && currentSelect.X != 0)
            {
                currentSelect.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Right) && !prevKeyboard.IsKeyDown(Keys.Right) && currentSelect.X != shopItems.GetLength(0) - 1)
            {
                currentSelect.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.Up) && !prevKeyboard.IsKeyDown(Keys.Up) && currentSelect.Y != 0)
            {
                currentSelect.Y -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Down) && !prevKeyboard.IsKeyDown(Keys.Down) && currentSelect.Y != shopItems.GetLength(1) - 1)
            {
                currentSelect.Y += 1;
            }

            if (keyboard.IsKeyDown(Keys.X) && !prevKeyboard.IsKeyDown(Keys.X) && !Gui.inventory.full && shopItems[currentSelect.X, currentSelect.Y] != null)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    if (p.money >= shopItems[currentSelect.X, currentSelect.Y].cost)
                    {
                        p.money -= shopItems[currentSelect.X, currentSelect.Y].cost;
                        Gui.inventory.AddItem(shopItems[currentSelect.X, currentSelect.Y].item);
                    }
                }
            }

            if (keyboard.IsKeyDown(Keys.Q) && !prevKeyboard.IsKeyDown(Keys.Q))
            {
                active = false;
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    p.inputDelay = 1;
                    p.inputActive = true;
                }
            }
        }

        public void Update()
        {
            if (delay >= 1) delay += 1;
            if (delay >= 8) delay = 0;

            if (active)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    p.inputActive = false;
                }

                if(delay <= 0) Input();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2((currentSelect.X * 64) - 16, (currentSelect.Y * 64) - 16), new Rectangle(364, 67, 32, 32), Color.White);

                for (int y = 0; y < shopItems.GetLength(1); y++)
                {
                    for (int x = 0; x < shopItems.GetLength(0); x++)
                    {
                        if (shopItems[x, y] != null)
                        {
                            spriteBatch.Draw(AssetManager.spritesheet, pos + new Vector2(x * 64, y * 16), new Rectangle(shopItems[x, y].item.IconSpirteCoords.X, shopItems[x, y].item.IconSpirteCoords.Y, 32, 32), shopItems[x, y].item.iconColor, 0, new Vector2(16, 16), shopItems[x, y].item.iconScale, SpriteEffects.None, 0);
                        }
                    }
                }
                if (shopItems[currentSelect.X, currentSelect.Y] != null)
                {
                    spriteBatch.DrawString(AssetManager.bigFont, shopItems[currentSelect.X, currentSelect.Y].item.Name, pos + new Vector2(0, 270), Color.White);
                    spriteBatch.DrawString(AssetManager.smallFont, shopItems[currentSelect.X, currentSelect.Y].item.Description + "\n" + "Cost: " + shopItems[currentSelect.X, currentSelect.Y].cost.ToString(), pos + new Vector2(0, 300), Color.White);
                }
            }
        }
    }

    class ShopItem
    {
        public short cost;

        public EquipableItem item;

        public ShopItem(EquipableItem item2, short cost2)
        {
            item = item2;
            cost = cost2;
        }
    }
}
