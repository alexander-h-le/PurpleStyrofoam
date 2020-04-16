using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Items;
using PurpleStyrofoam.Items.Weapons;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Managers;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.Maps.Dungeon_Areas;
using PurpleStyrofoam.SaveHandling;
using PurpleStyrofoam.SaveHandling.GameConverters;
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

        static JsonSerializerSettings settings;

        /// <summary>
        /// Starts up GameSaveHandler. Ran along with other initialize methods, but this is only run once.
        /// </summary>
        public static void Initialize()
        {
            string dash = "";
            //Mac
            if (Environment.OSVersion.VersionString.Contains("Unix")) dash = "/";
            //Windows
            else if (Environment.OSVersion.VersionString.Contains("Windows")) dash = "\\";

            PathDirectory = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dash + "PurpleStyrofoamGameFolder").FullName + dash;

            settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters.Add(new GameClassConverter());
            settings.Converters.Add(new ItemConverter());
            settings.Converters.Add(new AnimatedSpriteConverter());
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
        /// Sets the game state based on the save given
        /// </summary>
        /// <param name="SaveName">Absolute path of the save to be loaded</param>
        /// <returns>Returns whether or not the file could be found and successfully loaded</returns>
        public static bool LoadSave(string SaveName)
        {
            // RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
            try
            {
                using (StreamReader sr = File.OpenText(PathDirectory + SaveName))
                {
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                        GameSave save = jsonSerializer.Deserialize<GameSave>(reader);
                        PlayerController chara = new PlayerController(save.player);
                        ((PlayerManager)chara.Manager).Class.AddSpriteSource(chara);
                        ((PlayerManager)chara.Manager).CurrentSave = SaveName;

                        RenderHandler.InitiateChange(LoadMapFromName(save.ActiveMap), chara, save.PlayerPosition.ToPoint());
                    }
                }
            } catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
            return true;
        }

        public static GameSave RetrieveGameSave(string SaveName)
        {
            GameSave gSave;
            try
            {
                using (StreamReader sr = File.OpenText(PathDirectory + SaveName))
                {
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
                        GameSave save = jsonSerializer.Deserialize<GameSave>(reader);
                        PlayerController chara = new PlayerController(save.player);
                        ((PlayerManager)chara.Manager).Class.AddSpriteSource(chara);
                        ((PlayerManager)chara.Manager).CurrentSave = SaveName;
                        gSave = save;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
            return gSave;
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


            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + SaveName, JsonConvert.SerializeObject(newSave, settings));
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
            GAMESTATE prevGameState = RenderHandler.CurrentGameState;
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;

            GameSave newSave = new GameSave();
            newSave.PlayerPosition = Position;
            newSave.ActiveMap = map.GetType().Namespace + "." + map.GetType().Name;
            newSave.player = (PlayerManager) player.Manager;
            newSave.player.Class = targetClass;
            newSave.player.Inventory = new InventoryManager();
            newSave.player.EquippedWeapon = equippedWeapon;
            newSave.player.Inventory.Items[0] = equippedWeapon;

            // ------------------------------------------------------------------------------------------------

            File.WriteAllText(PathDirectory + SaveName, JsonConvert.SerializeObject(newSave, settings));
            RenderHandler.CurrentGameState = prevGameState;

            // ------------------------------------------------------------------------------------------------

            return true;
        }

        public static int GetNextSaveID()
        {
            int ID = 0;
            foreach (String f in Directory.GetFiles(PathDirectory))
            {
                int fileID;
                try { fileID = int.Parse(Path.GetFileName(f)); } catch { fileID = 0; }
                if (ID < fileID) ID = fileID;
            }
            return ++ID;
        }
    }
}
