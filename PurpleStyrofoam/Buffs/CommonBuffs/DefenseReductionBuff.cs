using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs.CommonBuffs
{
    public class DefenseReductionBuff : Buff
    {
        public DefenseReductionBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Defense Reduction", dur, lvl, "Your grows thinner", texture: TextureHelper.Blank(Color.DarkOrange))
        {
        }
    }
}
