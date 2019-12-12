using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public abstract class GameClass
    {
        public abstract void EAction();
        public abstract void RClick();
        public abstract void AddSpriteSource(AnimatedSprite spIN);
    }
}
