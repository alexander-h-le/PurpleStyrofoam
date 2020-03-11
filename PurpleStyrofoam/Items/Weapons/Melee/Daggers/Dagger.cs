using System;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    public abstract class Dagger : Weapon
    {
        public Dagger(string name, int damage, RARITY rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.VERY_FAST, rarity, sprite, typeof(Rogue))
        {
        }
    }
}
