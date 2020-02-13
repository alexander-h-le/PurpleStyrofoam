using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Texture2D menuTexture;
        public MenuItem(Rectangle rect, Texture2D texture)
        {
            MenuRectangle = rect;
            menuTexture = texture;
        }
        public Action Action{ get; set;}
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(menuTexture, MenuRectangle, new Rectangle(0,0,menuTexture.Width, menuTexture.Height), Color.White);
        }
    }
}
