using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    public interface IManager
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
        void AddDamage(int amount);
    }
}
