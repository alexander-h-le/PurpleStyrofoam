using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Items;
using PurpleStyrofoam.Items.Weapons;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.Maps.Dungeon_Areas;
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
    /// <summary>
    /// Handles saving in the game. A static class much like RenderHandler. 
    /// </summary>
    public static class GameSaveHandler
    {

        /// <summary>
        /// The string directory that holds all the saves in the game.
        /// </summary>
        public static string PathDirectory;

        /// <summary>
        /// Starts up GameSaveHandler. Ran along with other initialize methods, but this is only run once.
        /// </summary>
        public static void Initialize()
        {
            PathDirectory = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GameTestFolder").FullName + "\\";
        }

        /// <summary>
        /// Returns a BaseMap object based on a given string
        /// </summary>
        /// <param name="name">Path of the BaseMap to be created. Format: Namespace.ClassName</param>
        /// <returns>BaseMap object based on the string provided</returns>
        public static BaseMap LoadMapFromName(string name)
        {
            Type type = Type.GetType(name);
            return (BaseMap) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Returns a GameClass based on a given string
        /// </summary>
        /// <param name="name">Path of the GameClass to be created. Format: Namespace.ClassName</param>
        /// <returns>GameClass object based on the string provided</returns>
        public static GameClass LoadClass(string name)
        {
            Type type = Type.GetType(name);
            return (GameClass) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Returns an Item based on a given string
        /// </summary>
        /// <param name="name">Path of the Item to be created. Format: Namespace.ClassName</param>
        /// <returns>Item object based on the string provided</returns>
        public static Item LoadItem(string name)
        {
            Type type = Type.GetType(name);
            return (Item) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Sets the game state based on the save given
        /// </summary>
        /// <param name="Path">Absolute path of the save to be loaded</param>
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
                        if (save.player.Class != null) save.player.Class.AddSpriteSource(chara);
                        else save.player.Class = new Knight(chara);
                        chara.HeldWeapon = (Weapon) LoadItem(save.ActiveWeapon);
                        RenderHandler.InitiateChange(LoadMapFromName(save.ActiveMap), chara , (int)save.PlayerPosition.X, (int)save.PlayerPosition.Y);
                    }
                }
            } catch (FileNotFoundException e)
            {
                CreateSave(new PlayerController(Game.GameContent), new Vector2(200,200), new CathedralRuinsFBoss());
                LoadSave(Path);
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
        }

        /// <summary>
        /// Generates a new save based on the current game state.
        /// Do not run this method when the game is not active.
        /// </summary>
        /// <returns> Returns whether or not if it was successful</returns>
        public static bool CreateSave()
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            GameSave newSave = new GameSave();
            PlayerController _player = (PlayerController)RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            newSave.PlayerPosition = new Vector2(_player.X, _player.Y);
            newSave.player = (PlayerManager) _player.Manager;
            newSave.ActiveMap = RenderHandler.selectedMap.GetType().Namespace + "." +  RenderHandler.selectedMap.GetType().Name;
            if (_player.HeldWeapon != null) newSave.ActiveWeapon = _player.HeldWeapon.GetType().Namespace + "." + _player.HeldWeapon.GetType().Name;
            else newSave.ActiveWeapon = "PurpleStyrofoam.Items.Weapons.Melee.Swords.Flight";

            Debug.WriteLine($"Position: {newSave.PlayerPosition}\nPlayerInfo: {newSave.player.Health}\nMapName: {newSave.ActiveMap}" +
                $"\nClass: {newSave.player.Class}");

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

        /// <summary>
        /// Generates a save based on the given information
        /// </summary>
        /// <param name="player">The current PlayerController being used</param>
        /// <param name="Position">The position of the player</param>
        /// <param name="map">The current active map</param>
        /// <returns>Returns whether or not it was successful</returns>
        public static bool CreateSave(PlayerController player, Vector2 Position, BaseMap map)
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            GameSave newSave = new GameSave();
            newSave.PlayerPosition = Position;
            newSave.ActiveMap = map.GetType().Namespace + "." + map.GetType().Name;
            newSave.player = new PlayerManager();
            if (player.HeldWeapon != null) newSave.ActiveWeapon = player.HeldWeapon.GetType().Namespace + "." + player.HeldWeapon.GetType().Name;
            else newSave.ActiveWeapon = "PurpleStyrofoam.Items.Weapons.Melee.Swords.Flight";

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
