using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Maps.Dungeon_Areas;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus
{
    public static class MenuHandler
    {
        public static IPopUp ActivePopUp { get; set; }
        public static List<IPopUp> AllPopUps { get; private set; }
        public static void Initialize()
        {
            AllPopUps = new List<IPopUp>();
            AllPopUps.Add(new ExitMenuPopUp());
        }
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
            ActivePopUp.Draw(sp);
        }
        public static void Update()
        {
            if (ActivePopUp != null) ActivePopUp.Update();
        }
        private static KeyboardState oldState;
        private static KeyboardState newState;
        public static void CheckKeys()
        {
            newState = Keyboard.GetState();

            foreach (IPopUp menu in AllPopUps)
            {
                if (menu.ShouldClose(oldState, newState))
                {
                    RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
                    break;
                }
                if (menu.ShouldOpen(oldState,newState))
                {
                    RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
                    break;
                }
            }

            oldState = newState;
        }
    }
}
