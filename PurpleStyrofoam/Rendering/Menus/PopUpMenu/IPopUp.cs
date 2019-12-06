using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        bool ShouldOpen(KeyboardState oldState, KeyboardState newState);
    }
}
