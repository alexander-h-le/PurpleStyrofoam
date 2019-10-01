using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
