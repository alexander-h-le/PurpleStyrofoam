using Microsoft.Xna.Framework;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.SaveHandling
{
    public class GameSave
    {
        public PlayerController Player;
        public BaseMap ActiveMap;
        public Vector2 PlayerPosition;
    }
}
