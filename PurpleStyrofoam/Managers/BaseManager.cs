using PurpleStyrofoam.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    public abstract class BaseManager
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public abstract int Damage { get; set; }
    }
}
