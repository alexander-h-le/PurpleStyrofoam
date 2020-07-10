using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs
{
    public class Buff : IEquatable<Buff> 
    {
        public string Name;

        public string Description;

        public int Duration;

        public Texture2D Texture;

        public int Level;

        public Buff(string name, int dur, int lvl = 0, string desc = "", Action start = null, Action during = null, Action end = null, Texture2D texture = null)
        {
            Name = name + " " + GameMathHelper.NumToRomanNumeral(lvl);
            Duration = dur;
            Texture = texture;
            OnStart = start;
            During = during;
            OnEnd = end;
            Level = lvl;
            Description = desc;
        }

        public virtual void UpdateFrom(Buff b) { }

        public Action OnStart;
        public Action During;
        public Action OnEnd;

        public bool Equals(Buff other)
        {
            if (GetType() == other.GetType()) return true;
            return Name.Equals(other.Name);
        }
    }
}
