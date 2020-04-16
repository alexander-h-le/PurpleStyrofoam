using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    public class BasicDagger : Dagger
    {
        public BasicDagger() : base("Steel Dagger", 10, RARITY.COMMON, new ItemSprite("Daggers/BasicDagger"))
        {
        }

        public override int ID => 025;

        public override string Description => "The quintessential tool for any assassin.";

        public override void OnQAbility()
        {
            ((Rogue)Game.PlayerManager.Class).CurrentCooldown = 0.0;
            ((Rogue)Game.PlayerManager.Class).MaxCooldown = 0.25;
        }
    }
}
