using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;
using System.Diagnostics;

namespace PurpleStyrofoam.AiController
{
    static class MouseHandler
    {
        private static MouseState oldState;
        private static MouseState newState;
        public static void Update()
        {
            newState = Mouse.GetState();
            if (newState.RightButton == ButtonState.Pressed)
            {
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).X = newState.X;
                RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")).Y = newState.Y;
            }
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
            }
            oldState = newState;
        }
    }
}
