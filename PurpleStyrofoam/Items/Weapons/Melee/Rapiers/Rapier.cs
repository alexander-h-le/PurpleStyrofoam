using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Rapiers
{
    public abstract class Rapier : Weapon
    {
        public Rapier(string name, int damage, Color rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.FAST, rarity, sprite, new Vector2(50, 50), typeof(Rogue))
        {
        }
    }
}
