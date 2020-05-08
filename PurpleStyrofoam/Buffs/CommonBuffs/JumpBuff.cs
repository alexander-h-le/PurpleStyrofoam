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
    public class JumpBuff : Buff
    {
        public JumpBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Jump Boost", dur, lvl, "You feel as light as a feather", texture: TextureHelper.Blank(Color.Blue))
        {
            OnStart = () => { target.terminalVelocity.Y = AnimatedSprite.DefaultTerminalVelocity.Y + (lvl * 100); };
            OnEnd = () => { target.terminalVelocity.Y = AnimatedSprite.DefaultTerminalVelocity.Y; };
        }
    }
}
