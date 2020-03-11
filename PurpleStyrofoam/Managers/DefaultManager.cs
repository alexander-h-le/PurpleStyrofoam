using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    class DefaultManager : BaseManager
    {
        public override int Health { get; set; }
        public override int MaxHealth { get; set; }

        public DefaultManager()
        {
            MaxHealth = 100;
            Health = MaxHealth;
        }
    }
}
