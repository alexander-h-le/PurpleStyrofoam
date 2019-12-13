using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    class Fortz : Weapon
    {
        public override int ID => 002;

        public override string Description => "One of the few remaining weapons made from Shadowstone. It is rumored that this weapon is sharp enough to pierce a soul, and is invisible to all but the wielder, even in plain sight.";
        public Fortz(ContentManager content) : base("Fortz", 100, ATTACKSPEED.FAST, RARITY.LEGENDARY, new ItemSprite(content.Load<Texture2D>("Fortz"), new Vector2(0, 0)))
        {
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

    }
}
