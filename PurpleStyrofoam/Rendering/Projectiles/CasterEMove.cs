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
        bool CollidingL;
        bool CollidingR;
        public CasterEMove(int x, int y, Vector2 vel)
        {
            ProjRect = new Rectangle(x,y,25,25);
            Velocity = vel;
            CollidingL = false;
            CollidingR = false;
        }
        public override void DetectCollision() 
        {
            bool[] ar = CollisionDetection.DetectCollisionArrayMap(ProjRect);
            Colliding = ar[2] || ar[3];
        }
        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(text, ProjRect, new Rectangle(0,0,text.Width,text.Height), Color.White);
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
            if (!Colliding)
            {
                ProjRect.X += (int)Velocity.X;
                ProjRect.Y += (int)Velocity.Y;
            }
        }
    }
}
