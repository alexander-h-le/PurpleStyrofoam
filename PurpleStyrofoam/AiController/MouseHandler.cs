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
            if (newState.RightButton == ButtonState.Pressed)
            {
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).X = (int) RenderHandler.ScreenOffset.X + newState.X;
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).Y = (int) RenderHandler.ScreenOffset.Y + newState.Y;
            }
            if (newState.LeftButton == ButtonState.Pressed)
            {
                _ = (new TestProjectile((int)RenderHandler.ScreenOffset.X + newState.X, (int)RenderHandler.ScreenOffset.Y + newState.Y, 10, 10, 
                    new Vector2(), content.Load<Texture2D>("playerSprite")));
            }
            oldState = newState;
        }
    }
}
