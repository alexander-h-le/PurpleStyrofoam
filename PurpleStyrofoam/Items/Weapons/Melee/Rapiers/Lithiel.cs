using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Rapiers
{
    class Lithiel : Weapon
    {
        public override int Damage => 100;

        public override ATTACKSPEED AttackSpeed { get; set; }
        public override string Name { get; set; }

        public override int ID => 004;

        public override string Description => "Although it is considered a failed imitation of the legendary sword, Flight, its power is not to be pitied. Wielders of this powerful rapier have been known to finish fights faster than they started.";

        public override RARITY Rarity => RARITY.LEGENDARY;

        public override ImageHandler image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Lithiel()
        {
            Name = "Lithiel";
            AttackSpeed = ATTACKSPEED.MODERATE;
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
            // Attacks have no cooldown speed. I.E insanely fast speed. Also ignores defense.
            throw new NotImplementedException();
        }

        public override void OnRightClick()
        {
            throw new NotImplementedException();
        }
    }
}
