using Newtonsoft.Json;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.SaveHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public static class GameSaveHandler
    {
        public static BaseMap LoadMapFromFile()
        {
            throw new NotImplementedException();
        }
        public static PlayerController LoadCharacterFromFile()
        {
            throw new NotImplementedException();
        }
        public static bool SaveCharacterToFile()
        {
            throw new NotImplementedException();
        }
        public static GameSave LoadSave()
        {
            throw new NotImplementedException();
        }
        public static bool CreateSave()
        {
            GameSave newSave = new GameSave();
            newSave.Player = (PlayerController) RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            throw new NotImplementedException();
        }
    }
}
