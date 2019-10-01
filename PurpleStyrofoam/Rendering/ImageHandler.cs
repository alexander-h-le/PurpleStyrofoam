using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    class ImageHandler
    {
        Texture2D Texture { get; set; }
        private ContentManager Content;
        public ImageHandler(ContentManager content, Texture2D textIn)
        {
            Texture = textIn;
            Content = content;
        }
        public ImageHandler(ContentManager content, string name)
        {
            Texture = content.Load<Texture2D>(name);
            Content = content;
        }

        public void Draw(SpriteBatch sp, Vector2 location)
        {
            sp.Begin();
            sp.Draw(Texture, location, Color.White);
            sp.End();
        }
    }
}
