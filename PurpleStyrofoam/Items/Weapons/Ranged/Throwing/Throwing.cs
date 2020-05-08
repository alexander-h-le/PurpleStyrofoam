using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged.Throwing
{
    public abstract class Throwing : Ranged
    {
        protected Throwing(string nameIn, int damage, Color levelIn, ItemSprite imageIn, Vector2 ItemSize) : 
            base(nameIn, damage, ATTACKSPEED.VERY_FAST, levelIn, imageIn, ItemSize, typeof(Rogue))
        {
        }
    }
}
