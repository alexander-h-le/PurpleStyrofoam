using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    class DefaultManager : BaseManager
    {
        public DefaultManager(int damage = 0)
        {
            MaxHealth = 100;
            Health = MaxHealth;

            Damage = damage;
        }

        public override int Damage { get; set; }
    }
}
