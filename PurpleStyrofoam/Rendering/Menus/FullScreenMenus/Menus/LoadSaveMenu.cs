using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.SaveHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus.FullScreenMenus.Menus
{
    class LoadSaveMenu : IFullMenu
    {

        List<MenuItem> saves;

        public LoadSaveMenu()
        {
            saves = new List<MenuItem>();
        }

        public void ActionAtPosition(MouseState mouse)
        {
            Rectangle m = new Rectangle(mouse.X, mouse.Y, 1, 1);
            foreach (MenuItem i in saves)
            {
                if (m.Intersects(i.MenuRectangle)) i.Action();
            }
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (MenuItem i in saves)
            {
                i.Draw(sp);
            }
        }

        public void Initialize()
        {
            int LeftStart = (int)(0.05 * Game.ScreenSize.X);
            int YLevel = (int)(0.1 * Game.ScreenSize.Y);
            int i = 0;
            foreach (string f in Directory.GetFiles(GameSaveHandler.PathDirectory))
            {
                GameSave save = GameSaveHandler.RetrieveGameSave(Path.GetFileName(f));
                string gClass = save.player.Class.GetType().Name;
                string DisplayText = Path.GetFileName(f) + " : " + gClass;
                Vector2 DisplaySize = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default).MeasureString(DisplayText);
                saves.Add(new MenuItem(new Rectangle(LeftStart, (++i) * YLevel, (int)DisplaySize.X, (int)DisplaySize.Y), text: DisplayText)
                {
                    Action = () =>
                    {
                        GameSaveHandler.LoadSave(Path.GetFileName(f));
                    }
                });
            }
            saves.Add(new MenuItem(
                new Rectangle(new Point(LeftStart, ++i * YLevel), new Point((int)(0.13 * Game.ScreenSize.X), (int)(0.05 * Game.ScreenSize.Y))),
                Game.GameContent.Load<Texture2D>(TextureHelper.Sprites.EnemySprite), "Back")
            {
                Action = () =>
                {
                    MenuHandler.ActiveFullScreenMenu = new GameStartMenu();
                }
            });
        }
    }
}
