using Microsoft.Xna.Framework.Graphics;
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

        public int Duration;

        public Texture2D Texture;

        public Buff(string name, int dur, Action start, Action during, Action end, Texture2D texture = null)
        {
            Name = name;
            Duration = dur;
            Texture = texture;
            OnStart = start;
            During = during;
            OnEnd = end;
        }

        public Action OnStart;
        public Action During;
        public Action OnEnd;

        public bool Equals(Buff other)
        {
            return Name.Equals(other.Name);
        }
    }
}
