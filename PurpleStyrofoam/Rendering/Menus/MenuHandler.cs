using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Maps.Dungeon_Areas;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using PurpleStyrofoam.Rendering.Text;
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
            if (ActivePopUp != null) ActivePopUp.Draw(sp);
        }
        public static void Update()
        {
            if (ActivePopUp != null) ActivePopUp.Update();
        }
        public static void CheckKeys()
        {
            foreach (IPopUp menu in AllPopUps)
            {
                if (menu.ShouldClose())
                {
                    ActivePopUp = null;
                    if (DialogueHandler.ActiveDialogue == null) RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
                    break;
                }
                if (menu.ShouldOpen())
                {
                    ActivePopUp = menu;
                    RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
                    break;
                }
            }
        }
    }
}
