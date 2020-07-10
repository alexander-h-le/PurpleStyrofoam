using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs
{
    public class BuffHandler
    {
        public List<Buff> CurrentBuffs { get; }
        public BuffHandler()
        {
            CurrentBuffs = new List<Buff>();
        }

        public void Update()
        {
            for (int i = 0; i < CurrentBuffs.Count; i++)
            {
                Buff b = CurrentBuffs[i];
                b.During?.Invoke();
                b.Duration--;
                if (b.Duration <= 0)
                {
                    b.OnEnd?.Invoke();
                    CurrentBuffs.Remove(b);
                }
            }
        }

        public bool AddBuff(Buff b)
        {
            if (CurrentBuffs.Contains(b))
            {
                Buff current = CurrentBuffs.Find((x) => x.Equals(b));
                if (current != null)
                {
                    if (current.Level < b.Level)
                    {
                        CurrentBuffs[CurrentBuffs.IndexOf(current)] = b;
                        b.UpdateFrom(current);
                    }

                    if (current.Duration < b.Duration) current.Duration = b.Duration;
                }
                return false;
            }
            else
            {
                CurrentBuffs.Add(b);
                b.OnStart?.Invoke();
            }
            return true;
        }

        public bool RemoveBuff(Buff b)
        {
            if (CurrentBuffs.Contains(b))
            {
                b.OnEnd?.Invoke();
                CurrentBuffs.Remove(b);
                return true;
            }
            else return false;
        }
        public bool RemoveBuff(string name)
        {
            Buff b = CurrentBuffs.Find((x) => x.Name.Equals(name));
            if (b != null)
            {
                b.OnEnd?.Invoke();
                CurrentBuffs.Remove(b);
                return true;
            }
            else return false;
        }

        public bool RemoveBuff(Type t)
        {
            Buff b = CurrentBuffs.Find( (x) => x.GetType() == t );
            if (b != null)
            {
                b.OnEnd?.Invoke();
                CurrentBuffs.Remove(b);
                return true;
            }
            else return false;
        }

        public bool HasBuff(Type t)
        {
            foreach (Buff b in CurrentBuffs)
            {
                if (b.GetType() == t) return true;
            }
            return false;
        }
        public bool HasBuff(string t)
        {
            foreach (Buff b in CurrentBuffs)
            {
                if (b.Name.Equals(t)) return true;
            }
            return false;
        }
        public bool HasBuff(Buff t)
        {
            foreach (Buff b in CurrentBuffs)
            {
                if (b.Equals(t)) return true;
            }
            return false;
        }
    }
}
