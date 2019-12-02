using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.AiController.AIs
{
    public abstract class AIBase
    {
        public abstract void AddDamage(int amount);
        public abstract void NextMove();
        public abstract void SupplyAI(AnimatedSprite source);
    }
}
