using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public class Rogue : GameClass
    {
        PlayerController SourceSprite;
        public Rogue(AnimatedSprite inp)
        {
            SourceSprite = (PlayerController) inp;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SourceSprite = (PlayerController)spIN;
        }

        public override void EAction()
        {
            SourceSprite.velocity = BasicProjectile.GenerateVelocityVector(new Vector2(SourceSprite.SpriteRectangle.X, SourceSprite.SpriteRectangle.Y),
                MouseHandler.mousePos, 1500);
        }

        public override void RClick()
        {
            throw new NotImplementedException();
        }
    }
}
