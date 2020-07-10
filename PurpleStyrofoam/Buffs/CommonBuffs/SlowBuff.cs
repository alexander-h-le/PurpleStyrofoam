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
    public class SlowBuff : Buff
    {
        public SlowBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Slow", dur, lvl, "You're heavy", texture: TextureHelper.Blank(Color.DarkBlue))
        {
            OnStart = () => { target.terminalVelocity.X -= lvl * 40; };
            OnEnd = () => { target.terminalVelocity.X = AnimatedSprite.DefaultTerminalVelocity.X; };
        }
    }
}
