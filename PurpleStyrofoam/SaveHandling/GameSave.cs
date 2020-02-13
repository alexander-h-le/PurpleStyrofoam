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
    /// <summary>
    /// Class that holds game information. Frequently serialized to JSON
    /// </summary>
    public class GameSave
    {
        /// <summary>
        /// Serializable player information in the form of a PlayerManager
        /// </summary>
        public PlayerManager player;
        public string ActiveMap;
        public string EquippedWeapon;
        public string ActiveClass;
        public Vector2 PlayerPosition;
    }
}
