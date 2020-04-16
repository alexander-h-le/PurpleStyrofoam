using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items
{
    public abstract class Item
    {
        public string Name { get; set; } // The name that will be displayed to the player
        public abstract int ID { get; } // The ID that the code will refer to it by
        public abstract void OnInventoryUse(); // What happens when player will right click item in the inventory.
        public abstract string Description { get;  } // What will be displayed in the UI for item.
        public Color Rarity { get; } // Get the item's rarity
        public ItemSprite Sprite { get; set; }
        protected Item(string nameIn, Color levelIn, ItemSprite imageIn)
        {
            Name = nameIn;
            Rarity = levelIn;
            Sprite = imageIn;
        }
        public virtual void Update() { }
    }

    public class RARITY // Rarity levels
    {
        public static Color 
            JUNK = Color.Gray, 
            COMMON = Color.Green, 
            UNCOMMON = Color.LightBlue, 
            RARE = Color.Orange, 
            EPIC = Color.Red,
            SECRET = Color.Magenta,
            SPECIAL = Color.White;
    }
}
