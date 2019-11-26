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
            if (newState.RightButton == ButtonState.Pressed)
            {
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).X =(int) mousePos.X;
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).Y =(int) mousePos.Y;
            }
            if (newState.LeftButton == ButtonState.Pressed)
            {
                AnimatedSprite character = RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
                _ = (new BaseProjectile(character.X, character.Y, 10, 10,  
                    BaseProjectile.GenerateVelocityVector(character.X, character.Y, (int)mousePos.X, (int)mousePos.Y), 
                    content.Load<Texture2D>("playerSprite"), character));
            }
            oldState = newState;
        }
    }
}
