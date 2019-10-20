using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace PurpleStyrofoam.Maps
{
    class TestMap : BaseMap
    {
        private ContentManager Content;
        public TestMap(ContentManager inC)
        {
            Content = inC;
            BackgroundLayer = new MapObject[]
                {

                };
            ActiveLayer = new MapObject[]
                {
                    new MapObject("floor", Content, "testIMG", new Vector2(-(int) Game1.ScreenSize.X, (int) Game1.ScreenSize.Y - 50), (int) Game1.ScreenSize.X*5, 50),
                    new MapObject("roof", Content, "testIMG", new Vector2(0,0), (int) Game1.ScreenSize.X, 50),
                    new MapObject("platform-left", Content, "testIMG", new Vector2(200, Game1.ScreenSize.Y - 200), 100,50),
                    new MapObject("platform-right", Content, "testIMG", new Vector2(600, Game1.ScreenSize.Y - 200), 100, 50),
                    new MapObject("platform-upperx1-right", Content, "testIMG", new Vector2(800, Game1.ScreenSize.Y - 300), 100, 50),
                    new MapObject("platform-upperx2-right", Content, "testIMG", new Vector2(1000, Game1.ScreenSize.Y - 400), 100, 50),
                    new MapObject("platform-upperx3-right", Content, "testIMG", new Vector2(1200, Game1.ScreenSize.Y - 500), 100, 50),
                    new MapObject("platform-upperx4-right", Content, "testIMG", new Vector2(1400, Game1.ScreenSize.Y - 600), 100, 50),
                    new MapObject("platform-upperx5-right", Content, "testIMG", new Vector2(1600, Game1.ScreenSize.Y - 700), 100, 50),
                    new MapObject("platform-upperx6-right", Content, "testIMG", new Vector2(1800, Game1.ScreenSize.Y - 800), 100, 50),
                    new MapObject("platform-upperx7-right", Content, "testIMG", new Vector2(2000, Game1.ScreenSize.Y - 900), 100, 50),
                    new MapObject("platform-upperx8-right", Content, "testIMG", new Vector2(2200, Game1.ScreenSize.Y - 1000), 100, 50),
                    new MapObject("platform-upperx9-right", Content, "testIMG", new Vector2(2400, Game1.ScreenSize.Y - 1100), 100, 50),
                    new MapObject("platform-upperx10-right", Content, "testIMG", new Vector2(2600, Game1.ScreenSize.Y - 1200), 100, 50),
                    new MapObject("platform-upperx11-right", Content, "testIMG", new Vector2(2800, Game1.ScreenSize.Y - 1300), 100, 50),
                    new MapObject("platform-upperx12-right", Content, "testIMG", new Vector2(3000, Game1.ScreenSize.Y - 1400), 100, 50),
                    new MapObject("platform-upperx13-right", Content, "testIMG", new Vector2(3200, Game1.ScreenSize.Y - 1500), 100, 50),
                    new MapObject("platform-upperx14-right", Content, "testIMG", new Vector2(3400, Game1.ScreenSize.Y - 1600), 100, 50),
                    new MapObject("platform-upperx15-right", Content, "testIMG", new Vector2(3600, Game1.ScreenSize.Y - 1700), 100, 50),
                };
            ForegroundLayer = new MapObject[]
                {

                };
        }

        public override MapObject[] BackgroundLayer { get; set; }
        public override MapObject[] ActiveLayer { get; set; }
        public override MapObject[] ForegroundLayer { get; set; }
    }
}
