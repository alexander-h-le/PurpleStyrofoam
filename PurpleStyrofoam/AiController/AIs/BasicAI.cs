using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.AiController.AIs
{
    class BasicAI : AIBase
    {
        private int Health;
        private AnimatedSprite TargetSprite;
        private AnimatedSprite SourceSprite;
        public BasicAI(AnimatedSprite source, AnimatedSprite Target)
        {
            Health = 100;
            TargetSprite = Target;
            SourceSprite = source;
        }
        public BasicAI(AnimatedSprite Target)
        {
            TargetSprite = Target;
        }
        public override void AddDamage(int amount)
        {
            Health += amount;
        }

        public override void NextMove()
        {
            //Detect Direction
            if (TargetSprite.X < SourceSprite.X && !SourceSprite.West)
            {
                SourceSprite.X -= 6;
            }
            else if (!SourceSprite.East)
            {
                SourceSprite.X += 6;
            }
        }

        public override void SupplyAI(AnimatedSprite source)
        {
            SourceSprite = source;
        }
    }
}
