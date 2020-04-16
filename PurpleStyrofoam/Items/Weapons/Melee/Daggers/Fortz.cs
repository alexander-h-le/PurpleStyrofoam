using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    class Fortz : Dagger
    {
        public override int ID => 002;

        public override string Description => "One of the few remaining weapons made from Shadowstone. It is rumored that this weapon is sharp enough to pierce a soul, and is invisible to all but the wielder, even in plain sight.";
        public Fortz() : base("Fortz", 100, RARITY.LEGENDARY, new ItemSprite("Fortz"))
        {
        }

        public override void OnQAbility()
        {
            // Blade dance, but enemies a frozen by fear and screen gets darker.
            throw new NotImplementedException();
        }
    }
}
