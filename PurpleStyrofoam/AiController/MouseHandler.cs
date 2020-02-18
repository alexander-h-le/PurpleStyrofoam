﻿using Microsoft.Xna.Framework.Input;
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
        public static void Update(ContentManager content)
        {
            newState = Mouse.GetState();
            mousePos = new Vector2((int)RenderHandler.ScreenOffset.X + newState.X, (int)RenderHandler.ScreenOffset.Y + newState.Y);
            if (newState.RightButton == ButtonState.Pressed)
            {

                switch (RenderHandler.CurrentGameState)
                {
                    case GAMESTATE.MAINMENU:
                        break;
                    case (GAMESTATE.ACTIVE):
                        //RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).X = (int)mousePos.X;
                        //RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).Y = (int)mousePos.Y;
                        ((PlayerManager)Game.PlayerCharacter.Manager).Class.RClick();
                        //character.Manager.AddDamage(-1);
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
                        AnimatedSprite n = new AnimatedSprite(PlayerManager.jumpingDPlayerSprite, 1, 1, (int)mousePos.X, (int)mousePos.Y, new BasicAI(Game.PlayerCharacter), new DefaultManager());
                        n.AI.SupplyAI(n);
                        n.Load();
                        if (KeyHelper.CheckHeld(Keys.O)) RenderHandler.Add(n);
                        ((PlayerManager)Game.PlayerCharacter.Manager).EquippedWeapon.OnLeftClick();
                        break;
                    case GAMESTATE.PAUSED:
                        MenuHandler.ActivePopUp.ActionAtPosition(mousePos);
                        break;
                    default:
                        break;
                }
            }
            oldState = newState;
        }
    }
}
