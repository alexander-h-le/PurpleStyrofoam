using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.AiController.AIs
{
    public class PlayerControlledAI : AIBase
    {
        private int Health;
        public PlayerControlledAI()
        {
            Health = 100;
        }

        public override void NextMove()
        {
        }

        public override void SupplyAI(AnimatedSprite source)
        {
        }
    }
}
