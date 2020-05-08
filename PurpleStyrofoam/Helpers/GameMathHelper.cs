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
        public static float LookAtXY(Point source, Point target)
        {
            double x1 = target.X - source.X; // Get X distance
            double y1 = target.Y - source.Y; // Get Y distance
            double hypotenuse = Math.Sqrt((x1 * x1) + (y1 * y1)); // Get hypotenuse
            float angle = (float)Math.Acos(x1 / hypotenuse);
            return y1 > 0 ? angle : -angle;
        }

        /// <summary>
        /// Gives the angle in radians to make object look at the mouse
        /// </summary>
        /// <param name="source">The position of the origin</param>
        /// <returns>Returns angle in radians</returns>
        public static float LookAtMouse(Vector2 source)
        {
            double x1 = MouseHandler.mousePos.X - source.X; // Get X distance
            double y1 = MouseHandler.mousePos.Y - source.Y; // Get Y distance
            double hypotenuse = Math.Sqrt((x1 * x1) + (y1 * y1)); // Get hypotenuse
            float angle = (float)Math.Acos(x1 / hypotenuse);
            return y1 > 0 ? angle : -angle;
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
            if (amt <= 2) return "";

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
            return (seconds * 60);
        }

        public static int TimeToFrames(double seconds)
        {
            return (int) Math.Round(seconds * 60);
        }

        public static string NumToRomanNumeral(int amt)
        {
            if (amt < 4) return SingleDigitToNumeral(amt);
            else if (amt < 6) return SingleDigitToNumeral(amt - 5) + "V";
            else if (amt < 9) return "V" + SingleDigitToNumeral(amt - 5);
            else if (amt == 9) return "IX";
            else if (amt == 10) return "X";
            else return "X... ?";
        }

        private static string SingleDigitToNumeral(int amt)
        {
            string temp = "";
            for (int i = 0; i < amt; i++)
            {
                temp += "I";
            }
            return temp;
        }

        public class PIConstants
        {
            public static float PI_45 = (float)(Math.PI / 4);
            public static float PI_90 = (float)(Math.PI / 2);
            public static float PI_270 = (float)((Math.PI * 3) / 2);
        }
    }
}
