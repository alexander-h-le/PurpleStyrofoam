using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Items;
using PurpleStyrofoam.Helpers;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using Microsoft.Xna.Framework.Input;

namespace PurpleStyrofoam.Managers
{
    public class InventoryManager : IPopUp
    {
        private Item[] Inventory;
        readonly Rectangle location;

        public InventoryManager()
        {
            if (Inventory == null)
            {
                Inventory = new Item[100];
                Inventory[(int)ITEMSLOTS.WEAPON] = null; // Weapon
                Inventory[(int)ITEMSLOTS.ARMOR_HELMET] = null; // Helmet
                Inventory[(int)ITEMSLOTS.ARMOR_CHESTPLATE] = null; // Chestplate
                Inventory[(int)ITEMSLOTS.ARMOR_LEGGINGS] = null; // Leggings
                Inventory[(int)ITEMSLOTS.ARMOR_BOOTS] = null; // Boots
                Inventory[(int)ITEMSLOTS.COMPANION] = null; // Companion

                location = new Rectangle(
                    (int)RenderHandler.ScreenOffset.X, // X Position
                    (int)RenderHandler.ScreenOffset.Y, // Y Position
                    100, // Width
                    100  // Height
                    );
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(TextureHelper.Blank(Color.Black), location, Color.White);
            foreach (Item i in Inventory)
            {
                i.Sprite.Draw(sp);
            }
        }

        const int ItemSideLength = 50;
        public void Update()
        {
            int xPos = location.X;
            int yPos = location.Y;
            foreach (Item i in Inventory)
            {
                i.Sprite.ItemRectangle = new Rectangle(xPos, yPos, ItemSideLength, ItemSideLength);
                if (xPos < location.Right) xPos += ItemSideLength;
                else
                {
                    yPos += ItemSideLength;
                    xPos = location.X;
                }
            }
        }

        public void AddToInventory(Item i)
        {
            Inventory.Add(i);
        }

        public bool ShouldOpen()
        {
            return KeyHelper.CheckTap(Keys.Tab);
        }

        public bool ShouldClose()
        {
            return KeyHelper.CheckTap(Keys.Tab);
        }

        public void ActionAtPosition(Vector2 pos)
        {
            
        }

        public enum ITEMSLOTS
        {
            WEAPON = 0,
            ARMOR_HELMET = 1,
            ARMOR_CHESTPLATE = 2,
            ARMOR_LEGGINGS = 3,
            ARMOR_BOOTS = 4,
            COMPANION = 5
        }
    }
}
