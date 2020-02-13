using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.Maps.Dungeon_Areas;
using PurpleStyrofoam.Rendering.Menus;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;

namespace PurpleStyrofoam.Rendering
{
    /// <summary>
    /// The main engine of the game. Handles all update method calls, all draw method calls, and holds all object lists.
    /// </summary>
    static class RenderHandler
    {
        public static List<AnimatedSprite> allCharacterSprites { get; private set; }
        public static List<ItemSprite> allItemSprites { get; private set; }
        public static List<Projectile> allProjectiles { get; private set; }

        /// <summary>
        /// The projectiles to be deleted. Needed in order to circumvent editing lists mid-iteration.
        /// </summary>
        public static List<Projectile> purgeProjectiles { get; private set; }
        /// <summary>
        /// The sprites to be deleted. Needed in order to circumvent editing lists mid-iteration.
        /// </summary>
        public static List<AnimatedSprite> purgeSprites { get; private set; }
        public static BaseMap selectedMap;
        private static PlayerController savedPlayer;
        /// <summary>
        /// Active GameState. Used to differentiate pausing and active gameplay
        /// </summary>
        public static GAMESTATE CurrentGameState { get; set; }

        /// <summary>
        /// Initializes RenderHandler at the beginning of the game.
        /// </summary>
        public static void Initialize()
        {
            allCharacterSprites = new List<AnimatedSprite>();
            allItemSprites = new List<ItemSprite>();
            allProjectiles = new List<Projectile>();
            purgeProjectiles = new List<Projectile>();
            purgeSprites = new List<AnimatedSprite>();
            MenuHandler.Initialize();
            CurrentGameState = GAMESTATE.MAINMENU;
        }

        /// <summary>
        /// Changes the game state. Primarily used to change the map.
        /// </summary>
        /// <param name="newMap">The map to be changed to</param>
        /// <param name="player">The player information to be handed in</param>
        /// <param name="newX">The new player position</param>
        /// <param name="newY">The new player position</param>
        public static void InitiateChange(BaseMap newMap, PlayerController player, int newX = 0, int newY = 0)
        {
            allCharacterSprites.Clear();
            allItemSprites.Clear();
            allProjectiles.Clear();
            savedPlayer = player;
            selectedMap = newMap;
            ObjectMapper.MapObjects(selectedMap);
            allCharacterSprites.Add(player);
            allItemSprites = new List<ItemSprite>();
            if ( ((PlayerManager) player.Manager).EquippedWeapon != null) allItemSprites.Add(((PlayerManager)player.Manager).EquippedWeapon.Sprite);
            player.SpriteRectangle.X = newX;
            player.SpriteRectangle.Y = newY;
            PlayerInfoUI.Initialize();

            Game.PlayerCharacter = player;
        }

        /// <summary>
        /// Changes the game state. Primarily used to change the map. Allows for custom sprites and items to be inputted.
        /// </summary>
        /// <param name="newMap">The new map to be changed to</param>
        /// <param name="player">The player information to be handed in</param>
        /// <param name="newSprites">The custom list of sprites to be used</param>
        /// <param name="newItems">The custom list of items to be used</param>
        /// <param name="newX">The new player postiion</param>
        /// <param name="newY">The new player position</param>
        public static void InitiateChange(BaseMap newMap, PlayerController player, List<AnimatedSprite> newSprites, List<ItemSprite> newItems, int newX = 0, int newY = 0)
        {
            allCharacterSprites.Clear();
            allItemSprites.Clear();
            allProjectiles.Clear();
            savedPlayer = player;
            selectedMap = newMap;
            ObjectMapper.MapObjects(selectedMap);
            allCharacterSprites = newSprites;
            if (!allCharacterSprites.Contains(player)) allCharacterSprites.Add(player);
            allItemSprites = newItems;
            player.SpriteRectangle.X = newX;
            player.SpriteRectangle.Y = newY;
            PlayerInfoUI.Initialize();

            Game.PlayerCharacter = player;
        }

        /// <summary>
        /// Changes the game state. Primarily used to change the map. Allows for custom sprites to be inputted.
        /// </summary>
        /// <param name="newMap">The new map to be changed to</param>
        /// <param name="player">The player information to be handed in</param>
        /// <param name="newSprites">The custom list of sprites to be used</param>
        /// <param name="newX">The new player position</param>
        /// <param name="newY">The new player position</param>
        public static void InitiateChange(BaseMap newMap, PlayerController player, List<AnimatedSprite> newSprites, int newX = 0, int newY = 0)
        {
            allCharacterSprites.Clear();
            allItemSprites.Clear();
            allProjectiles.Clear();
            savedPlayer = player;
            selectedMap = newMap;
            ObjectMapper.MapObjects(selectedMap);
            allCharacterSprites = newSprites;
            if (!allCharacterSprites.Contains(player)) allCharacterSprites.Add(player);
            allItemSprites = new List<ItemSprite>();
            player.SpriteRectangle.X = newX;
            player.SpriteRectangle.Y = newY;
            PlayerInfoUI.Initialize();

            Game.PlayerCharacter = player;
        }



