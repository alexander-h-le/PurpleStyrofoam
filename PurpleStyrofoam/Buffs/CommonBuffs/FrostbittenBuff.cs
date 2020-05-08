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
    public class FrostbittenBuff : Buff
    {
        AnimatedSprite Target;
        TimerHelper timer;
        public FrostbittenBuff(int dur, int lvl, AnimatedSprite target) : base("Frostbitten", dur, lvl, "You're really, really cold", texture: TextureHelper.Blank(Color.DarkBlue))
        {
            Target = target;
            OnStart = () =>
            {
                if (timer == null)
                {
                    target.terminalVelocity.X -= lvl * 30;
                    timer = new TimerHelper(
                        GameMathHelper.TimeToFrames(1f - (1f / 100f * (lvl - 1f))),
                        () => { Target.AddHealthIgnoreInvincibility(-(1 + (lvl))); }
                        );
                }
            };
            OnEnd = () =>
            {
                target.terminalVelocity.X = AnimatedSprite.DefaultTerminalVelocity.X;
                timer?.Delete();
                timer = null;
            };
        }
    }
}
