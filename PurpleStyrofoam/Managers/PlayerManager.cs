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
    public class PlayerManager : BaseManager
    {
        public int Mana { get; set;}
        public int MaxMana { get; set; }
        public string CurrentSave { get; set; }
        public GameClass Class { get; set; }
        public InventoryManager Inventory { get; set; }

        public Weapon EquippedWeapon { 
            get
            {
                if (Inventory.Items[0] is BlankItem) return null;
                return (Weapon) Inventory.Items[0];
            } 
            set
            {
                if (value != null) Inventory.Items[0] = value;
                else Inventory.Items[0] = new BlankItem();
            }
        }

        public override int Damage { get => 0; set { } }

        public static string basePlayerSpriteName = "playerSprite";
        public static string movingPlayerSprite = "playerSpriteMoving";
        public static string jumpingDPlayerSprite = "playerSpriteJumpingDynamic";
        public static string jumpingSPlayerSprite = "playerSpriteJumpingStatic";

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
