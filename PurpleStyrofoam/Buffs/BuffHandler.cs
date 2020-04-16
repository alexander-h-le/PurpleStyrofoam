using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs
{
    public class BuffHandler
    {
        public List<Buff> CurrentBuffs { get; }
        List<Buff> EndingBuffs;
        public BuffHandler()
        {
            CurrentBuffs = new List<Buff>();
            EndingBuffs = new List<Buff>();
        }

        public void Update()
        {
            foreach (Buff b in CurrentBuffs)
            {
                b.During();
                b.Duration--;
                if (b.Duration <= 0) EndingBuffs.Add(b);
            }

            foreach (Buff b in EndingBuffs)
            {
                b.OnEnd();
                CurrentBuffs.Remove(b);
            }

            EndingBuffs.Clear();
        }

        public bool AddBuff(Buff b)
        {
            if (CurrentBuffs.Contains(b)) return false;
            else CurrentBuffs.Add(b);
            b.OnStart();
            return true;
        }
    }
}
