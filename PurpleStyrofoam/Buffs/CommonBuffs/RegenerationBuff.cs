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
    public class RegenerationBuff : Buff
    {
        AnimatedSprite Target;
        TimerHelper timer;
        public RegenerationBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Regeneration", dur, lvl, "Your wounds are healing", texture: TextureHelper.Blank(Color.LightPink))
        {
            Target = target;
            OnStart = () => 
            {
                if (timer == null)
                {
                    timer = new TimerHelper(
                        GameMathHelper.TimeToFrames(0.3f - ( 0.3f / 20f * ( lvl - 1f) )),
                        () => { Target.AddHealth(1 + (lvl / 2 )); }
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
