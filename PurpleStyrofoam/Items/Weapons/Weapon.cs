using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons
{
    public abstract class Weapon : Item
    {
        protected Weapon(string nameIn, int damage, ATTACKSPEED atkspd, RARITY levelIn, ItemSprite imageIn) : base(nameIn, levelIn, imageIn)
        {
            Damage = damage;
            AttackSpeed = atkspd;
        }
        public Weapon()
        {

        }

        public int Damage { get; } // Get the item's damage.
        public abstract void OnLeftClick(); // Will be called when player left-clicks
        public abstract void OnQAbility(); // Will be called when player presses Q
        public ATTACKSPEED AttackSpeed { get; set; }
    }

    public enum ATTACKSPEED
    {
        INSANELY_SLOW, VERY_SLOW, SLOW, MODERATE, FAST, VERY_FAST, INSANELY_FAST
    }
}
