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
    public class HealthBoostBuff : Buff
    {
        public HealthBoostBuff(int dur, int lvl, AnimatedSprite target) : base("Health Boost", dur, lvl, "You feel tougher", texture: TextureHelper.Blank(Color.Pink))
        {
            OnStart = () => { target.Manager.Health += (int)(target.Manager.MaxHealth * (0.05 * lvl)); };
            OnEnd = () =>
            {
                if (target.Manager.Health > target.Manager.MaxHealth) target.Manager.Health = target.Manager.MaxHealth;
            };
        }
    }
}
