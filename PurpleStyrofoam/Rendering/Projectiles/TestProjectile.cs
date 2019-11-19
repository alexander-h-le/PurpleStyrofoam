using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    class TestProjectile : Projectile
    {
        Rectangle ProjectileRectangle;
        Vector2 Velocity;
        Vector2 TerminalVelocity;
        Texture2D Texture;
        public TestProjectile(int x, int y, int width, int height, Vector2 velocityIn, Texture2D texture)
        {
            ProjectileRectangle = new Rectangle(x, y, width, height);
            Velocity = velocityIn;
            RenderHandler.allProjectiles.Add(this);
            Texture = texture;
        }
        public override void DetectCollision()
        {
        }

        public float Angle = 0.0f;
        public Vector2 Origin;
        public float Scale = 1.0f;
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(Texture, new Vector2(ProjectileRectangle.X, ProjectileRectangle.Y), new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, Angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
        }

        public override void Update()
        {
            ProjectileRectangle.X += (int) Velocity.X;
            ProjectileRectangle.Y += (int) Velocity.Y;
        }
    }
}
