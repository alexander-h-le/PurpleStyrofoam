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
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Managers;

namespace PurpleStyrofoam.Maps
{
    class TestMap : BaseMap
    {
        private ContentManager Content;
        public TestMap()
        {
            Content = Game.GameContent;
            AnimatedSprite player = RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            AnimatedSprite enemySprite = new AnimatedSprite(Content.Load<Texture2D>("enemyTest"), 1, 1, 100, 100, new BasicAI(player), new DefaultManager());
            enemySprite.AI.SupplyAI(enemySprite);
            BackgroundLayer = new MapObject[]
                {
                    new MapObject("Background", Content, "testBackground", new Vector2(0,-620), 3800, 1700)
                };
            ActiveLayer = new MapObject[]
                {
                    new MapObject("left-wall", Content, "testIMG", new Vector2(-100,-620), 100, 1700),
                    new MapObject("east-wall", Content, "testIMG", new Vector2(3700,-620), 100, 1700),
                    new MapObject("floor", Content, "testIMG", new Vector2(0, 1080), 3700, 50),
                    new MapObject("roof", Content, "testIMG", new Vector2(0,-620), 3700, 50),
                    new MapObject("platform-left", Content, "testIMG", new Vector2(200, 930), 100,50),
                    new MapObject("platform-right", Content, "testIMG", new Vector2(600, 930), 100, 50),
                    new MapObject("platform-upperx1-right", Content, "testIMG", new Vector2(800, 880), 100, 50),
                    new MapObject("platform-upperx2-right", Content, "testIMG", new Vector2(1000, 780), 100, 50),
                    new MapObject("platform-upperx3-right", Content, "testIMG", new Vector2(1200, 680), 100, 50),
                    new MapObject("platform-upperx4-right", Content, "testIMG", new Vector2(1400, 580), 100, 50),
                    new MapObject("platform-upperx5-right", Content, "testIMG", new Vector2(1600, 480), 100, 50),
                    new MapObject("platform-upperx6-right", Content, "testIMG", new Vector2(1800, 380), 100, 50),
                    new MapObject("platform-upperx7-right", Content, "testIMG", new Vector2(2000, 280), 100, 50),
                    new MapObject("platform-upperx8-right", Content, "testIMG", new Vector2(2200, 180), 100, 50),
                    new MapObject("platform-upperx9-right", Content, "testIMG", new Vector2(2400, 80), 100, 50),
                    new MapObject("platform-upperx10-right", Content, "testIMG", new Vector2(2600, -20), 100, 50),
                    new MapObject("platform-upperx11-right", Content, "testIMG", new Vector2(2800, -120), 100, 50),
                    new MapObject("platform-upperx12-right", Content, "testIMG", new Vector2(3000, -220), 100, 50),
                    new MapObject("platform-upperx13-right", Content, "testIMG", new Vector2(3200, -320), 100, 50),
                    new MapObject("platform-upperx14-right", Content, "testIMG", new Vector2(3400, -420), 100, 50),
                    new MapObject("platform-upperx15-right", Content, "testIMG", new Vector2(3600, -520), 100, 50),
                };
            ForegroundLayer = new MapObject[]
                {
                };
            sprites = new List<AnimatedSprite>()
            {
                enemySprite
            };
        }

        public override MapObject[] BackgroundLayer { get; set; }
        public override MapObject[] ActiveLayer { get; set; }
        public override MapObject[] ForegroundLayer { get; set; }
        public override List<AnimatedSprite> sprites { get; set; }
    }
}
