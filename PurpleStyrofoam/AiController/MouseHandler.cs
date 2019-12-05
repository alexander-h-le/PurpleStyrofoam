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

namespace PurpleStyrofoam.AiController
{
    static class MouseHandler
    {
        private static MouseState oldState;
        private static MouseState newState;
        public static void Update(ContentManager content)
        {
            newState = Mouse.GetState();
            Vector2 mousePos = new Vector2((int)RenderHandler.ScreenOffset.X + newState.X, (int)RenderHandler.ScreenOffset.Y + newState.Y);
            AnimatedSprite character = null;
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
                        AnimatedSprite capabiltiy = new AnimatedSprite(Game.GameContent.Load<Texture2D>("playerSpriteJumpingDynamic"), 1, 1, (int)mousePos.X, (int)mousePos.Y,
                            new BasicAI(character), new DefaultManager());
                        capabiltiy.AI.SupplyAI(capabiltiy);
                        RenderHandler.Add(capabiltiy);
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
                        _ = (new BasicProjectile(character.X, character.Y, 10, 10,
                           BasicProjectile.GenerateVelocityVector(character.X, character.Y, (int)mousePos.X, (int)mousePos.Y),
                           RenderHandler.LookAtMouse(new Vector2(character.X, character.Y)),
                           content.Load<Texture2D>("playerSprite"), character));
                        break;
                    default:
                        break;
                }
            }
            oldState = newState;
        }
    }
}
