using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.Rendering.Menus;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Managers;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using PurpleStyrofoam.Helpers;

namespace PurpleStyrofoam.AiController
{
    public static class MouseHandler
    {
        private static MouseState oldState;
        private static MouseState newState;
        public static Vector2 mousePos;
        public static void Update()
        {
            mousePos = new Vector2((int)RenderHandler.ScreenOffset.X + newState.X, (int)RenderHandler.ScreenOffset.Y + newState.Y);
            if (newState.RightButton == ButtonState.Pressed)
            {

                switch (RenderHandler.CurrentGameState)
                {
                    case GAMESTATE.MAINMENU:
                        break;
                    case (GAMESTATE.ACTIVE):
                        ((PlayerManager)Game.PlayerCharacter.Manager).Class.RClick();
                        break;
                    default:
                        break;
                }
            }
            if (newState.LeftButton == ButtonState.Pressed)
            {
                switch (RenderHandler.CurrentGameState)
                {
                    case GAMESTATE.MAINMENU:
                        if (oldState.LeftButton == ButtonState.Released) MenuHandler.ActiveFullScreenMenu.ActionAtPosition(newState);
                        break;
                    case (GAMESTATE.ACTIVE):
                        ((PlayerManager)Game.PlayerCharacter.Manager).EquippedWeapon.OnLeftClick();
                        break;
                    case GAMESTATE.PAUSED:
                        if (MenuHandler.ActivePopUp != null && oldState.LeftButton == ButtonState.Released) MenuHandler.ActivePopUp.ActionAtPosition(mousePos);
                        break;
                    default:
                        break;
                }
            }
            oldState = newState;
            newState = Mouse.GetState();
        }
    }
}
