using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Maps;

namespace PurpleStyrofoam.Rendering
{
    static class RenderHandler
    {
        public static List<AnimatedSprite> allCharacterSprites { get; private set; }
        public static List<ItemSprite> allItemSprites { get; private set; }
        private static BaseMap selectedMap;
        public static bool IsLoading { get; set; }
        private static bool FirstTime;
        public static void Initialize()
        {
            allCharacterSprites = new List<AnimatedSprite>();
            allItemSprites = new List<ItemSprite>();
            IsLoading = false;
            FirstTime = true;
        }
        public static void InitiateChange(BaseMap newMap, PlayerController player, int newX = 0, int newY = 0)
        {
            IsLoading = true;
            FirstTime = true;
            selectedMap = newMap;
            allCharacterSprites = new List<AnimatedSprite>();
            allCharacterSprites.Add(player);
            allItemSprites = new List<ItemSprite>();
            if (player.HeldWeapon != null) allItemSprites.Add(player.HeldWeapon.sprite);
            player.X = newX;
            player.Y = newY;
        }
        public static void InitiateChange(BaseMap newMap, PlayerController player, List<AnimatedSprite> newSprites, List<ItemSprite> newItems, int newX = 0, int newY = 0)
        {
            IsLoading = true;
            FirstTime = true;
            selectedMap = newMap;
            allCharacterSprites = newSprites;
            allItemSprites = newItems;
            player.X = newX;
            player.Y = newY;
        }
        public static void Update()
        {
        }
        public static void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.End();
            if (selectedMap != null) selectedMap.Draw(sp);
            foreach (AnimatedSprite item in allCharacterSprites)
            {
                item.Draw(sp);
            }
            foreach (ItemSprite item in allItemSprites)
            {
                item.Draw(sp);
            }
            if (selectedMap != null) selectedMap.DrawForeground(sp);
            if (FirstTime) IsLoading = false;
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
