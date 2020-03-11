using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    public class KnightHook : Projectile
    {
        public CONNECTION Connected { get; set; }
        Texture2D text;
        public Rectangle ProjRect;
        Vector2 Velocity;
        AnimatedSprite SpriteSource;
        public AnimatedSprite ConnectedSprite;
        public KnightHook(Rectangle rect, Vector2 velIn, float ang, AnimatedSprite source)
        {
            ProjRect = rect;
            Angle = ang;
            text = Game.GameContent.Load<Texture2D>("testBackground");
            Velocity = velIn;
            SpriteSource = source;
            Connected = CONNECTION.NONE;
            RenderHandler.allProjectiles.Add(this);
        }
        public override void DetectCollision()
        {
            if (CollisionDetection.DetectCollisionSprites(SpriteSource, ProjRect, out ConnectedSprite))
            {
                Connected = CONNECTION.SPRITE;
            } else if (CollisionDetection.DetectCollisionMap(ProjRect, out _))
            {
                Connected = CONNECTION.MAP;
            }
        }

        public float Angle = 0.0f;
        public Vector2 Origin;
        public float Scale = 1.0f;
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(text, new Vector2(ProjRect.X, ProjRect.Y), new Rectangle(0, 0, 50, 50), 
                Color.White, Angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
            throw new NotSupportedException();
        }

        public override void ProjectileAction(AnimatedSprite source, MapObject target)
        {
            throw new NotSupportedException();
        }

        public override void Update()
        {
            if (Connected == CONNECTION.NONE)
            {
                ProjRect.X += (int)Velocity.X;
                ProjRect.Y += (int)Velocity.Y;
                DetectCollision();
            } else if (Connected == CONNECTION.SPRITE)
            {
                ProjRect = ConnectedSprite.SpriteRectangle;
            }
        }
        public enum CONNECTION
        {
            NONE,SPRITE,MAP
        }
    }
}
