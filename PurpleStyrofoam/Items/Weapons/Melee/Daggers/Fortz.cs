using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    class Fortz : Weapon
    {
        public override int Damage => 100;

        public override ATTACKSPEED AttackSpeed { get; set; }
        public override string Name { get; set; }

        public override int ID => 002;

        public override string Description => "One of the few remaining weapons made from Shadowstone. It is rumored that this weapon is sharp enough to pierce a soul, and is invisible to all but the wielder, even in plain sight.";

        public override RARITY Rarity => RARITY.LEGENDARY;

        public override ImageHandler image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Fortz()
        {
            AttackSpeed = ATTACKSPEED.FAST;
            Name = "Fortz";
        }

        public override void OnInventoryUse()
        {
            throw new NotImplementedException();
        }

        public override void OnLeftClick()
        {
            throw new NotImplementedException();
        }

        public override void OnQAbility()
        {
            // Blade dance, but enemies a frozen by fear and screen gets darker.
            throw new NotImplementedException();
        }

        public override void OnRightClick()
        {
            throw new NotImplementedException();
        }
    }
}
