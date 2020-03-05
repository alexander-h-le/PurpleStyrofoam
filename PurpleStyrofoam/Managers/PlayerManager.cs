using PurpleStyrofoam.Items;
using PurpleStyrofoam.Items.Weapons;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Managers;
using PurpleStyrofoam.Managers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam
{
    public class PlayerManager : IManager
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set;}
        public int MaxMana { get; set; }
        public string CurrentSave { get; set; }
        public GameClass Class { get; set; }
        public InventoryManager Inventory { get; }
        public Weapon EquippedWeapon { get; set; }

        public static string basePlayerSpriteName = "playerSprite";
        public static string movingPlayerSprite = "playerSpriteMoving";
        public static string jumpingDPlayerSprite = "playerSpriteJumpingDynamic";
        public static string jumpingSPlayerSprite = "playerSpriteJumpingStatic";

        public void AddDamage(int amount)
        {
            Health += Health + amount < MaxHealth ? amount : 0;
        }
        public void AddMana(int amount)
        {
            Mana += Mana + amount < MaxMana ? amount : 0;
        }
        public PlayerManager()
        {
            MaxHealth = 100;
            Health = MaxHealth;
            MaxMana = 100;
            Mana = MaxMana;
        }
    }
}
