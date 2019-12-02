using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus.PopUpMenu
{
    public interface IPopUp
    {
        void Draw(SpriteBatch sp);
        void Update(SpriteBatch sp);
    }
}
