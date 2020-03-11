using System;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Axes
{
    public abstract class Axe : Weapon
    {

        public Axe(string name, int damage, RARITY rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.SLOW, rarity, sprite, typeof(Knight))
        { }
    }
}
