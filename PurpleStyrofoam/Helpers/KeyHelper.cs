using System;
using Microsoft.Xna.Framework.Input;

namespace PurpleStyrofoam.Helpers
{
    public static class KeyHelper
    {
        private static KeyboardState oldState;
        private static KeyboardState newState;

        public static bool CheckHeld(params Keys[] keys)
        {
            foreach (Keys k in keys)
            {
                if (!newState.IsKeyDown(k)) return false;
            }
            return true;
        }

        public static bool CheckTap(Keys key)
        {
            return newState.IsKeyDown(key) && oldState.IsKeyUp(key);
        }

        public static bool CheckCombination(Keys finisher, params Keys[] combo)
        {
            return CheckTap(finisher) && CheckHeld(combo);
        }

        public static bool CheckUp(Keys key)
        {
            return newState.IsKeyUp(key);
        }

        public static void Update()
        {
            oldState = newState;
            newState = Keyboard.GetState();
        }
    }
}
