using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus
{
    public static class MenuHandler
    {
        public static List<IPopUp> ActivePopUps { get; private set; }
        public static IFullMenu ActiveFullScreenMenu { 
            get
            {
                return fullMenu;
            }
            set
            {
                fullMenu = value;
                ActiveFullScreenMenu.Initialize();
            }
        }
        private static IFullMenu fullMenu;
        public static void DrawFullScreenMenu(SpriteBatch sp)
        {
            ActiveFullScreenMenu.Draw(sp);
        }
        public static void DrawPopUpMenu(SpriteBatch sp)
        {
            foreach (IPopUp menu in ActivePopUps)
            {
                menu.Draw(sp);
            }
        }
        public static void Update()
        {

        }
    }
}
