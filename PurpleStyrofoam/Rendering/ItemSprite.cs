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
    public class ItemSprite
    {
        public Texture2D Texture { get; private set; }
        public Rectangle ItemRectangle;
        public float Angle { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public bool Flipped { get; set; }
        public bool Visible { get; set; }
        public ItemSprite(Texture2D textIn, Vector2 origIn, int xIn = 0, int yIn = 0, float angleIn = 0.0f, float scale = 1.0f, int width = 50, int height = 50)
        {
            Texture = textIn;
            Origin = origIn;
            Scale = scale;
            ItemRectangle = new Rectangle(xIn, yIn, width, height);
            Flipped = false;
            Visible = true;
        }

        public void Draw(SpriteBatch sp)
        {
            if (Visible) sp.Draw(Texture, new Vector2(ItemRectangle.X,ItemRectangle.Y), ItemRectangle, Color.White, Angle, Origin, Scale,
                Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
        }
    }
}
