using System;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Helpers
{
    public static class DamageHelper
    {
        public static void DamageTarget(int amount, AnimatedSprite target, Action optionalAction = null)
        {
            if (target == null) return;
            target.Manager.Health = target.Manager.Health + amount < target.Manager.MaxHealth ? target.Manager.Health + amount : target.Manager.MaxHealth;
            if (target.Manager.Health < 0)
            {
                target.Delete();
                optionalAction?.Invoke();
            }
        }
    }
}
