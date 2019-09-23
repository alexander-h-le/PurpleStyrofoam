using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items
{
    abstract class Item
    {
        public abstract string Name { get; set; } // The name that will be displayed to the player
        public abstract int ID { get; } // The ID that the code will refer to it by
        public abstract void OnInventoryUse(); // What happens when player will right click item in the inventory.
        public abstract string Description { get;  } // What will be displayed in the UI for item.
        public abstract RARITY Rarity { get; } // Get the item's rarity
        /**
         * TODO
         *  - add way to get display process
         *  - add way to get image information
         **/
    }

    enum RARITY // Rarity levels
    {
        JUNK, COMMON, UNCOMMON, RARE, EPIC, LEGENDARY, SPECIAL
    }
}
