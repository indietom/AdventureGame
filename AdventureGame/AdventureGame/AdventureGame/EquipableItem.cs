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
            orgin = new Vector2(OrginalSize.X / 2, OrginalSize.Y / 2);
            scale = 1;
            color = Color.White;
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
                    case "p":
                        spriteCoords = new Point(Convert.ToInt32(currentLine.Split(':')[1]), Convert.ToInt32(currentLine.Split(':')[2]));
                        break;
                }
                
            }
            sr.Dispose();
        }

        public virtual void Use()
        {
            durabilityCount = 1;
            useDelayCount = 1;
            size = OrginalSize;
        }

        public override void Update()
        {
            if (UseType == UseType.Melee)
            {
                foreach (Enemy e in Game1.gameObjects.Where(item => item is Enemy))
                {
                    if (e.HitBox().Intersects(HitBox()) && size.X > 0)
                    {
                        if(e.hitCount <= 0)
                        {
                            e.health -= damege;
                        }
                        e.hitCount = 1;
                    }
                }
            }
            base.Update();
        }

        public virtual void UpdateDraw()
        {
            if (useDelayCount >= 1) useDelayCount += 1;
            if (useDelayCount >= useDelay) useDelayCount = 0;

            if(durabilityCount >= 1)
            {
                durabilityCount += 1;
                durabilityCount = (durabilityCount >= durability) ? (short)0 : durabilityCount;
            }

            if (durabilityCount <= 0) SetSize(0);

            foreach (Player p in Game1.gameObjects.Where(item => item is Player))
            {
                if(p.direction == 0)
                {
                    if (size.Y == 16) pos = p.pos + new Vector2(-size.X / 2, size.Y) + p.Vel();
                    else pos = p.pos + new Vector2(-size.X / 2, size.Y/2 + 16 - orgin.X/2) + p.Vel();
                    rotation = -180;
                }
                else if(p.direction == 1)
                {
                    if (size.Y == 16) pos = p.pos + new Vector2(32 + size.X / 2, size.Y) + p.Vel();
                    else pos = p.pos + new Vector2(32 + size.X / 2, size.Y / 2 + 16 - orgin.X / 2) + p.Vel();
                    rotation = 0;
                }
                else if(p.direction == 2)
                {
                    if (size.Y == 16) pos = p.pos + new Vector2(16 - 2, -size.Y / 2) + p.Vel();
                    else pos = p.pos + new Vector2(size.X-orgin.X/2, -size.Y-4) + p.Vel();
                    rotation = -90;
                }
                else if(p.direction == 3)
                {
                    if (size.Y == 16) pos = p.pos + new Vector2(16, size.Y / 2 + 32) + p.Vel();
                    else pos = p.pos + new Vector2(size.X - orgin.X / 2, 32 + size.Y+4) + p.Vel();
                    rotation = -270;
                }
            }
        }
    }
}
