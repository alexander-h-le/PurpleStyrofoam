using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items
{
    public class ItemStack : Item
    {
        public List<Item> items;
        public override int ID => items[0].ID;

        public override string Description => items[0].Description;

        public ItemStack(List<Item> its) : base(its[0].Name, its[0].Rarity, its[0].Sprite)
        {
            items = its;
        }

        public bool AddToStack(Item i)
        {
            Debug.WriteLine(items[0].Name);
            Debug.WriteLine(i.Name);
            if (items[0].Equals(i)) items.Add(i);
            else return false;
            return true;
        }

        public override void OnInventoryUse()
        {
            items[0].OnInventoryUse();
        }
    }
}
