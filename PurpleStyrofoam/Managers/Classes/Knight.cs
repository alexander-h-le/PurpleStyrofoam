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
        public Knight()
        {
            SpriteSource = Game.PlayerCharacter;
        }

        public override void AddSpriteSource(AnimatedSprite spIn)
        {
            SpriteSource = (PlayerController) spIn;
        }

        public override void EAction()
        {
            if (projectile == null)
            {
                projectile = new KnightHook(
                    SpriteSource.SpriteRectangle,
                    BasicProjectile.GenerateVelocityVector(new Vector2(SpriteSource.SpriteRectangle.X, SpriteSource.SpriteRectangle.Y),
                    MouseHandler.mousePos),0f, SpriteSource);
            } else if (projectile.Connected == KnightHook.CONNECTION.MAP)
            {
                Vector2 newVect = BasicProjectile.GenerateVelocityVector(new Vector2(SpriteSource.SpriteRectangle.X, SpriteSource.SpriteRectangle.Y),
                    new Vector2(projectile.ProjRect.X, projectile.ProjRect.Y), 1500);
                SpriteSource.velocity.X = newVect.X;
                SpriteSource.velocity.Y = newVect.Y;
                projectile.Delete();
                projectile = null;
            } else if (projectile.Connected == KnightHook.CONNECTION.SPRITE)
            {
                Vector2 newVect = BasicProjectile.GenerateVelocityVector(new Vector2(projectile.ConnectedSprite.SpriteRectangle.X, projectile.ConnectedSprite.SpriteRectangle.Y),
                    new Vector2(SpriteSource.SpriteRectangle.X, SpriteSource.SpriteRectangle.Y), 2000);
                projectile.ConnectedSprite.velocity.X = newVect.X;
                projectile.ConnectedSprite.velocity.Y = newVect.Y;
                projectile.Delete();
                projectile = null;
            }
        }

        public override void RClick()
        {
            throw new NotImplementedException();
        }
    }
}
