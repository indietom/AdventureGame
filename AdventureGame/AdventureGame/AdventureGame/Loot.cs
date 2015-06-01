using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Loot : GameObject
    {
        byte type;

        public Loot(Vector2 pos2, byte type2)
        {
            pos = pos2;
            type = type2;

            SetSpriteCoords(Frame(type, 16) + 264, 1);
            SetSize(16);
            scale = 1;
            color = Color.White;
        }

        public override void Update()
        {
            foreach (Player p in Game1.gameObjects.Where(item => item is Player))
            {
                if(p.HitBox().Intersects(HitBox()))
                {
                    if(type == 0 && p.health + 1 != p.maxHealth+1)
                    {
                        p.health += 1;
                    }
                    if(type == 1)
                    {
                       p.money += 10;
                    }
                    destroy = true;
                }
            }
            base.Update();
        }
    }
}
