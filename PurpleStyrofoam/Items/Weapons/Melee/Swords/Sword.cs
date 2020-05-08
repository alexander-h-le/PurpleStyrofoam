using System;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons.Melee.Swords
{
    public abstract class Sword : Melee
    {
        public Sword(string name, int damage, Color rarity, ItemSprite sprite) : 
            base(name, damage, ATTACKSPEED.VERY_FAST, rarity, sprite, new Vector2(60, 60), typeof(Knight))
        {
        }
    }
}
