using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs.CommonBuffs
{
    public class SpeedBuff : Buff
    {
        public SpeedBuff(int dur, int level, AnimatedSprite target) : 
            base("Speed", dur, level, "You're light on your feet", texture: TextureHelper.Blank(Color.LightBlue))
        {
            Level = level;
            OnStart = () => { target.terminalVelocity.X += 100 * Level; };
            OnEnd = () => { target.terminalVelocity.X = AnimatedSprite.DefaultTerminalVelocity.X; };
        }
    }
}
