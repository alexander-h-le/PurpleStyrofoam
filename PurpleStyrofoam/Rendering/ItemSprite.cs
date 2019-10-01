using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PurpleStyrofoam.Rendering
{
    class ItemSprite
    {
        public Texture2D Texture { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Angle { get; set; }
        public Vector2 Origin { get; set; }
        public ItemSprite(Texture2D textIn, Vector2 origIn, int xIn = 0, int yIn = 0, float angleIn = 0.0f)
        {
            Texture = textIn;
            X = xIn;
            Y = yIn;
            Origin = origIn;
        }

        public void Draw(SpriteBatch sp, float scale = 1.0f)
        {
            sp.Begin();
            sp.Draw(Texture, new Vector2(X,Y), new Rectangle(0,0, Texture.Width, Texture.Height), Color.White, Angle, Origin, scale, SpriteEffects.None, 1.0f);
            sp.End();
        }
    }
}
