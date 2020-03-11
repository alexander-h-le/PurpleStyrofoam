using System;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Swords
{
    public abstract class Sword : Weapon
    {
        public Sword(string name, int damage, RARITY rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.VERY_FAST, rarity, sprite, typeof(Knight), typeof(Manipulator))
        {
        }
    }
}
