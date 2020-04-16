using Microsoft.Xna.Framework;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items
{
    public class InventoryDeleteItem : Item
    {
        public override int ID => 000;

        public override string Description => "lol";

        public InventoryDeleteItem() : base("Delete Item", RARITY.SPECIAL, new ItemSprite(TextureHelper.Sprites.EnemySprite)) { }

        public override void OnInventoryUse()
        {
        }
    }
}
