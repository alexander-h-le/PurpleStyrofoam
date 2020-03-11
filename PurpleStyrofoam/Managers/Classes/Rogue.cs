using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.AiController.AIs;
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
    public class Rogue : GameClass
    {
        PlayerController SourceSprite;
        public double MaxCooldown = 2.50;
        double CurrentCooldown = 0.0;
        public Rogue()
        {
            SourceSprite = Game.PlayerCharacter;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SourceSprite = (PlayerController)spIN;
        }

        public override double CooldownPercentage()
        {
            return CurrentCooldown / MaxCooldown;
        }

        public override void EAction()
        {
            if (CurrentCooldown != MaxCooldown) return;
            Game.PlayerCharacter.velocity = BasicProjectile.GenerateVelocityVector(new Vector2(Game.PlayerCharacter.SpriteRectangle.X, Game.PlayerCharacter.SpriteRectangle.Y),
                MouseHandler.mousePos, 1000, yModifier: 3.0);
            CurrentCooldown -= 0.01;
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
