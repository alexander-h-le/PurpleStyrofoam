using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.AiController.AIs
{
    public abstract class AIBase
    {
        protected AnimatedSprite SpriteSource { get; set; }
        public int Health { get; set; }
        public virtual void AddDamage(int amount)
        {
            Health += amount;
        }
        public abstract void NextMove();
        public virtual void SupplyAI(AnimatedSprite source)
        {
            SpriteSource = source;
        }
        public virtual void Delete()
        {
            RenderHandler.purgeSprites.Add(SpriteSource);
        }
    }
}