        /// <summary>
        /// Excess MapObjects being created that weren't included in original map definition
        /// </summary>
        public static List<MapObject> extras = new List<MapObject>();

        /// <summary>
        /// Primary update method. Runs update method calls when needed.
        /// </summary>
        /// <seealso cref="CurrentGameState"/>
        public static void Update()
        {
            switch (CurrentGameState)
            {
                case GAMESTATE.ACTIVE:
                    MenuHandler.CheckKeys();
                    foreach (AnimatedSprite sprite in allCharacterSprites)
                    {
                        sprite.Update();
                        ObjectMapper.DeleteSpriteObject(sprite);
                        ObjectMapper.AddSpriteObject(sprite);
                    }
                    foreach (Projectile item in allProjectiles)
                    {
                        item.Update();
                    }
                    if (purgeProjectiles.Count != 0) DeleteProjectiles();
                    if (purgeSprites.Count != 0) DeleteSprites();
                    if (KeyHelper.CheckCombination(Keys.K, Keys.LeftShift))
                        ((PlayerManager)savedPlayer.Manager).Class = new Knight(savedPlayer);
                    if (KeyHelper.CheckCombination(Keys.F, Keys.LeftShift))
                        ((PlayerManager)savedPlayer.Manager).EquippedWeapon = new Flight(Game.GameContent);
                    PlayerInfoUI.Update();
                    break;
                case GAMESTATE.MAINMENU:
                    break;
                case GAMESTATE.PAUSED:
                    MenuHandler.CheckKeys();
                    MenuHandler.Update();
                    break;
                default:
                    throw new NotSupportedException("Game has entered an invalid gamestate: " + CurrentGameState);
            }
        }

        /// <summary>
        /// Removes sprites listed in the <c>purgeSprites</c> list
        /// </summary>
        public static void DeleteSprites()
        {
            foreach (AnimatedSprite sprite in purgeSprites)
            {
                allCharacterSprites.Remove(sprite);
                ObjectMapper.DeleteSpriteObject(sprite);
            }
            purgeSprites.Clear();
        }

        /// <summary>
        /// Removes projectiles listed in the <c>purgeProjectiles</c> list
        /// </summary>
        public static void DeleteProjectiles()
        {
            foreach (Projectile proj in purgeProjectiles)
            {
                allProjectiles.Remove(proj);
            }
            purgeProjectiles.Clear();
        }

        public static Vector2 ScreenOffset = new Vector2(0, 0);
        private static int XOffset = (int) Game.ScreenSize.X/2;
        private static int YOffset = (int) Game.ScreenSize.Y/2;

