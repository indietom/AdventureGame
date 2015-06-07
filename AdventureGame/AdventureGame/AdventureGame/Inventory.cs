using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Inventory
    {
        EquipableItem[,] items = new EquipableItem[4, 4];

        Vector2 pos;

        public bool active;
        public bool full;

        Point currentSelect;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public short delay;

        public Inventory()
        {
            Random random = new Random();

            pos = new Vector2(16, 70);

            // just test, ingnore
            for (int i = 0; i < 17; i++)
            {
                if (GetNextEmptyCell() != new Point(-1, -1)) full = false;
                else full = true;
                AddItem(new EquipableItem("test.txt"));
            }

            items[2, 1] = new EquipableItem("test.txt");
        }

        public void Input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (items[currentSelect.X, currentSelect.Y] != null)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    if (keyboard.IsKeyDown(Keys.C) && !prevKeyboard.IsKeyDown(Keys.C))
                    {
                        Game1.gameObjectsToAdd.Add(new ItemLoot(p.pos + new Vector2(16, 16), items[currentSelect.X, currentSelect.Y]));
                        items[currentSelect.X, currentSelect.Y] = null;
                    }

                    if (keyboard.IsKeyDown(Keys.X) && !prevKeyboard.IsKeyDown(Keys.X))
                    {
                        if (items[currentSelect.X, currentSelect.Y].Name != p.equipedItems[1].Name)
                        {
                            p.equipedItems[0] = items[currentSelect.X, currentSelect.Y];
                        }
                    }
                    if (keyboard.IsKeyDown(Keys.Z) && !prevKeyboard.IsKeyDown(Keys.Z))
                    {
                        if (items[currentSelect.X, currentSelect.Y].Name != p.equipedItems[0].Name)
                        {
                            p.equipedItems[1] = items[currentSelect.X, currentSelect.Y];
                        }
                    }
                }
            }

            if (keyboard.IsKeyDown(Keys.Left) && !prevKeyboard.IsKeyDown(Keys.Left) && currentSelect.X != 0)
            {
                currentSelect.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Right) && !prevKeyboard.IsKeyDown(Keys.Right) && currentSelect.X != items.GetLength(0)-1)
            {
                currentSelect.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.Up) && !prevKeyboard.IsKeyDown(Keys.Up) && currentSelect.Y != 0)
            {
                currentSelect.Y -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Down) && !prevKeyboard.IsKeyDown(Keys.Down) && currentSelect.Y != items.GetLength(1) - 1)
            {
                currentSelect.Y += 1;
            }

            if (keyboard.IsKeyDown(Keys.Q) && !prevKeyboard.IsKeyDown(Keys.Q) && delay <= 0)
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
            if (GetNextEmptyCell() != new Point(-1, -1)) full = false;
            else full = true;

            if (active)
            {
                foreach (Player p in Game1.gameObjects.Where(item => item is Player))
                {
                    p.inputActive = false;
                }

                Input();

                for (int y = 0; y < items.GetLength(1); y++)
                {
                    for (int x = 0; x < items.GetLength(0); x++)
                    {
                        if (items[x, y] != null)
                        {
                            if (items[currentSelect.X, currentSelect.Y] == items[x, y])
                            {
                                items[x, y].iconScale = Globals.Lerp(items[x, y].iconScale, 0.7f, 0.1f);
                                items[x, y].color = new Color(205, 205, 205);
                            }
                            else
                            {
                                items[x, y].iconScale = Globals.Lerp(items[x, y].iconScale, 1, 0.2f);
                                items[x, y].color = Color.White;
                            }
                        }
                    }
                }
            }
            else
            {
                currentSelect = new Point(items.GetLength(0) / 2 - 1, items.GetLength(1) / 2 - 1);
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    for (int x = 0; x < items.GetLength(0); x++)
                    {
                        if (items[x, y] != null)
                        {
                            items[x, y].iconScale = 1;
                            items[x, y].color = Color.White;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                if (delay >= 1)
                {
                    delay += 1;
                    if (delay >= 8) delay = 0;
                }
                spritebatch.Draw(AssetManager.spritesheet, pos + new Vector2((currentSelect.X * 64)-16, (currentSelect.Y * 64)-16), new Rectangle(364, 67, 32, 32), Color.White);
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    for (int x = 0; x < items.GetLength(0); x++)
                    {
                        if (items[x, y] != null)
                        {
                            spritebatch.Draw(AssetManager.spritesheet, pos + new Vector2(x * 64, y * 64), new Rectangle(items[x, y].IconSpirteCoords.X, items[x, y].IconSpirteCoords.Y, 32, 32), items[x, y].color, 0, new Vector2(16, 16), items[x, y].iconScale, SpriteEffects.None, 0);
                        }
                    }
                }

                if(items[currentSelect.X, currentSelect.Y] != null)
                {
                    spritebatch.DrawString(AssetManager.bigFont, items[currentSelect.X, currentSelect.Y].Name, pos + new Vector2(0, 270), Color.White);
                    spritebatch.DrawString(AssetManager.smallFont, items[currentSelect.X, currentSelect.Y].Description, pos + new Vector2(0, 300), Color.White);
                }
            }
        }

        public void AddItem(EquipableItem item)
        {
            if(!full) items[GetNextEmptyCell().X, GetNextEmptyCell().Y] = item;
        }

        public Point GetNextEmptyCell()
        {

            for (int y = 0; y < items.GetLength(1); y++)
            {
                for (int x = 0; x < items.GetLength(0); x++)
                {
                    if (items[x, y] == null)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return new Point(-1, -1);
        }
    }
}
