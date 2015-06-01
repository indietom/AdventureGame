using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdventureGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;
        }

        internal static Camera camera;

        internal static List<GameObject> gameObjects = new List<GameObject>();
        internal static List<GameObject> gameObjectsToRemove = new List<GameObject>();
        internal static List<GameObject> gameObjectsToAdd = new List<GameObject>();

        EquipableItem test = new EquipableItem("test.txt");

        protected override void Initialize()
        {
            camera = new Camera();
            gameObjectsToAdd.Add(new Player());
            base.Initialize();
        }

        Texture2D spritesheet;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach(GameObject gm in gameObjectsToAdd)
            {
                gameObjects.Add(gm);
            }
            gameObjectsToAdd.Clear();

            foreach (GameObject gm in gameObjectsToRemove)
            {
                gameObjects.Remove(gm);
            }
            gameObjectsToRemove.Clear();

            foreach (GameObject gm in gameObjects)
            {
                gm.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.GetTransform(GraphicsDevice));
            foreach(GameObject gm in gameObjects)
            {
                gm.DrawSprite(spriteBatch, spritesheet);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
