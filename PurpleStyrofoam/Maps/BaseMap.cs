using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Maps
{
    abstract class BaseMap
    {
        public abstract MapObject[] BackgroundLayer { get; set; }
        public abstract MapObject[] ActiveLayer { get; set; }
        public abstract MapObject[] ForegroundLayer { get; set; }
        public abstract List<AnimatedSprite> sprites { get; set; }

        public void DrawBackground(SpriteBatch sp)
        {
            foreach (MapObject item in BackgroundLayer)
            {
                item.Draw(sp);
            }
        }
        public void Draw(SpriteBatch sp)
        {
            foreach (MapObject item in ActiveLayer)
            {
                item.Draw(sp);
            }
        }
        public void DrawForeground(SpriteBatch sp)
        {
            foreach (MapObject item in ForegroundLayer)
            {
                item.Draw(sp);
            }
        }
    }
}
