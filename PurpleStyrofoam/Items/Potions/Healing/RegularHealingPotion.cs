using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Potions.Healing
{
    public class RegularHealingPotion : Potion
    {
        public RegularHealingPotion() : base("Regular Healing Potion", RARITY.COMMON, new ItemSprite("Potions/Healing/RegularHealth")) { }

        public override int ID => 420;

        public override string Description => "A simple healing potion capable of healing injuries.";


        public override string EffectDescription => "Heals 50 HP";

        public override int Duration => 0;

        public override void OnInventoryUse()
        {
            Game.PlayerCharacter.AddHealth(50);
            Game.PlayerManager.Inventory.DeleteItem(this);
        }
    }
}
