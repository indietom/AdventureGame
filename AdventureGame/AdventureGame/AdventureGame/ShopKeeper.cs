using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class ShopKeeper : Character
    {
        public ShopKeeper(Vector2 pos2)
        {
            pos = pos2;

            name = "Shop Keeper";
            text = "What are you buying?";

            spriteCoords = new Point(34, 166);
            SetSize(32);
            scale = 1;
            color = Color.White;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
