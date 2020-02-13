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
            if (name == null) return new CathedralRuinsFBoss();
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
            if (name == null) return new Knight(RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController")));
            Type type = Type.GetType(name);
            return (GameClass) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Sets the game state based on the save given
        /// </summary>
        /// <param name="SaveName">Absolute path of the save to be loaded</param>
        /// <returns>Returns whether or not the file could be found and successfully loaded</returns>
        public static bool LoadSave(string SaveName)
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            try
            {
                using (StreamReader sr = File.OpenText(PathDirectory + SaveName))
                {
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        var settings = new JsonSerializerSettings();
                        settings.Converters.Add(new GameClassConverter());
                        JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                        GameSave save = jsonSerializer.Deserialize<GameSave>(reader);
                        PlayerController chara = new PlayerController(save.player);
                        ((PlayerManager)chara.Manager).Class.AddSpriteSource(chara);
                        ((PlayerManager)chara.Manager).EquippedWeapon = (Weapon) Activator.CreateInstance(Type.GetType(save.EquippedWeapon));
                        RenderHandler.InitiateChange(LoadMapFromName(save.ActiveMap), chara , (int)save.PlayerPosition.X, (int)save.PlayerPosition.Y);
                    }
                }
            } catch
            {
                return false;
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
            return true;
        }

        /// <summary>
        /// Generates a new save based on the current game state.
        /// Do not run this method when the game is not active.
        /// </summary>
        /// <returns> Returns whether or not if it was successful</returns>
        public static bool CreateSave(string SaveName)
        {
            GAMESTATE prevGameState = RenderHandler.CurrentGameState;
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;

            GameSave newSave = new GameSave();

            PlayerController _player = Game.PlayerCharacter;

            BaseMap map = RenderHandler.selectedMap;

            newSave.PlayerPosition = new Vector2(_player.SpriteRectangle.X, _player.SpriteRectangle.Y);
            newSave.player = (PlayerManager) _player.Manager;
            newSave.ActiveMap = map.GetType().Namespace + "." +  map.GetType().Name;

            Debug.WriteLine($"Position: {newSave.PlayerPosition}\nPlayerInfo: {newSave.player.Health}\nMapName: {newSave.ActiveMap}" +
                $"\nClass: {newSave.player.Class} \nWeapon: {newSave.EquippedWeapon}");

            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + SaveName, JsonConvert.SerializeObject(newSave));
            using (StreamWriter sw = File.CreateText(PathDirectory + SaveName))
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new GameClassConverter());
                JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                jsonSerializer.Serialize(sw, newSave);
            }
            RenderHandler.CurrentGameState = prevGameState;

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
        public static bool CreateSave(string SaveName, PlayerController player, Vector2 Position, BaseMap map, GameClass targetClass, Weapon equippedWeapon)
        {
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            GameSave newSave = new GameSave();
            newSave.PlayerPosition = Position;
            newSave.ActiveMap = map.GetType().Namespace + "." + map.GetType().Name;
            newSave.player = (PlayerManager) player.Manager;
            newSave.EquippedWeapon = equippedWeapon.GetType().Namespace + "." + equippedWeapon.GetType().Name;
            newSave.player.Class = targetClass;

            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + SaveName, JsonConvert.SerializeObject(newSave));
            using (StreamWriter sw = File.CreateText(PathDirectory + SaveName))
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
