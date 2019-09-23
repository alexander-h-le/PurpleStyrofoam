using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    class Fortz : Weapon
    {
        private int dmg;
        private ATTACKSPEED atkspd;
        private string name;
        private string desc;
        public override int Damage => dmg;

        public override ATTACKSPEED AttackSpeed { get => atkspd; set => atkspd = value; }
        public override string Name { get => name; set => name = value; }

        public override int ID => 002;

        public override string Description => desc;

        public override RARITY Rarity => RARITY.LEGENDARY;

        public Fortz()
        {
            dmg = 100;
            atkspd = ATTACKSPEED.FAST;
            name = "Fortz";
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
