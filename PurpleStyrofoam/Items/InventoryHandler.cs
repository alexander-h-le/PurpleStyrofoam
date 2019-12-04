using PurpleStyrofoam.Items.Armors;
using PurpleStyrofoam.Items.Miscellaneous_Items.Companions;
using PurpleStyrofoam.Items.Weapons;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items
{
    public static class InventoryHandler
    {
        public static ArrayList Inventory = new ArrayList();
        public static Weapon EquippedWeapon { get; set; }
        public static Armor[] EquippedArmor = new Armor[4];
        public static Armor[] EquippedCosmeticArmor = new Armor[4];
        public static Companion EquippedCompanion { get; set; }
        public static int Currency { get; set; }
        public static void AddToInventory(Item input)
        {
            if (typeof(Weapon).IsInstanceOfType(input))
            {

            } else if (typeof(Armor).IsInstanceOfType(input)){

            } else if (typeof(Companion).IsInstanceOfType(input)){

            } else {

            }
        }
    }
}
