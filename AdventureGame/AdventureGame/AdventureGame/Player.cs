using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Player : GameObject
    {
        public Player()
        {
            pos = new Vector2(1000, 1000);
            SetSpriteCoords(1, 1);
            SetSize(32);
            color = Color.White;
            scale = 1f;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
