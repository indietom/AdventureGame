using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class Tile : GameObject
    {
        public bool active;
        public bool solid;

        public Tile(Vector2 pos2, short frame, bool solid2)
        {
            pos = pos2;
            solid = solid2;

            texture = AssetManager.tilesheets[0];

            currentFrame = frame;
            SetSize(16);
            SetSpriteCoords(16*frame, 0); 

            scale = 1.0f;
            color = Color.White;

            active = true;
        }

        public override void Update()
        {
            if (active)
            {
                if (size.X != 16) SetSize(16);
            }
            else
            {
                if (size.X != 0) SetSize(0);
            }
        }
    }
}
