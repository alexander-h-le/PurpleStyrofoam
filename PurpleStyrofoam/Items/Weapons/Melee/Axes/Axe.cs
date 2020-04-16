using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Axes
{
    public abstract class Axe : Weapon
    {

        public Axe(string name, int damage, Color rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.SLOW, rarity, sprite, new Vector2(55, 55), typeof(Knight))
        { }
    }
}
