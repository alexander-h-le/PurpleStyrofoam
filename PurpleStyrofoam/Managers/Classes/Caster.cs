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
    public class Caster : GameClass
    {
        public SpellType CurrentSpellType;

        PlayerController SpriteSource;
        CasterEMove t;
        private const double MaxCooldown = 3.0;
        private double CurrentCooldown = 0.0;
        public Caster()
        {
            SpriteSource = Game.PlayerCharacter;
            CurrentSpellType = SpellType.FIRE;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SpriteSource = (PlayerController)spIN;
        }

        public override double CooldownPercentage()
        {
            return CurrentCooldown / MaxCooldown;
        }

        public override void EAction()
        {
            if (t == null)
            {
                if (CurrentCooldown != MaxCooldown) return;
                t = new CasterEMove(SpriteSource.SpriteRectangle.Right - (SpriteSource.SpriteRectangle.Width/2), SpriteSource.SpriteRectangle.Top,
                BasicProjectile.GenerateVelocityVector(new Vector2(SpriteSource.SpriteRectangle.Center.X, SpriteSource.SpriteRectangle.Center.Y), MouseHandler.mousePos, 7));
                // t.Velocity.Y = 0;
                RenderHandler.allProjectiles.Add(t);
                CurrentCooldown -= 0.01;
            } else
            {
                if (t.Colliding[2])
                {
                    SpriteSource.SpriteRectangle.X = t.ProjRect.Right - SpriteSource.SpriteRectangle.Width;
                    SpriteSource.SpriteRectangle.Y = t.ProjRect.Y + 2;
                } else
                {
                    SpriteSource.SpriteRectangle.X = t.ProjRect.X;
                    SpriteSource.SpriteRectangle.Y = t.ProjRect.Y + 2;
                }
                t.Delete();
                t = null;
            }
        }

        public override void RClick()
        {
            CurrentSpellType++;
            if ((int)CurrentSpellType > 3) CurrentSpellType = 0;
        }

        public override void Update()
        {
            if (!(CurrentCooldown == MaxCooldown))
            {
                if (CurrentCooldown < 0) CurrentCooldown = MaxCooldown;
                else CurrentCooldown -= 0.016;
            }
        }

        public enum SpellType
        {
            FIRE,ICE,WIND,EARTH
        }
    }
}
