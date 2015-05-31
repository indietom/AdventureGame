using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Player : GameObject
    {
        float friction;

        sbyte health;

        bool dead;
        bool inputActive;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Player()
        {
            pos = new Vector2(0, 0);
            SetSpriteCoords(1, 1);
            SetSize(32);
            color = Color.White;
            scale = 1f;
            friction = 0.7f;
            speed = 2;
            maxAnimationCount = 8;
            maxFrame = 4;
            inputActive = true;
        }

        void Input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Left))
            {
                velX -= speed;
                direction = 0;
            }

            if(keyboard.IsKeyDown(Keys.Right))
            {
                velX += speed;
                direction = 1;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                velY -= speed;
                direction = 2;
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                velY += speed;
                direction = 3;
            }

            if(keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.Up))
            {
                animationCount += 1;
            }
            if(!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
            {
                animationCount = 0;
                currentFrame = 0;
            }
        }

        void Movment()
        {
            if (velX >= 0.2f || velX <= -0.2f)
                pos.X += velX;
            if (velY >= 0.2f || velY <= -0.2f)
                pos.Y += velY;

            velX *= friction;
            velY *= friction;
        }

        void HealthUpdate()
        {

        }

        public override void Update()
        {
            //Game1.camera.LerpToTarget(pos + new Vector2(16, 16), 0.1f);

            Animate();
            if(!dead) SetSpriteCoords(Frame(currentFrame), Frame(direction));

            Movment();
            if(inputActive) Input();
            HealthUpdate();

            base.Update();
        }
    }
}
