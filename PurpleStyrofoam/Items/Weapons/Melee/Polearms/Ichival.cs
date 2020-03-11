using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Polearms
{
    class Ichival : Polearm
    {
        public override int ID => 001;
        public override string Description => "A legendary spear, wielded by countless heroes of the past. With its attunement to lightning, it strikes down its foes with a current strong enough to ionize the very air around it. ";
        public Ichival() : base("Ichival", 100, RARITY.LEGENDARY, new ItemSprite("Ichival", new Vector2(0, 0)))
        {
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

    }
}
