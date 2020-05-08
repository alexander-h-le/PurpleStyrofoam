using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged.Staves
{
    public abstract class Staff : Ranged
    {
        protected Staff(string nameIn, int damage,  Color levelIn, ItemSprite imageIn, Vector2 ItemSize) : 
            base(nameIn, damage, ATTACKSPEED.MODERATE, levelIn, imageIn, ItemSize, typeof(Caster))
        {
        }
    }
}
