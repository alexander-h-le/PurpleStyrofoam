using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public void Initialize()
        {
            menuItems.Add(new MenuItem((int)(0.05 * Game1.ScreenSize.X), (int) (0.5 * Game1.ScreenSize.Y),
                (int)(0.18 * Game1.ScreenSize.X), (int)(0.1 * Game1.ScreenSize.Y), Game1.Contents.Load<Texture2D>("NewGameButton"))
            {
                Action = () =>
                {
                    RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
                }
            });

        }
    }
}
