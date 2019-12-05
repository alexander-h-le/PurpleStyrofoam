﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers
{
    class DefaultManager : IManager
    {
        public int Health { get; set; }

        public void AddDamage(int amount)
        {
            Health += amount;
        }
    }
}