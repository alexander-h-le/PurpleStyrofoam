using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons
{
    public abstract class Weapon : Item
    {

        private Type[] EquippableClass;

        protected Weapon(string nameIn, int damage, ATTACKSPEED atkspd, RARITY levelIn, ItemSprite imageIn, params Type[] EquippableBy) : base(nameIn, levelIn, imageIn)
        {
            Damage = damage;
            AttackSpeed = atkspd;
            EquippableClass = EquippableBy;
        }

        public int Damage { get; } // Get the item's damage.
        public abstract void OnLeftClick(); // Will be called when player left-clicks
        public abstract void OnQAbility(); // Will be called when player presses Q
        public ATTACKSPEED AttackSpeed { get; set; }
        public override void OnInventoryUse()
        {
            foreach (Type t in EquippableClass)
            {
                if (t == Game.PlayerManager.Class.GetType()) Game.PlayerManager.Inventory.SwitchItems(this, Game.PlayerManager.Inventory.Items[0]);
            }
        }
    }

    public enum ATTACKSPEED
    {
        INSANELY_SLOW, VERY_SLOW, SLOW, MODERATE, FAST, VERY_FAST, INSANELY_FAST
    }
}
