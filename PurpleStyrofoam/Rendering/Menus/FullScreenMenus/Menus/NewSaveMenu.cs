using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Items.Weapons.Melee.Daggers;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps.Dungeon_Areas;

namespace PurpleStyrofoam.Rendering.Menus.FullScreenMenus.Menus
{
    public class NewSaveMenu : IFullMenu
    {
        List<MenuItem> menuItems;
        GameClass targetClass;

        public NewSaveMenu()
        {
            menuItems = new List<MenuItem>();
        }

        public void ActionAtPosition(MouseState mouse)
        {
            foreach (MenuItem i in menuItems) if (i.MenuRectangle.Intersects(new Rectangle(mouse.X, mouse.Y, 1, 1))) i.Action();
        }

        public void Draw(SpriteBatch sp)
        {
            sp.DrawString(Game.GameContent.Load<SpriteFont>(""), "Chosen Class: " + targetClass.GetType().Name, new Vector2(100, 100), Color.Black);
            foreach (MenuItem i in menuItems) i.Draw(sp);
        }

        Point IconSize = new Point((int)(0.13 * Game.ScreenSize.X), (int)(0.05 * Game.ScreenSize.Y));
        int LeftStart = (int)(0.025 * Game.ScreenSize.X);
        public void Initialize()
        {
            // Caster Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.3 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.basePlayerSpriteName))
            {
                Action = () =>
                {
                }
            });

            // Knight Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.4 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingDPlayerSprite))
            {
                Action = () =>
                {
                }
            });

            // Manipulator Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.5 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingSPlayerSprite))
            {
                Action = () =>
                {
                }
            });

            // Rogue Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.6 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.movingPlayerSprite))
            {
                Action = () =>
                {
                }
            });

            // Back Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.7 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(SpriteTextureHelper.EnemySprite))
            {
                Action = () =>
                {
                    MenuHandler.ActiveFullScreenMenu = new GameStartMenu();
                }
            });

            // Create Save Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.7 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(SpriteTextureHelper.TestImage))
            {
                Action = () =>
                {
                    if (targetClass is Knight)
                        GameSaveHandler.CreateSave("abc", new PlayerController(new PlayerManager()), new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else if (targetClass is Caster)
                        GameSaveHandler.CreateSave("abc", new PlayerController(new PlayerManager()), new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else if (targetClass is Manipulator)
                        GameSaveHandler.CreateSave("abc", new PlayerController(new PlayerManager()), new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Fortz());
                    else if (targetClass is Rogue)
                        GameSaveHandler.CreateSave("abc", new PlayerController(new PlayerManager()), new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Fortz());
                    else
                        throw new Exception("Class not found");
                }
            });
        }
    }
}
