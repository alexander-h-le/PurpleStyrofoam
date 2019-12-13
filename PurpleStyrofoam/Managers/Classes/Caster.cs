using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public class Caster : GameClass
    {
        PlayerController SpriteSource;
        CasterEMove t;
        public Caster(AnimatedSprite sp)
        {
            SpriteSource = (PlayerController)sp;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SpriteSource = (PlayerController)spIN;
        }

        public override void EAction()
        {
            if (t == null)
            {
                t = new CasterEMove(SpriteSource.SpriteRectangle.Right, SpriteSource.SpriteRectangle.Top,
                BasicProjectile.GenerateVelocityVector(SpriteSource.SpriteRectangle.Center.X, SpriteSource.SpriteRectangle.Center.Y, (int)MouseHandler.mousePos.X, (int)MouseHandler.mousePos.Y,5));
                t.Velocity.Y = 0;
                RenderHandler.allProjectiles.Add(t);
            } else
            {
                if (t.CollidingR)
                {
                    SpriteSource.X = t.ProjRect.Right - SpriteSource.SpriteRectangle.Width;
                    SpriteSource.Y = t.ProjRect.Y;
                } else
                {
                    SpriteSource.X = t.ProjRect.X;
                    SpriteSource.Y = t.ProjRect.Y;
                }
                t.Delete();
                t = null;
            }
        }

        public override void RClick()
        {
            throw new NotImplementedException();
        }
    }
}
