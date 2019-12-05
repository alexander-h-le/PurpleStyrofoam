using PurpleStyrofoam.Managers;
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

        public void AddDamage(int amount)
        {
            Health += amount;
        }
        public PlayerManager()
        {
            Health = 0;
        }
    }
}
