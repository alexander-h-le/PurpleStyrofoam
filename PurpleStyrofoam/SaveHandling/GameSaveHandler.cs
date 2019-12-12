using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.SaveHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public static class GameSaveHandler
    {
        public static string PathDirectory;
        public static void Initialize()
        {
            PathDirectory = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GameTestFolder").FullName + "\\";
        }
        public static BaseMap LoadMapFromName(string name)
        {
            Type type = Type.GetType(name);
            return (BaseMap) Activator.CreateInstance(type);
        }
        public static GameClass LoadClass(string name)
        {
            Type type = Type.GetType(name);
            return (GameClass) Activator.CreateInstance(type);
        }
        public static void LoadSave(string Path)
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            try
            {
                using (StreamReader sr = File.OpenText(Path))
                {
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        var settings = new JsonSerializerSettings();
                        settings.Converters.Add(new GameClassConverter());
                        JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                        GameSave save = jsonSerializer.Deserialize<GameSave>(reader);
                        PlayerController chara = new PlayerController(Game.GameContent, manager: save.player);
                        save.player.Class.AddSpriteSource(chara);
                        RenderHandler.InitiateChange(LoadMapFromName(save.ActiveMap), chara , (int)save.PlayerPosition.X, (int)save.PlayerPosition.Y);
                    }
                }
            } catch (FileNotFoundException e)
            {
                CreateSave(new PlayerController(Game.GameContent), new Vector2(0,0), new TestMap());
                LoadSave(Path);
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
        }
        public static bool CreateSave()
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            GameSave newSave = new GameSave();
            PlayerController _player = (PlayerController)RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            newSave.PlayerPosition = new Vector2(_player.X, _player.Y);
            newSave.player = (PlayerManager) _player.Manager;
            newSave.ActiveMap = RenderHandler.selectedMap.GetType().Namespace + "." +  RenderHandler.selectedMap.GetType().Name;

            Debug.WriteLine($"Position: {newSave.PlayerPosition}\nPlayerInfo: {newSave.player.Health}\nMapName: {newSave.ActiveMap}");

            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + "TestDocument.json", JsonConvert.SerializeObject(newSave));
            using (StreamWriter sw = File.CreateText(PathDirectory + "TestDocument.json"))
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new GameClassConverter());
                JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                jsonSerializer.Serialize(sw, newSave);
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;

            // ------------------------------------------------------------------------------------------------

            return true;
        }
        public static bool CreateSave(PlayerController player, Vector2 Position, BaseMap map)
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            GameSave newSave = new GameSave();
            newSave.PlayerPosition = Position;
            newSave.ActiveMap = map.GetType().Namespace + "." + map.GetType().Name;
            newSave.player = new PlayerManager();

            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + "TestDocument.json", JsonConvert.SerializeObject(newSave));
            using (StreamWriter sw = File.CreateText(PathDirectory + "TestDocument.json"))
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new GameClassConverter());
                JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                jsonSerializer.Serialize(sw, newSave);
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;

            // ------------------------------------------------------------------------------------------------

            return true;
        }
    }
}
