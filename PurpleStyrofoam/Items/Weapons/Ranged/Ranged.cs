using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged
{
    public abstract class Ranged : Weapon
    {
        protected Ranged(string nameIn, int damage, float atkspd, Color levelIn, ItemSprite imageIn, Vector2 ItemSize, params Type[] EquippableBy) : 
            base(nameIn, damage, atkspd, levelIn, imageIn, ItemSize, EquippableBy)
        {
        }
    }
}
