using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureGame
{
    class Bomb : GameObject
    {
        short lifeTime;
        short maxLifeTime;

        float friction;

        bool enemy;
        
        public Bomb()
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
