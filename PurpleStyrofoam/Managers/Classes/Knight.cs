using Microsoft.Xna.Framework;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Projectiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public class Knight : GameClass
    {
        public KnightHook projectile;
        PlayerController SpriteSource;
        public Knight(AnimatedSprite source)
        {
            SpriteSource = (PlayerController)source;
        }
        public override void EAction()
        {
            if (projectile == null)
            {
                projectile = new KnightHook(SpriteSource.X, SpriteSource.Y, (int)SpriteSource.SpriteRectangle.Width, (int)SpriteSource.SpriteRectangle.Height,
                    BasicProjectile.GenerateVelocityVector(SpriteSource.X, SpriteSource.Y, (int)MouseHandler.mousePos.X, (int)MouseHandler.mousePos.Y),0f, SpriteSource
                    );
            } else if (projectile.Connected == KnightHook.CONNECTION.MAP)
            {
                Vector2 newVect = BasicProjectile.GenerateVelocityVector(SpriteSource.X, SpriteSource.Y, projectile.ProjRect.X, projectile.ProjRect.Y, 1500);
                SpriteSource.velocity.X = newVect.X;
                SpriteSource.velocity.Y = newVect.Y;
                projectile.Delete();
                projectile = null;
            } else if (projectile.Connected == KnightHook.CONNECTION.SPRITE)
            {

            }
        }
    }
}
