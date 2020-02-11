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
            AnimatedSprite character = null;
            mousePos = new Vector2((int)RenderHandler.ScreenOffset.X + newState.X, (int)RenderHandler.ScreenOffset.Y + newState.Y);
            if (RenderHandler.CurrentGameState == GAMESTATE.ACTIVE) character = RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            if (newState.RightButton == ButtonState.Pressed)
            {

                switch (RenderHandler.CurrentGameState)
                {
                    case GAMESTATE.MAINMENU:
                        break;
                    case (GAMESTATE.ACTIVE):
                        //RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).X = (int)mousePos.X;
                        //RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).Y = (int)mousePos.Y;
                        ((PlayerManager)character.Manager).Class.RClick();
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
                        MenuHandler.ActiveFullScreenMenu.ActionAtPosition(newState);
                        break;
                    case (GAMESTATE.ACTIVE):
                        ((PlayerController)character).HeldWeapon.OnLeftClick();
                        if (character != null)
                        {
                            AnimatedSprite temp = new AnimatedSprite(content.Load<Texture2D>("playerSpriteMoving"), 1, 1,
                            (int)mousePos.X, (int)mousePos.Y, new BasicAI(character), new DefaultManager());
                            temp.AI.SupplyAI(temp);
                            RenderHandler.Add(temp);
                        }
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
