using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        PlayerController player;
        string Errors;

        public NewSaveMenu()
        {
            menuItems = new List<MenuItem>();
            Errors = "";
        }

        public void ActionAtPosition(MouseState mouse)
        {
            foreach (MenuItem i in menuItems) if (i.MenuRectangle.Intersects(new Rectangle(mouse.X, mouse.Y, 1, 1))) i.Action();
        }

        public void Draw(SpriteBatch sp)
        {
            sp.DrawString(Game.GameContent.Load<SpriteFont>(SpriteTextureHelper.Fonts.Default),
                "Chosen Class: " + (targetClass != null ? targetClass.GetType().Name : "None") , new Vector2(LeftStart + 10, (int)(0.2 * Game.ScreenSize.Y)), Color.White);
            sp.DrawString(Game.GameContent.Load<SpriteFont>(SpriteTextureHelper.Fonts.Default), Errors,
                new Vector2(LeftStart + (int)(0.13 * Game.ScreenSize.X) + 10, (int)(0.7 * Game.ScreenSize.Y)), Color.Wheat);
            foreach (MenuItem i in menuItems) i.Draw(sp);
        }

        Point IconSize = new Point((int)(0.13 * Game.ScreenSize.X), (int)(0.05 * Game.ScreenSize.Y));
        int LeftStart = (int)(0.025 * Game.ScreenSize.X);
        public void Initialize()
        {
            player = new PlayerController(new PlayerManager());

            // Caster Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.3 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.basePlayerSpriteName))
            {
                Action = () =>
                {
                    targetClass = new Caster();
                }
            });

            // Knight Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.4 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingDPlayerSprite))
            {
                Action = () =>
                {
                    targetClass = new Knight();
                }
            });

            // Manipulator Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.5 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingSPlayerSprite))
            {
                Action = () =>
                {
                    targetClass = new Manipulator();
                }
            });

            // Rogue Class Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.6 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.movingPlayerSprite))
            {
                Action = () =>
                {
                    targetClass = new Rogue();
                }
            });

            // Create Save Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.7 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(SpriteTextureHelper.Sprites.TestImage))
            {
                Action = () =>
                {
                    string saveID = GameSaveHandler.GetNextSaveID().ToString();
                    if (targetClass is Knight)
                        GameSaveHandler.CreateSave(saveID, player, new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else if (targetClass is Caster)
                        GameSaveHandler.CreateSave(saveID, player, new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else if (targetClass is Manipulator)
                        GameSaveHandler.CreateSave(saveID, player, new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else if (targetClass is Rogue)
                        GameSaveHandler.CreateSave(saveID, player, new Vector2(100, 100), new CathedralRuinsFBoss(), targetClass, new Flight());
                    else
                    {
                        Errors = "Choose a class!";
                        return;
                    }
                    Debug.WriteLine(saveID);
                    GameSaveHandler.LoadSave(saveID);
                }
            });

            // Back Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.8 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(SpriteTextureHelper.Sprites.EnemySprite))
            {
                Action = () =>
                {
                    MenuHandler.ActiveFullScreenMenu = new GameStartMenu();
                }
            });
        }
    }
}
