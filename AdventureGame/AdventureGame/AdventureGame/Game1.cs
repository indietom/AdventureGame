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
        internal static Gui gui = new Gui();
        internal static LevelManager levelManager = new LevelManager();

        internal static List<GameObject> gameObjects = new List<GameObject>();
        internal static List<GameObject> gameObjectsToRemove = new List<GameObject>();
        internal static List<GameObject> gameObjectsToAdd = new List<GameObject>();

        EquipableItem test = new EquipableItem("test.txt");

        protected override void Initialize()
        {
            AssetManager.Load(Content);
            camera = new Camera();
            gameObjectsToAdd.Add(new Player());
            gameObjectsToAdd.Add(new Loot(new Vector2(20, 64), 5));
            gameObjectsToAdd.Add(new BasicMonster(new Vector2(100, 100)));
            gameObjectsToAdd.Add(new PushableTile(new Vector2(100, 100), new Point(1, 430), new Point(32, 32), 0.4f));
            gameObjectsToAdd.Add(new Character(new Vector2(300, 300), "ayy", "this is a test", new Point(1, 166)));
            gameObjectsToAdd.Add(new ItemLoot(new Vector2(-50, -50), new EquipableItem("test.txt")));
            gameObjectsToAdd.Add(new ItemLoot(new Vector2(50, -50), new EquipableItem("test2.txt")));
            gameObjectsToAdd.Add(new ItemLoot(new Vector2(150, -50), new EquipableItem("test2.txt")));
            gameObjectsToAdd.Add(new ItemLoot(new Vector2(25, -50), new EquipableItem("test2.txt")));
            gameObjectsToAdd.Add(new Chest(new Vector2(100, -50), true, new GameObject[] {new Tile(new Vector2(0, 0), 1, false)}));
            gameObjectsToAdd.Add(new ShopKeeper(new Vector2(364, 300)));

            levelManager.StartLevel("maptest");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            gui.Update();

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
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.GetTransform(GraphicsDevice));
            foreach(GameObject gm in gameObjects)
            {
                gm.DrawSprite(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            gui.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
