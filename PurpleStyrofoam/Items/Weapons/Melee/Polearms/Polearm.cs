using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Polearms
{
    public abstract class Polearm : Weapon
    {
        public Polearm(string name, int damage, Color rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.MODERATE, rarity, sprite, new Vector2(80, 80), typeof(Knight))
        {
        }
    }
}
