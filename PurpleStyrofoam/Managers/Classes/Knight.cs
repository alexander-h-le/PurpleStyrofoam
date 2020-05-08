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
        const double MaxCooldown = 3.0;
        double CurrentCooldown = 0.0;
        public Knight()
        {
            SpriteSource = Game.PlayerCharacter;
        }

        public override void AddSpriteSource(AnimatedSprite spIn)
        {
            SpriteSource = (PlayerController) spIn;
        }

        public override double CooldownPercentage()
        {
            return CurrentCooldown / MaxCooldown;
        }

        public override void EAction()
        {
            if (projectile == null)
            {
                if (CurrentCooldown != MaxCooldown) return;
                projectile = new KnightHook(
                    SpriteSource.SpriteRectangle,
                    Projectile.GenerateVelocityVector(new Vector2(SpriteSource.SpriteRectangle.X, SpriteSource.SpriteRectangle.Y),
                    MouseHandler.mousePos),0f, SpriteSource);
                CurrentCooldown -= 0.01;
            } else if (projectile.Connected == KnightHook.CONNECTION.MAP)
            {
                Vector2 newVect = Projectile.GenerateVelocityVector(new Vector2(SpriteSource.SpriteRectangle.X, SpriteSource.SpriteRectangle.Y),
                    new Vector2(projectile.ProjRect.X, projectile.ProjRect.Y), 1500);
                SpriteSource.velocity.X = newVect.X;
                SpriteSource.velocity.Y = newVect.Y;
                projectile.Delete();
                projectile = null;
            } else if (projectile.Connected == KnightHook.CONNECTION.SPRITE)
            {
                Vector2 newVect = Projectile.GenerateVelocityVector(projectile.ConnectedSprite.SpriteRectangle.Location.ToVector2(),
                    SpriteSource.SpriteRectangle.Location.ToVector2(), 2000);
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

        public override void Update()
        {
            if (!(CurrentCooldown == MaxCooldown))
            {
                if (CurrentCooldown < 0) CurrentCooldown = MaxCooldown;
                else CurrentCooldown -= 0.016;
            }
        }
    }
}
