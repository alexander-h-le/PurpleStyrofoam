using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Maps;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus.Menus;

namespace PurpleStyrofoam.Rendering.Menus.FullScreenMenus
{
    class GameStartMenu : IFullMenu
    {
        private List<MenuItem> menuItems { get; set; }
        public GameStartMenu()
        {
            menuItems = new List<MenuItem>();
        }
        public void ActionAtPosition(MouseState mouse)
        {
            Rectangle mouseSpace = new Rectangle(mouse.X, mouse.Y, 2, 2);
            foreach (MenuItem menu in menuItems)
            {
                if (mouseSpace.Intersects(menu.MenuRectangle))
                {
                    menu.Action();
                }
            }
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (MenuItem menu in menuItems)
            {
                menu.Draw(sp);
            }
        }

        Point IconSize = new Point((int)(0.18 * Game.ScreenSize.X), (int)(0.1 * Game.ScreenSize.Y));
        int LeftStart = (int)(0.05 * Game.ScreenSize.X);
        public void Initialize()
        {
            // New Save Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.3 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>("NewGameButton"))
            {
                Action = () =>
                {
                    MenuHandler.ActiveFullScreenMenu = new NewSaveMenu();
                }
            });

            // Load Save Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.4 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingDPlayerSprite))
            {
                Action = () =>
                {
                    MenuHandler.ActiveFullScreenMenu = new LoadSaveMenu();
                }
            });

            // Settings Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.5 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.jumpingSPlayerSprite))
            {
                Action = () =>
                {
                }
            });

            // Exit Button
            menuItems.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, (int)(0.6 * Game.ScreenSize.Y)), IconSize),
                Game.GameContent.Load<Texture2D>(PlayerManager.movingPlayerSprite))
            {
                Action = () =>
                {
                    Game.ShouldClose = true;
                }
            });
        }
    }
}
