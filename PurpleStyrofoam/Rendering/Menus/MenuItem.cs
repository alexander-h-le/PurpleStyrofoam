using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus
{
    public class MenuItem
    {
        public Rectangle MenuRectangle { get; private set; }
        Vector2 StringLocation;
        Texture2D menuTexture;
        SpriteFont font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);
        string Text;
        public MenuItem(Rectangle rect, Texture2D texture = null, string text = "")
        {
            MenuRectangle = rect;
            menuTexture = texture;
            Text = text;

            Vector2 temp = font.MeasureString(text);
            StringLocation = new Vector2(MenuRectangle.X + ((MenuRectangle.Width - temp.X)/2), MenuRectangle.Y + ((MenuRectangle.Height - temp.Y)/2));
            if (StringLocation.X < 0) StringLocation.X = MenuRectangle.X;
            if (StringLocation.Y < 0) StringLocation.Y = MenuRectangle.Y;
        }
        public Action Action{ get; set;}
        public void Draw(SpriteBatch sp)
        {
            if (menuTexture != null) sp.Draw(menuTexture, MenuRectangle, new Rectangle(0, 0, menuTexture.Width, menuTexture.Height), Color.White);
            sp.DrawString(font, Text, StringLocation, Color.White);
        }
    }
}
