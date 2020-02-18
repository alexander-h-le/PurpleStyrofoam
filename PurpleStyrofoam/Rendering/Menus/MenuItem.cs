using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus
{
    public class MenuItem
    {
        public Rectangle MenuRectangle { get; private set; }
        Texture2D menuTexture;
        string Text;
        bool isText;
        public MenuItem(Rectangle rect, Texture2D texture)
        {
            MenuRectangle = rect;
            menuTexture = texture;
            isText = false;
        }
        public MenuItem(Rectangle rect, string text)
        {
            MenuRectangle = rect;
            isText = true;
            Text = text;
        }
        public Action Action{ get; set;}
        public void Draw(SpriteBatch sp)
        {
            if (isText) sp.DrawString(Game.GameContent.Load<SpriteFont>(SpriteTextureHelper.Fonts.Default), Text, new Vector2(MenuRectangle.X, MenuRectangle.Y), Color.White);
            else sp.Draw(menuTexture, MenuRectangle, new Rectangle(0, 0, menuTexture.Width, menuTexture.Height), Color.White);
        }
    }
}
