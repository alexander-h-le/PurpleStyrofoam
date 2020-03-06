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
    public class ItemSprite : GameObject
    {
        public Texture2D Texture { get; private set; }
        private string textureName;
        public Rectangle ItemRectangle;
        public float Angle { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public bool Flipped { get; set; }
        public bool Visible { get; set; }
        public ItemSprite(string TextureName, Vector2 origIn, int xIn = 0, int yIn = 0, float angleIn = 0.0f, float scale = 1.0f, int width = 50, int height = 50)
        {
            textureName = TextureName;
            Origin = origIn;
            Scale = scale;
            ItemRectangle = new Rectangle(xIn, yIn, width, height);
            Flipped = false;
            Visible = true;
        }
        public ItemSprite(Texture2D textureIn, Vector2 origIn, int xIn = 0, int yIn = 0, float angleIn = 0.0f, float scale = 1.0f, int width = 50, int height = 50)
        {
            Texture = textureIn;
            textureName = Texture.Name;
            Origin = origIn;
            Scale = scale;
            ItemRectangle = new Rectangle(xIn, yIn, width, height);
            Flipped = false;
            Visible = true;
        }

        public override void Draw(SpriteBatch sp)
        {
            try {
                if (Visible) sp.Draw(Texture, new Vector2(ItemRectangle.X, ItemRectangle.Y), ItemRectangle, Color.White, Angle, Origin, Scale,
                     Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
            }
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
            if (Texture == null) Texture = Game.GameContent.Load<Texture2D>(textureName);
        }
    }
}
