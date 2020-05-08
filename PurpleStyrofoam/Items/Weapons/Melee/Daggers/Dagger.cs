using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Daggers
{
    public abstract class Dagger : Melee
    {
        public Dagger(string name, int damage, Color rarity, ItemSprite sprite) : base(name, damage, ATTACKSPEED.INSANELY_FAST, rarity, sprite, new Vector2(30, 30), typeof(Rogue))
        {
        }
    }
}
