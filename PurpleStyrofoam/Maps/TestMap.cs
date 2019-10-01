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
                    new MapObject("floor", Content, "testIMG", new Vector2(0, 430), 800, 50),
                    new MapObject("roof", Content, "testIMG", new Vector2(0,0), 800, 50),
                    new MapObject("platform-left", Content, "testIMG", new Vector2(200, 280), 100,50),
                    new MapObject("platform-right", Content, "testIMG", new Vector2(600, 280), 100, 50)
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
