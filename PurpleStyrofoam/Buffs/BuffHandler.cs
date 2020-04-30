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
                b.During?.Invoke();
                b.Duration--;
                if (b.Duration <= 0) EndingBuffs.Add(b);
            }

            foreach (Buff b in EndingBuffs)
            {
                b.OnEnd?.Invoke();
                CurrentBuffs.Remove(b);
            }

            EndingBuffs.Clear();
        }

        public bool AddBuff(Buff b)
        {
            if (CurrentBuffs.Contains(b))
            {
                Buff current = CurrentBuffs.Find((x) => x.Equals(b));
                if (current != null) if (current.Duration < b.Duration) current.Duration = b.Duration;
                return false;
            }
            else CurrentBuffs.Add(b);
            b.OnStart?.Invoke();
            return true;
        }

        public bool RemoveBuff(Buff b)
        {
            if (CurrentBuffs.Contains(b))
            {
                EndingBuffs.Add(b);
                return true;
            }
            else return false;
        }
        public bool RemoveBuff(string name)
        {
            Buff b = CurrentBuffs.Find((x) => x.Name.Equals(name));
            if (b != null)
            {
                EndingBuffs.Add(b);
                return true;
            }
            else return false;
        }
    }
}
