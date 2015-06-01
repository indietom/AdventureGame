using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            speed = 1;
            orginalSpeed = speed;
            maxHitCount = 8;
            maxTurnCount = 32;
            health = 2;
        }

        public override void Update()
        {
            if (direction == 1)
                spriteEffect = SpriteEffects.FlipHorizontally;
            if (direction == 0)
                spriteEffect = SpriteEffects.None;

            RandomMovment();

            HealthUpdate();
            HitUpdate();
            base.Update();
        }
    }
}
