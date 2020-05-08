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

        public override void DetectCollision()
        {
            if (CollisionDetection.DetectCollisionMap(ProjectileRectangle, out _))
                Delete();

            AnimatedSprite temp;
            if (CollisionDetection.DetectCollisionSprite(SpriteSource, ProjectileRectangle, out temp))
                if (temp != null) ProjectileAction(SpriteSource, temp);
        }

        public float Angle = 0.0f;
        public float Scale = 1.0f;
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(Texture, new Vector2(ProjectileRectangle.X, ProjectileRectangle.Y), Texture.Bounds, 
                Color.White, Angle, new Vector2(), Scale, SpriteEffects.None, 1.0f);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
            target.AddHealth(-projectiledamage);
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
