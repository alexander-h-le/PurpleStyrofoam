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
        public void Draw(SpriteBatch sp)
        {
            foreach (MapObject item in BackgroundLayer)
            {
                //Debug.WriteLine($"Drawing {item.name} for {this.GetType().Name}");
                item.Draw(sp);
            }
            foreach (MapObject item in ActiveLayer)
            {
                //Debug.WriteLine($"Drawing {item.name} for {this.GetType().Name}");
                item.Draw(sp);
            }
        }
        public void DrawForeground(SpriteBatch sp)
        {
            foreach (MapObject item in ForegroundLayer)
            {
                //Debug.WriteLine($"Drawing {item.name} for {this.GetType().Name}");
                item.Draw(sp);
            }
        }
    }
}
