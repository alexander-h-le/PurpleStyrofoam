using Microsoft.Xna.Framework;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged.Wands
{
    public class Iythil : Wand
    {
        public Iythil() : base("Iythil's Wand", 10, RARITY.SECRET, new ItemSprite("Swords/Flight", 40, 40))
        {
        }

        public override int ID => 009;

        public override string Description => "Iythil, one of the best mages to ever exist, created this wand for his descendants. Unfortunately, his descendants no longer own this weapon, and now you own it.";

        public override void OnQAbility()
        {
            throw new NotImplementedException();
        }
    }
}
