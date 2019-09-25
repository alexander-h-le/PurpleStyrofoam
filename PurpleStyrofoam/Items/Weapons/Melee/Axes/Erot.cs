using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Axes
{
    class Erot : Weapon
    {
        public override int Damage => 100;

        public override ATTACKSPEED AttackSpeed { get; set; }
        public override string Name { get; set; }

        public override int ID => 005;

        public override string Description => "A forgotten weapon, lost to the tides of time. Wielding it gives you power strong enough";

        public override RARITY Rarity => RARITY.LEGENDARY;

        public override ImageHandler image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
        }

        public override void OnRightClick()
        {
            throw new NotImplementedException();
        }
    }
}
