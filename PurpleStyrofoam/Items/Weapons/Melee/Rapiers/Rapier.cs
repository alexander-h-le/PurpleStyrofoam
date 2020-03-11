using System;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Rapiers
{
    public abstract class Rapier : Weapon
    {
        public Rapier(string name, int damage, RARITY rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.FAST, rarity, sprite, typeof(Rogue))
        {
        }
    }
}
