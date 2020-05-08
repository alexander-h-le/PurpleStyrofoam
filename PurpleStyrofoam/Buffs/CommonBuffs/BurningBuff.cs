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
    public class BurningBuff : Buff
    {
        AnimatedSprite Target;
        TimerHelper timer;

        public BurningBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Burning", dur, lvl, "You're burning!", texture: TextureHelper.Blank(Color.Red))
        {
            Target = target;
            OnStart = () =>
            {
                if (timer == null)
                {
                    timer = new TimerHelper(
                        GameMathHelper.TimeToFrames(0.3f - (0.3f / 20f * (lvl - 1f))),
                        () => { Target.AddHealthIgnoreInvincibility(-(1 + (lvl / 2))); }
                        );
                }
            };
            OnEnd = () =>
            {
                timer?.Delete();
                timer = null;
            };
        }
    }
}
