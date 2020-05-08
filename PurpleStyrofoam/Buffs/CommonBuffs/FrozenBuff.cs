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
    public class FrozenBuff : Buff
    {
        public FrozenBuff(int dur, AnimatedSprite target) : base("Frozen", dur, 0, "You can't move", texture: TextureHelper.Blank(Color.DodgerBlue))
        {
            OnStart = () =>
            {
                target.terminalVelocity = new Vector2();
                target.velocity = new Vector2();
            };

            OnEnd = () =>
            {
                target.terminalVelocity = AnimatedSprite.DefaultTerminalVelocity;
            };
        }
    }
}
