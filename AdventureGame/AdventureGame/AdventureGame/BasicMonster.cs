using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class BasicMonster : Enemy
    {
        public BasicMonster(Vector2 pos2)
        {
            pos = pos2;

            SetSpriteCoords(1, 199);
            SetSize(32);
            scale = 1;
            color = Color.White;
            maxHitCount = 8;
            health = 2;
        }

        public override void Update()
        {
            HealthUpdate();
            HitUpdate();
            base.Update();
        }
    }
}
