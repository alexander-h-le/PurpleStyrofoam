using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Maps;

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
            menuItems.Add(new MenuItem((int)(0.05 * Game.ScreenSize.X), (int) (0.5 * Game.ScreenSize.Y),
                (int)(0.18 * Game.ScreenSize.X), (int)(0.1 * Game.ScreenSize.Y), Game.GameContent.Load<Texture2D>("NewGameButton"))
            {
                Action = () =>
                {
                    RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
                    PlayerController player = new PlayerController(Game.GameContent);
                    TestMap tM = new TestMap(Game.GameContent);
                    RenderHandler.InitiateChange(tM, player,  newX: 100, newY: 300);
                }
            });

        }
    }
}
