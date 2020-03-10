using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public abstract class GameClass
    {
        /// <summary>
        /// Method that would be called when the E key is pressed.
        /// </summary>
        public abstract void EAction();

        /// <summary>
        /// Method that would be called when the right mouse button is pressed.
        /// </summary>
        public abstract void RClick();

        /// <summary>
        /// When unable to provide a sprite during <code>AnimatedSprite</code> initialization, use this method immediately after.
        /// </summary>
        /// <param name="spIN">The sprite to be inserted into class</param>
        public abstract void AddSpriteSource(AnimatedSprite spIN);

        /// <summary>
        /// Returns the double value of the cooldown. It is not in the 100% format, but rather 0.99 format.
        /// </summary>
        /// <returns></returns>
        public abstract double CooldownPercentage();

        public abstract void Update();
    }
}
