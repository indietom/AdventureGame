using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace AdventureGame
{
    enum UseType { Melee, Distance, Magic }

    class EquipableItem : GameObject
    {
        public string Name { private set; get; }
        public string Description { private set; get; }

        public UseType UseType { private set; get; }

        public Point IconSpirteCoords { private set; get; }
        public Point OrginalSize { private set; get; }

        public sbyte damege;
        
        public byte useDelayCount;
        public byte useDelay;

        public short durabilityCount;
        public short durability;    

        public EquipableItem(string path)
        {
            Load(path);
        }

        public void Load(string path)
        {
            int amountOfLines = File.ReadAllLines(path).Count();

            string currentLine;

            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < amountOfLines; i++)
            {
                currentLine = sr.ReadLine();
                switch(currentLine.Split(':')[0])
                {
                    case "n":
                        Name += currentLine.Split(':')[1];
                        break;
                    case "d":
                        Description += currentLine.Split(':')[1];
                        break;
                    case "u":
                        if (currentLine.Split(':')[1] == "melee")
                        {
                            UseType = UseType.Melee;
                        }
                        else if (currentLine.Split(':')[1] == "distance")
                        {
                            UseType = UseType.Distance;
                        }
                        else
                        {
                            UseType = UseType.Magic;
                        }
                        break;
                    case "i":
                        IconSpirteCoords = new Point(Convert.ToInt32(currentLine.Split(':')[1]), Convert.ToInt32(currentLine.Split(':')[2]));
                        break;
                    case "o":
                        OrginalSize = new Point(Convert.ToInt32(currentLine.Split(':')[1]), Convert.ToInt32(currentLine.Split(':')[2]));
                        break;
                    case "h":
                        damege = sbyte.Parse(currentLine.Split(':')[1]);
                        break;
                    case "s":
                        useDelay = byte.Parse(currentLine.Split(':')[1]);
                        break;
                    case "r":
                        durability = short.Parse(currentLine.Split(':')[1]);
                        break;
                }
                
            }
            sr.Dispose();
        }

        public virtual void Use()
        {
            durabilityCount = 1;
            size = OrginalSize;
        }

        public virtual void UpdateDraw()
        {
            if(durabilityCount >= 1)
            {
                durabilityCount += 1;
                durabilityCount = (durabilityCount >= durability) ? (short)0 : durabilityCount;
            }

            if (durabilityCount <= 0) SetSize(0);

            foreach (GameObject gm in Game1.gameObjects.Where(item => item is Player))
            {
                if(gm.direction == 0)
                {
                    pos = gm.pos + new Vector2(-size.X/2, 0);
                    rotation = -180;
                }
                else if(gm.direction == 1)
                {
                    pos = gm.pos + new Vector2(32, 0);
                    rotation = 0;
                }
                else if(gm.direction == 2)
                {
                    pos = gm.pos + new Vector2(0, -size.X/2);
                    rotation = -90;
                }
                else if(gm.direction == 3)
                {
                    pos = gm.pos + new Vector2(0, 32);
                    rotation = -270;
                }
            }
        }
    }
}
