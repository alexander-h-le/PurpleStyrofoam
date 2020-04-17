using Microsoft.Xna.Framework;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering;
using System;

namespace PurpleStyrofoam.Helpers
{
    public static class GameMathHelper
    {
        /// <summary>
        /// Gives the angle in radians to make object look at certain point
        /// </summary>
        /// <param name="source">The position of the origin</param>
        /// <param name="xIn">The target X position</param>
        /// <param name="yIn">The target Y position</param>
        /// <returns>Returns angle in radians</returns>
        public static float LookAtXY(Vector2 source, int xIn, int yIn)
        {
            double deltaX = xIn - source.X;
            double deltaY = yIn - source.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Gives the angle in radians to make object look at the mouse
        /// </summary>
        /// <param name="source">The position of the origin</param>
        /// <returns>Returns angle in radians</returns>
        public static float LookAtMouse(Vector2 source)
        {
            double deltaX = MouseHandler.mousePos.X - source.X;
            double deltaY = MouseHandler.mousePos.Y - source.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Gives the angle in radians to make an <c>ItemSprite</c> look at a sprite
        /// </summary>
        /// <param name="objectIn">The <c>ItemSprite</c> source</param>
        /// <param name="objectToSee">The <c>AnimatedSprite</c> to look at</param>
        /// <returns>Returns the angle in radians</returns>
        public static float LookAtSprite(ItemSprite objectIn, AnimatedSprite objectToSee)
        {
            double deltaX = objectToSee.SpriteRectangle.X - objectIn.ItemRectangle.X;
            double deltaY = objectToSee.SpriteRectangle.Y - objectIn.ItemRectangle.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Give the angle in radians to make an <c>ItemSprite</c> look at a sprite
        /// </summary>
        /// <param name="objectIn">The <c>ItemSprite</c> source</param>
        /// <param name="characterSpriteName">The name of the sprite to be looked at</param>
        /// <returns>Returns the angle in radians</returns>
        public static float LookAtSprite(ItemSprite objectIn, string characterSpriteName)
        {
            double deltaX = RenderHandler.allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).SpriteRectangle.X - objectIn.ItemRectangle.X;
            double deltaY = RenderHandler.allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).SpriteRectangle.Y - objectIn.ItemRectangle.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        public static int RadianToDegree(float radian)
        {
            return (int) Math.Round( (180/Math.PI) * radian );
        }

        public static float DegreeToRadian(int degree)
        {
            return (float) (Math.PI / 180) * degree;
        }

        public static string FramesToStringTime(int amt)
        {
            if (amt == 0) return "Instantaneous";

            int seconds = (int) (amt / 60);
            int minutes = 0;
            int hours = 0;
            string time = "";
            if (seconds >= 60)
            {
                minutes = seconds / 60;
                seconds = seconds % 60;
                if (minutes >= 60)
                {
                    hours = minutes / 60;
                    minutes = minutes % 60;
                }
            }
            if (hours > 0) time += hours + "h ";
            if (minutes > 0) time += minutes + "m ";
            time += seconds + "s";
            return time;
        }

        public static int TimeToFrames(int seconds)
        {
            return (int) (seconds * 60);
        }

        public class PIConstants
        {
            public static float PI_45 = (float)(Math.PI / 4);
            public static float PI_90 = (float)(Math.PI / 2);
            public static float PI_270 = (float)((Math.PI * 3) / 2);
        }
    }
}
