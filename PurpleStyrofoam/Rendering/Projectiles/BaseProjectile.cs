﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    class BaseProjectile : Projectile
    {
        Rectangle ProjectileRectangle;
        Vector2 Velocity;
        Vector2 TerminalVelocity;
        Texture2D Texture;
        AnimatedSprite SpriteSource;
        readonly int projectiledamage = 10;
        public BaseProjectile(int x, int y, int width, int height, Vector2 velocityIn, float ang, Texture2D texture, AnimatedSprite source)
        {
            ProjectileRectangle = new Rectangle(x, y, width, height);
            Velocity = velocityIn;
            RenderHandler.allProjectiles.Add(this);
            Texture = texture;
            SpriteSource = source;
            Angle = ang;
        }

        const double ProjectileSpeedModifier = 40;
        public static Vector2 GenerateVelocityVector(int xSource, int ySource, int xTarget, int yTarget)
        {
            double x1 = xTarget - xSource;
            double y1 = yTarget - ySource;
            double hypotenuse = Math.Sqrt((x1 * x1) + (y1 * y1));
            double angle = Math.Acos(x1 / hypotenuse);
            double normalizedHypotenuse = ProjectileSpeedModifier;
            double yResult = Math.Sin(angle) * normalizedHypotenuse;
            double xResult = Math.Cos(angle) * normalizedHypotenuse;
            return new Vector2((float) (xResult), (float) (yTarget > ySource ? yResult : -yResult));
            
        }
        public static Vector2 GenerateVelocityVector(Vector2 source, Vector2 target)
        {
            double x1 = target.X - source.X;
            double y1 = target.Y - source.Y;
            double hypotenuse = Math.Sqrt((x1 * x1) + (y1 * y1));
            double angle = Math.Acos(x1 / hypotenuse);
            double normalizedHypotenuse = ProjectileSpeedModifier;
            double yResult = Math.Sin(angle) * normalizedHypotenuse;
            double xResult = Math.Cos(angle) * normalizedHypotenuse;
            return new Vector2((float)(xResult), (float)(target.Y > source.Y ? yResult : -yResult));

        }
        public override void DetectCollision()
        {
            foreach (MapObject map in RenderHandler.selectedMap.ActiveLayer)
            {
                if (ProjectileRectangle.Intersects(map.MapRectangle))
                {
                    ProjectileAction(SpriteSource, map);
                    break;
                }
            }
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                Rectangle spriteRect = new Rectangle(sprite.X, sprite.Y, sprite.Texture.Width, sprite.Texture.Height);
                if (sprite != SpriteSource && ProjectileRectangle.Intersects(spriteRect))
                {
                    ProjectileAction(SpriteSource, sprite);
                    break;
                }
            }
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
            target.AI.AddDamage(-projectiledamage);
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
