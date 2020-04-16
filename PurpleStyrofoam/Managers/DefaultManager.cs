using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    class DefaultManager : BaseManager
    {

        public DefaultManager()
        {
            MaxHealth = 100;
            Health = MaxHealth;
        }
    }
}
