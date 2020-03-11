using System;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Polearms
{
    public abstract class Polearm : Weapon
    {
        public Polearm(string name, int damage, RARITY rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.MODERATE, rarity, sprite, typeof(Knight))
        {
        }
    }
}
