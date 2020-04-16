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

        public override void NextMove()
        {
            //Detect Direction
            if (TargetSprite.SpriteRectangle.X < SpriteSource.SpriteRectangle.X && !SpriteSource.West)
            {
                SpriteSource.velocity.X -= 3;
            }
            else if (!SpriteSource.East)
            {
                SpriteSource.velocity.X += 3;
            }

            if (SpriteSource.South && TargetSprite.SpriteRectangle.Bottom < SpriteSource.SpriteRectangle.Y)
            {
                SpriteSource.SpriteRectangle.Y -= 1;
                SpriteSource.velocity.Y -= 500;
            }
        }
    }
}
