using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PurpleStyrofoam.Rendering.Menus.PopUpMenu
{
    public class ExitMenuPopUp : IPopUp
    {
        public void Draw(SpriteBatch sp)
        {
            throw new NotImplementedException();
        }

        public bool ShouldOpen(KeyboardState oldState, KeyboardState newState)
        {
            if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape))
            {

            }
        }

        public void Update(SpriteBatch sp)
        {
            throw new NotImplementedException();
        }
    }
}
