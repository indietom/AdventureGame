using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class ItemLoot : GameObject
    {
        EquipableItem equipableItem;

        float cosCount;

        KeyboardState keyboard;

        public ItemLoot(Vector2 pos2, EquipableItem item2)
        {
            pos = pos2;
            equipableItem = item2;

            spriteCoords = equipableItem.spriteCoords;
            SetSize(equipableItem.OrginalSize.X, equipableItem.OrginalSize.Y);

            orgin = new Vector2(size.X / 2, size.Y / 2);

            scale = 1;
            color = Color.White;

            rotation = -45;
        }

        public override void Update()
        {
            keyboard = Keyboard.GetState();

            pos.Y += (float)Math.Sin(20 * cosCount + 30);
            cosCount += 0.01f;

            foreach(Player p in Game1.gameObjects.Where(item => item is Player))
            {
                if (p.HitBox().Intersects(HitBox()))
                {
                    if (keyboard.IsKeyDown(Keys.X) && !Gui.inventory.full && !Gui.inventory.active)
                    {
                        Gui.inventory.AddItem(equipableItem);
                        destroy = true;
                    }
                }
            }

            base.Update();
        }
    }
}
