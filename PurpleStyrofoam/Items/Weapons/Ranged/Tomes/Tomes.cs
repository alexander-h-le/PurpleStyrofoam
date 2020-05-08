using Microsoft.Xna.Framework;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged.Tomes
{
    public abstract class Tomes : Ranged
    {
        protected Tomes(string nameIn, int damage, Color levelIn, ItemSprite imageIn, Vector2 ItemSize) :
            base(nameIn, damage, ATTACKSPEED.INSANELY_SLOW, levelIn, imageIn, ItemSize, typeof(Caster))
        {
        }
    }
}
