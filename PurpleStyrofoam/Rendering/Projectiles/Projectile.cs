using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    abstract class Projectile
    {
        public abstract void Draw(SpriteBatch sp);
        public abstract void ProjectileAction(AnimatedSprite source, AnimatedSprite target);
        public abstract void DetectCollision();
        public abstract void Update();
    }
}
