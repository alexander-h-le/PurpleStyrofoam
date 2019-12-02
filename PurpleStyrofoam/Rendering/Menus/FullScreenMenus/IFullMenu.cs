using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus.FullScreenMenus
{
    public interface IFullMenu
    {
        void Draw(SpriteBatch sp);
        void ActionAtPosition(MouseState mouse);
        void Initialize();
    }
}
