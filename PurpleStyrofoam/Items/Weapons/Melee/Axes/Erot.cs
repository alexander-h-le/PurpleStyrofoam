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

namespace PurpleStyrofoam.Items.Weapons.Melee.Axes
{
    class Erot : Axe
    {
        public override int ID => 005;

        public override string Description => "A forgotten weapon, lost to the tides of time. Wielding it gives you power strong enough to tear through the veils of reality";
        public Erot() : base("Erot", 100, RARITY.EPIC, new ItemSprite("Erot", 55, 55))
        {

        }

        public override void OnQAbility()
        {
            throw new NotImplementedException();
        }
    }
}
