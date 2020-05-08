using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    public class CasterEMove : Projectile
    {
        Texture2D text = Game.GameContent.Load<Texture2D>("playerSpriteJumpingDynamic");
        public Rectangle ProjRect;
        public Vector2 Velocity;
        public bool[] Colliding;
        public CasterEMove(int x, int y, Vector2 vel)
        {
            ProjRect = new Rectangle(x,y,25,25);
            Velocity = vel;
            Colliding = new bool[] { false, false, false, false };
        }
        public override void DetectCollision() 
        {
            bool[] ar = CollisionDetection.DetectCollisionArrayMap(ProjRect);
            Colliding = ar;
        }
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(text, ProjRect, text.Bounds, Color.White);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
            throw new NotImplementedException();
        }

        public override void ProjectileAction(AnimatedSprite source, MapObject target)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            DetectCollision();
            if (!Colliding.Contains(true))
            {
                ProjRect.X += (int)Velocity.X;
                ProjRect.Y += (int)Velocity.Y;
                if (Velocity.Y != 0) Velocity.Y += Velocity.Y > 0 ? -0.05f : 0.05f;
                if (Velocity.X != 0) Velocity.X += Velocity.X > 0 ? -0.015f : 0.015f;
            }
        }
    }
}
