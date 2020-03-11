using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using PurpleStyrofoam.Helpers;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    class BasicProjectile : Projectile
    {
        Rectangle ProjectileRectangle;
        Vector2 Velocity;
        Texture2D Texture;
        AnimatedSprite SpriteSource;
        readonly int projectiledamage = 1;
        public BasicProjectile(Rectangle rect, Vector2 velocityIn, float ang, Texture2D texture, AnimatedSprite source)
        {
            ProjectileRectangle = rect;
            Velocity = velocityIn;
            RenderHandler.allProjectiles.Add(this);
            Texture = texture;
            SpriteSource = source;
            Angle = ang;
        }

        const double ProjectileSpeedModifier = 40;
        public static Vector2 GenerateVelocityVector(Vector2 source, Vector2 target,
            double SpeedModifier = ProjectileSpeedModifier, double xModifier = 1.0, double yModifier = 1.0)
        {
            double x1 = target.X - source.X;
            double y1 = target.Y - source.Y;
            double hypotenuse = Math.Sqrt((x1 * x1) + (y1 * y1));
            double angle = Math.Acos(x1 / hypotenuse);
            double normalizedHypotenuse = SpeedModifier;
            double yResult = Math.Sin(angle) * normalizedHypotenuse * yModifier;
            double xResult = Math.Cos(angle) * normalizedHypotenuse * xModifier;
            return new Vector2((float)(xResult), (float)(target.Y > source.Y ? yResult : -yResult));

        }
        public override void DetectCollision()
        {
            if (CollisionDetection.DetectCollisionMap(ProjectileRectangle))
            {
                Delete();
            }
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                if (sprite != SpriteSource && ProjectileRectangle.Intersects(sprite.SpriteRectangle))
                {
                    ProjectileAction(SpriteSource, sprite);
                    break;
                }
            }
        }

        public float Angle = 0.0f;
        public float Scale = 1.0f;
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(Texture, new Vector2(ProjectileRectangle.X, ProjectileRectangle.Y), new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, Angle, new Vector2(), Scale, SpriteEffects.None, 1.0f);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
            DamageHelper.DamageTarget(-projectiledamage, target);
            Delete();
        }

        public override void Update()
        {
            ProjectileRectangle.X += (int) Velocity.X;
            ProjectileRectangle.Y += (int) Velocity.Y;
            DetectCollision();
        }

        public override void ProjectileAction(AnimatedSprite source, MapObject target)
        {
            Delete();
            
        }
    }
}
