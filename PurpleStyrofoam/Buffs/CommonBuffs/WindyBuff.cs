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
    public class WindyBuff : Buff
    {
        TimerHelper timer;
        public WindyBuff(int dur, int lvl, AnimatedSprite target) : base("Windy", dur, lvl, "It's too windy to control!", texture: TextureHelper.Blank(Color.Tan))
        {
            Random rand = new Random();
            OnStart = () =>
            {
                if (timer == null)
                {
                    timer = new TimerHelper(
                        GameMathHelper.TimeToFrames(1f - (1f / 20f * (lvl - 1f))),
                        () => {
                            int bounds = lvl * 100;
                            int x = rand.Next(-bounds, bounds);
                            int y = rand.Next(-bounds, bounds);
                            if (y > 0 && target.North) y = 0;
                            else if (y < 0 && target.South) y = 0;

                            if (x > 0 && target.East) x = 0;
                            else if (x < 0 && target.West) x = 0;

                            if (y < -target.terminalVelocity.Y) y = (int) -target.terminalVelocity.Y;
                            else if (y > target.terminalVelocity.Y) y = (int) target.terminalVelocity.Y;

                            if (x < -target.terminalVelocity.X) x = (int)-target.terminalVelocity.X;
                            else if (x > target.terminalVelocity.X) x = (int)target.terminalVelocity.X;

                            target.velocity += new Vector2(x, y);
                        }
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
