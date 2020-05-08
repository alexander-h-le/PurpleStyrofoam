using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public abstract class Projectile
    {
        public abstract void Draw(SpriteBatch sp);
        public abstract void ProjectileAction(AnimatedSprite source, AnimatedSprite target);
        public abstract void ProjectileAction(AnimatedSprite source, MapObject target);
        public abstract void DetectCollision();
        public abstract void Update();
        public void Delete()
        {
            RenderHandler.purgeProjectiles.Add(this);
            
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
    }
}
