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
    public class BlankItem : Item
    {
        public override int ID => 000;

        public override string Description => "No Item";

        public BlankItem() : base("Blank Item", RARITY.SPECIAL, new ItemSprite(TextureHelper.Sprites.TestImage)) { }

        public override void OnInventoryUse()
        {
            
        }
    }
}
