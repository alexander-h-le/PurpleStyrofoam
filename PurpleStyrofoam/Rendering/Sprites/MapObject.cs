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
    public class MapObject : GameObject
    {
        public Texture2D texture;
        protected string TextureName;
        public Vector2 location;
        public int width;
        public int height;
        public Rectangle MapRectangle;
        public MapObject(string textureName, Vector2 locationIn, int wIn, int hIn)
        {
            TextureName = textureName;
            location = locationIn;
            width = wIn;
            height = hIn;
            MapRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch sp)
        {
            try { sp.Draw(texture, new Rectangle((int)location.X, (int)location.Y, width, height), Color.White); }
            catch (ArgumentNullException e)
            {
                throw new Exception("Texture was not properly loaded at: " + e.ParamName);
            }
        }

        public override void Update()
        {
            return;
        }

        public override void Load()
        {
            texture = Game.GameContent.Load<Texture2D>(TextureName);
        }
    }
}
