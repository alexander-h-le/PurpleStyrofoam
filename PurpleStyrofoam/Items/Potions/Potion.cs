using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Potions
{
    public abstract class Potion : Item
    {
        protected Potion(string nameIn, Color levelIn, ItemSprite imageIn) : base(nameIn, levelIn, imageIn)
        {
        }

        public abstract string EffectDescription { get; }
    }
}
