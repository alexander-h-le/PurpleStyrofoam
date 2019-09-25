﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Polearms
{
    class Ichival : Weapon
    {
        private ATTACKSPEED atkspd;
        public override int Damage => 100;
        public override ATTACKSPEED AttackSpeed { get; set; }
        public override string Name { get; set; }
        public override int ID => 001;
        public override string Description => "A legendary spear, wielded by countless heroes of the past. With its attunement to lightning, it strikes down its foes with a current strong enough to ionize the very air around it. ";
        public override RARITY Rarity => RARITY.LEGENDARY;

        public override ImageHandler image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Ichival()
        {
            Name = "Ichival";
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
            //Thrust out spear to hurt enemies. Any enemies beyond the tip in a small area that the spear didnt hit get electrocuted.
            throw new NotImplementedException();
        }

        public override void OnRightClick()
        {
            throw new NotImplementedException();
        }
    }
}
