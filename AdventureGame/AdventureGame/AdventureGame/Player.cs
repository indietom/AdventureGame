﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame
{
    class Player : GameObject
    {
        public float friction;
        public float OrginalFriction { get; private set; } 

        bool dead;
        public bool inputActive;
        public bool moving;

        short hitCount;
        short maxHitCount;
        public short amountOfArrows;
        public short money;

        public byte mana;
        public byte maxMana;
        public byte amountOfBombs;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        EquipableItem[] equipedItems = new EquipableItem[2];

        public Player()
        {
            pos = new Vector2(0, 0);
            SetSpriteCoords(1, 1);
            SetSize(32);
            color = Color.White;
            scale = 1f;
            friction = 0.7f;
            OrginalFriction = friction;
            speed = 2;
            maxAnimationCount = 4;
            maxFrame = 4;
            inputActive = true;
            maxHealth = 4;
            maxHitCount = 32;
            amountOfArrows = 50;
            equipedItems[0] = new EquipableItem("test.txt");
            equipedItems[1] = new EquipableItem("test.txt");
        }

        void Input()
        {
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
                moving = true;
            }
            if(!keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
            {
                moving = false;
                animationCount = 0;
                currentFrame = 0;
            }

            if (keyboard.IsKeyDown(Keys.X) && !prevKeyboard.IsKeyDown(Keys.X) && equipedItems[0].UseDelayCount <= 0 && equipedItems[1].UseDelayCount <= 0)
            {
                equipedItems[0].Use();
            }

            if (keyboard.IsKeyDown(Keys.Z) && !prevKeyboard.IsKeyDown(Keys.Z) && equipedItems[0].UseDelayCount <= 0 && equipedItems[1].UseDelayCount <= 0)
            {
                equipedItems[1].Use();
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
            if(hitCount >= 1)
            {
                hitCount += 1;
                color = Color.Red;
                inputActive = false;
            }
            if(hitCount >= maxHitCount)
            {
                hitCount = 0;
                color = Color.White;
                inputActive = true;
            }

            foreach (Enemy e in Game1.gameObjects.Where(item => item is Enemy))
            {
                if(e.HitBox().Intersects(HitBox()) && hitCount <= 0)
                {
                    hitCount = 1;
                    health -= 1;
                }
            }
        }

        public override void Update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            Game1.camera.LerpToTarget(pos + new Vector2(16, 16), 0.3f);

            Animate();
            if (!dead) SetSpriteCoords(Frame(currentFrame), Frame(direction));

            for (int i = 0; i < equipedItems.Count(); i++)
            {
                equipedItems[i].UpdateDraw();
                equipedItems[i].Update();
            }

            for (int i = 0; i < equipedItems.Count(); i++)
            {
                if(equipedItems[i].DurabilityCount >= 1)
                {
                    inputActive = false;
                    currentFrame = 4;
                }
                if(equipedItems[i].DurabilityCount >= equipedItems[i].Durability-1)
                {
                    inputActive = true;
                }
            }

            Movment();
            if (inputActive) Input();
            HealthUpdate();

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            base.DrawSprite(spriteBatch, spritesheet);
            for (int i = 0; i < equipedItems.Count(); i++)
            {
                equipedItems[i].DrawSprite(spriteBatch, spritesheet);
            }
            spriteBatch.Draw(spritesheet, new Vector2(300, 0), new Rectangle(123, 213, 123, 213), Color.White);
        }
    }
}
