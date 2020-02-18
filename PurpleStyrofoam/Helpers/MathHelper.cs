using Microsoft.Xna.Framework;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering;
using System;

namespace PurpleStyrofoam.Helpers
{
    public static class MathHelper
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
    }
}
