﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public abstract class GameObject 
    {
        public abstract void Update();
        public abstract void Draw(SpriteBatch sp);
        public abstract void Load();
    }
}
