using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Maps;

namespace PurpleStyrofoam.Rendering
{
    static class RenderHandler
    {
        public static List<AnimatedSprite> allCharacterSprites { get; private set; }
        public static List<ItemSprite> allItemSprites { get; private set; }
        public static BaseMap selectedMap { get; set; }
        public static void Initialize()
        {
            allCharacterSprites = new List<AnimatedSprite>();
            allItemSprites = new List<ItemSprite>();
        }
        public static void Add(ItemSprite input)
        {
            allItemSprites.Add(input);
        }
        public static void Add(AnimatedSprite input)
        {
            allCharacterSprites.Add(input);
        }
        public static float LookAtXY(ItemSprite objectIn, int xIn, int yIn)
        {
            double deltaX = xIn - objectIn.X;
            double deltaY = yIn - objectIn.Y;
            return (float) Math.Atan2(deltaY, deltaX);
        }
        public static float LookAtMouse(ItemSprite objectIn)
        {
            double deltaX = Mouse.GetState().X - objectIn.X;
            double deltaY = Mouse.GetState().Y - objectIn.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }
        public static float LookAtSprite(ItemSprite objectIn, AnimatedSprite objectToSee)
        {
            double deltaX = objectToSee.X - objectIn.X;
            double deltaY = objectToSee.Y - objectIn.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }
        public static float LookAtSprite(ItemSprite objectIn, string characterSpriteName)
        {
            double deltaX = allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).X - objectIn.X;
            double deltaY = allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).Y - objectIn.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }
    }
}
