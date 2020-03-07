using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Rendering.Menus;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus;

namespace PurpleStyrofoam.Rendering.Menus.PopUpMenu
{
    public class ExitMenuPopUp : IPopUp
    {
        List<MenuItem> menuItems { get; set; }
        public bool Open { get; set; }

        Rectangle PopUpRect;
        const int SizeX = 500;
        const int SizeY = 500;
        public ExitMenuPopUp()
        {
            Open = false;
            menuItems = new List<MenuItem>();
        }
        public void ActionAtPosition(Vector2 pos)
        {
            Rectangle checkRect = new Rectangle((int)pos.X,(int)pos.Y, 2,2);
            foreach (MenuItem i in menuItems)
            {
                if (i.MenuRectangle.Intersects(checkRect)) i.Action();break;
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Game.GameContent.Load<Texture2D>("playerSpriteJumpingDynamic"),
                PopUpRect, 
                Color.Brown);
            foreach(MenuItem i in menuItems)
            {
                i.Draw(sp);
            }
        }

        public bool ShouldOpen()
        {
            if (!Open && KeyHelper.CheckTap(Keys.Escape))
            {
                PopUpRect = new Rectangle((int)RenderHandler.ScreenOffset.X + ((int)Game.ScreenSize.X / 2) - (SizeX/2), 
                    (int)RenderHandler.ScreenOffset.Y + ((int)Game.ScreenSize.Y / 2) - (SizeY/2), 
                    SizeX, SizeY);
                menuItems.Clear();

                //Exit Button
                menuItems.Add(new MenuItem(new Rectangle(PopUpRect.X, PopUpRect.Bottom, PopUpRect.Width, 50),
                    Game.GameContent.Load<Texture2D>(PlayerManager.movingPlayerSprite), "Exit")
                {
                    Action = () =>
                    {
                        RenderHandler.CurrentGameState = GAMESTATE.MAINMENU;
                        GameSaveHandler.CreateSave(( (PlayerManager) Game.PlayerCharacter.Manager).CurrentSave);
                        MenuHandler.ActiveFullScreenMenu = new GameStartMenu();
                        Open = false;
                    }
                });
                Open = true;
                return true;
            }
            return false;
        }

        public void Update()
        {
        }

        public bool ShouldClose()
        {
            if (Open && KeyHelper.CheckTap(Keys.Escape))
            {
                Open = false;
                return true;
            }
            return false;
        }
    }
}
