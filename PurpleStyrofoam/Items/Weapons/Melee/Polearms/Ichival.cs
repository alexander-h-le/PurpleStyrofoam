using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Melee.Polearms
{
    class Ichival : Weapon
    {
        private string name;
        private string desc;
        private ATTACKSPEED atkspd;
        public override int Damage => 100;
        public override ATTACKSPEED AttackSpeed { get => atkspd; set => atkspd = value; }
        public override string Name { get => name; set => name = value; }
        public override int ID => 001;
        public override string Description => desc;
        public override RARITY Rarity => RARITY.LEGENDARY;

        public Ichival()
        {
            name = "Ichival";
            desc = "A legendary spear, wielded by countless heroes of the past. With its attunement to lightning, it strikes down its foes with a current strong enough to ionize the very air around it. ";
            atkspd = ATTACKSPEED.MODERATE;
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
            //Thrust out spear to hurt enemies. Any enemies beyond the tip in a small area that the spear didnt hit get electrocuted.
            throw new NotImplementedException();
        }

        public override void OnRightClick()
        {
            throw new NotImplementedException();
        }
    }
}