        /// <summary>
        /// Primary Draw method. Calls all draw methods required for game
        /// </summary>
        /// <param name="sp">The <c>SpriteBatch</c> given from the Game class.</param>
        public static void Draw(SpriteBatch sp)
        {
            switch (CurrentGameState)
            {
                case GAMESTATE.ACTIVE:
                    int xMove = ScreenOffset.X < selectedMap.maxBounds.Left ? selectedMap.maxBounds.Left :
                        ScreenOffset.X > selectedMap.maxBounds.Right ? selectedMap.maxBounds.Right : (-savedPlayer.SpriteRectangle.X) + XOffset;
                    int yMove = ScreenOffset.Y < selectedMap.maxBounds.Top ? selectedMap.maxBounds.Top :
                        ScreenOffset.Y > selectedMap.maxBounds.Bottom ? selectedMap.maxBounds.Bottom : (-savedPlayer.SpriteRectangle.Y) + YOffset;
                    sp.Begin(SpriteSortMode.Deferred, transformMatrix: Matrix.CreateTranslation(xMove, yMove, 0));
                    ScreenOffset.X = (ScreenOffset.X < selectedMap.maxBounds.Left && xMove < 0) 
                        || (ScreenOffset.X > selectedMap.maxBounds.Right && xMove > 0) ? 
                        ScreenOffset.X : (savedPlayer.SpriteRectangle.X) - XOffset;
                    ScreenOffset.Y = (ScreenOffset.Y < selectedMap.maxBounds.Top && yMove < 0)
                        || (ScreenOffset.Y > selectedMap.maxBounds.Bottom && yMove > 0) ? 
                        ScreenOffset.Y : (savedPlayer.SpriteRectangle.Y) - YOffset;
                    if (selectedMap != null) selectedMap.DrawBackground(sp);
                    if (selectedMap != null) selectedMap.Draw(sp);
                    foreach (MapObject i in  extras)
                    {
                        i.Draw(sp);
                    }
                    foreach (AnimatedSprite item in allCharacterSprites)
                    {
                        item.Draw(sp);
                    }
                    foreach (Projectile item in allProjectiles)
                    {
                        item.Draw(sp);
                    }
                    foreach (ItemSprite item in allItemSprites)
                    {
                        item.Draw(sp);
                    }
                    if (selectedMap != null) selectedMap.DrawForeground(sp);
                    PlayerInfoUI.Draw(sp);
                    break;
                case GAMESTATE.MAINMENU:
                    sp.Begin(SpriteSortMode.Deferred, transformMatrix: Matrix.CreateTranslation(0, 0, 0));
                    if (MenuHandler.ActiveFullScreenMenu != null) MenuHandler.DrawFullScreenMenu(sp);
                    break;
                case GAMESTATE.PAUSED:
                    sp.Begin(SpriteSortMode.Deferred, transformMatrix: Matrix.CreateTranslation((-savedPlayer.SpriteRectangle.X) + XOffset, (-savedPlayer.SpriteRectangle.Y) + YOffset, 0));
                    if (selectedMap != null) selectedMap.DrawBackground(sp);
                    if (selectedMap != null) selectedMap.Draw(sp);
                    foreach (AnimatedSprite item in allCharacterSprites)
                    {
                        item.Draw(sp);
                    }
                    foreach (Projectile item in allProjectiles)
                    {
                        item.Draw(sp);
                    }
                    foreach (ItemSprite item in allItemSprites)
                    {
                        item.Draw(sp);
                    }
                    if (selectedMap != null) selectedMap.DrawForeground(sp);
                    if (MenuHandler.ActivePopUp != null) MenuHandler.DrawPopUpMenu(sp);
                    break;
                default:
                    throw new NotSupportedException("Game has entered an invalid gamestate: " + CurrentGameState);
            }
            sp.End();
        }

        /// <summary>
        /// Adds a sprite to <c>allItemSprites</c>
        /// </summary>
        /// <param name="input"></param>
        public static void Add(ItemSprite input)
        {
            allItemSprites.Add(input);
        }

        /// <summary>
        /// Adds a sprite to <c>allSprites</c>
        /// </summary>
        /// <param name="input"></param>
        public static void Add(AnimatedSprite input)
        {
            allCharacterSprites.Add(input);
        }

        /// <summary>
        /// Gives the angle in radians to make object look at certain point
        /// </summary>
        /// <param name="source">The position of the origin</param>
        /// <param name="xIn">The target X position</param>
        /// <param name="yIn">The target Y position</param>
        /// <returns>Returns angle in radians</returns>
        public static float LookAtXY(Vector2 source, int xIn, int yIn)
        {
            double deltaX = xIn - source.X;
            double deltaY = yIn - source.Y;
            return (float) Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Gives the angle in radians to make object look at the mouse
        /// </summary>
        /// <param name="source">The position of the origin</param>
        /// <returns>Returns angle in radians</returns>
        public static float LookAtMouse(Vector2 source)
        {
            double deltaX = MouseHandler.mousePos.X - source.X;
            double deltaY = MouseHandler.mousePos.Y - source.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Gives the angle in radians to make an <c>ItemSprite</c> look at a sprite
        /// </summary>
        /// <param name="objectIn">The <c>ItemSprite</c> source</param>
        /// <param name="objectToSee">The <c>AnimatedSprite</c> to look at</param>
        /// <returns>Returns the angle in radians</returns>
        public static float LookAtSprite(ItemSprite objectIn, AnimatedSprite objectToSee)
        {
            double deltaX = objectToSee.SpriteRectangle.X - objectIn.ItemRectangle.X;
            double deltaY = objectToSee.SpriteRectangle.Y - objectIn.ItemRectangle.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }

        /// <summary>
        /// Give the angle in radians to make an <c>ItemSprite</c> look at a sprite
        /// </summary>
        /// <param name="objectIn">The <c>ItemSprite</c> source</param>
        /// <param name="characterSpriteName">The name of the sprite to be looked at</param>
        /// <returns>Returns the angle in radians</returns>
        public static float LookAtSprite(ItemSprite objectIn, string characterSpriteName)
        {
            double deltaX = allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).SpriteRectangle.X - objectIn.ItemRectangle.X;
            double deltaY = allCharacterSprites.Find(x => x.GetType().Name == characterSpriteName).SpriteRectangle.Y - objectIn.ItemRectangle.Y;
            return (float)Math.Atan2(deltaY, deltaX);
        }
    }

    /// <summary>
    /// Data type for game's current state
    /// </summary>
    enum GAMESTATE
    {
        MAINMENU, ACTIVE, PAUSED
    }
}
