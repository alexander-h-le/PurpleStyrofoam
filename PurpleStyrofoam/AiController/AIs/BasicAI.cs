using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.AiController.AIs
{
    class BasicAI : AIBase
    {
        private AnimatedSprite TargetSprite;
        public BasicAI(AnimatedSprite source, AnimatedSprite Target)
        {
            TargetSprite = Target;
            SpriteSource = source;
        }
        public BasicAI(AnimatedSprite Target)
        {
            TargetSprite = Target;
        }

        bool inAir = false;
        public override void NextMove()
        {
            //Detect Direction
            if (TargetSprite.X < SpriteSource.X && !SpriteSource.West)
            {
                SpriteSource.X -= 3;
            }
            else if (!SpriteSource.East)
            {
                SpriteSource.X += 3;
            }

            if (SpriteSource.South && TargetSprite.SpriteRectangle.Bottom < SpriteSource.Y)
            {
                SpriteSource.Y -= 1;
                SpriteSource.velocity.Y -= 500;
            }
        }
    }
}
