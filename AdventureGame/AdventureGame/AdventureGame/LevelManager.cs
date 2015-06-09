using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    class LevelManager
    {
        public byte currentTilesheet;

        public int[,] map;
        public int[,] mapCollision;

        public void StartLevel(string path)
        {
            ClearLevel();

            map = LoadLevelFile(path);
            mapCollision = LoadLevelFile(path+"C");

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] != 0) Game1.gameObjectsToAdd.Add(new Tile(new Vector2(y * 16, x * 16), (short)map[x, y], false));
                    if (mapCollision[x, y] != 0) Game1.gameObjectsToAdd.Add(new Tile(new Vector2(y * 16, x * 16), (short)mapCollision[x, y], true));
                }
            }

            map = null;
            mapCollision = null;
        }

        public void ClearLevel()
        {
            foreach (GameObject g in Game1.gameObjects)
            {
                if (g is Player == false)
                    Game1.gameObjectsToRemove.Add(g);
            }
        }

        public void LoadObjectLayer()
        {

        }

        public int[,] LoadLevelFile(string path)
        {
            int[,] map;
            string mapData = path + ".txt";
            int width = 0;
            int height = File.ReadLines(mapData).Count();

            StreamReader sReader = new StreamReader(mapData);
            string line = sReader.ReadLine();
            string[] tileNo;
            tileNo = line.Split(',');

            width = tileNo.Count();

            map = new int[height, width];

            sReader = new StreamReader(mapData);

            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                {
                    if (tileNo[x] != "" || tileNo[x] != " ")
                    {
                        map[y, x] = Convert.ToInt32(tileNo[x]);
                    }
                }
            }
            sReader.Close();

            return map;
        }
    }
}
