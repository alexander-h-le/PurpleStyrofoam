using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering
{
    class MapObject
    {
        public string name;
        public Texture2D texture;
        private ContentManager content;
        public Vector2 location;
        public int width;
        public int height;

        public MapObject(string nameIn, ContentManager contentIn, Vector2 locationIn, int wIn, int hIn)
        {
            name = nameIn;
            content = contentIn;
            texture = contentIn.Load<Texture2D>(name);
            location = locationIn;
            width = wIn;
            height = hIn;
        }
        public MapObject(string nameIn, ContentManager contentIn, string textureName, Vector2 locationIn, int wIn, int hIn)
        {
            name = nameIn;
            content = contentIn;
            texture = contentIn.Load<Texture2D>(textureName);
            location = locationIn;
            width = wIn;
            height = hIn;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(texture, new Rectangle((int) location.X, (int) location.Y, width, height), Color.White);
            sp.End();
        }
    }
}
