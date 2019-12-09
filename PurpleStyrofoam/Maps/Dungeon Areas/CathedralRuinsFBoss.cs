﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Maps.Dungeon_Areas
{
    public class CathedralRuinsFBoss : DungeonArea
    {
        public CathedralRuinsFBoss()
        {
            maxBounds = new Rectangle(0,0,10000,10000);
            ContentManager Content = Game.GameContent;
            BackgroundLayer = new MapObject[]
                {
                };
            ActiveLayer = new MapObject[]
                {
                    new MapObject("StartPlatformRoof", Content, "testIMG", new Vector2(0, 0), 1600, 100),
                    new MapObject("StartPlatformWestWall", Content, "testIMG", new Vector2(0, 0), 100, 500),
                    new MapObject("StartPlatformFloorx1", Content, "testIMG", new Vector2(0, 500), 800, 100),
                    new MapObject("StartPlatformFloorx2", Content, "testIMG", new Vector2(1200, 500), 500, 100),
                    new MapObject("StartPlatformEastWall", Content, "testIMG", new Vector2(1600, 0), 100, 500),

                    new MapObject("StartPlatformLeftFallWall", Content, "testIMG", new Vector2(700, 600), 100, 1000),
                    new MapObject("StartPlatformRightFallWall", Content, "testIMG", new Vector2(1200, 600), 100, 1000),

                    new MapObject("FirstRoomRoof", Content, "testIMG", new Vector2(0, 1600), 800, 100),
                    new MapObject("FirstRoomRoofFar", Content, "testIMG", new Vector2(1200, 1600), 600, 100),
                    new MapObject("FirstRoomLeftWall", Content, "testIMG", new Vector2(0, 1600), 100, 1500),
                    new MapObject("FirstRoomRightWallx1", Content, "testIMG", new Vector2(1700, 1600), 100, 900),
                    new MapObject("FirstRoomRightWallx2", Content, "testIMG", new Vector2(1700, 2800), 100, 300),
                    new MapObject("FirstRoomFloor", Content, "testIMG", new Vector2(0, 3100), 1800, 100),
                    new MapObject("FirstRoomPlaftformx1", Content, "testIMG", new Vector2(300, 2925), 1400, 50),
                    new MapObject("FirstRoomPlaftformx2", Content, "testIMG", new Vector2(100, 2600), 700, 50),
                    new MapObject("FirstRoomPlaftformx3", Content, "testIMG", new Vector2(1000, 2750), 400, 50),
                    new MapObject("FirstRoomPlaftformx4", Content, "testIMG", new Vector2(300, 2200), 1200, 50),

                    new MapObject("FirstRoomHallwayx1", Content, "testIMG", new Vector2(1800, 2800), 800, 100),
                    new MapObject("FirstRoomHallwayx1", Content, "testIMG", new Vector2(1800, 2400), 800, 100),

                    new MapObject("TallRoomLeftWallx1", Content, "testIMG", new Vector2(2600, 2800), 100, 300),
                    new MapObject("TallRoomFloorx1", Content, "testIMG", new Vector2(2600, 3100), 400, 100),
                    new MapObject("TallRoomFloorx2", Content, "testIMG", new Vector2(3200, 3100), 400, 100),
                    new MapObject("TallRoomRightWallx1", Content, "testIMG", new Vector2(3600, 1200), 100, 2000),
                };
            ForegroundLayer = new MapObject[]
                {
                };
        }
        public override MapObject[] BackgroundLayer { get; set; }
        public override MapObject[] ActiveLayer { get; set; }
        public override MapObject[] ForegroundLayer { get; set; }
        public override List<AnimatedSprite> sprites { get; set; }
    }
}