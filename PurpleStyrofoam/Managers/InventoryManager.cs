using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Items;
using PurpleStyrofoam.Helpers;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Menus.PopUpMenu;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using PurpleStyrofoam.Items.Weapons.Melee.Swords;
using PurpleStyrofoam.Items.Weapons.Melee.Rapiers;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering.Menus;

namespace PurpleStyrofoam.Managers
{
    public class InventoryManager : IPopUp
    {
        public Item[] Inventory;
        private Item SelectedItem;
        Rectangle location;
        public InventoryManager()
        {
            Open = false;
            if (Inventory == null)
            {
                Inventory = new Item[107];
                Inventory[(int)ITEMSLOTS.WEAPON] = new Lithiel(); // Weapon
                Inventory[(int)ITEMSLOTS.ARMOR_HELMET] = new BlankItem(); // Helmet
                Inventory[(int)ITEMSLOTS.ARMOR_CHESTPLATE] = new BlankItem(); // Chestplate
                Inventory[(int)ITEMSLOTS.ARMOR_LEGGINGS] = new BlankItem(); // Leggings
                Inventory[(int)ITEMSLOTS.ARMOR_BOOTS] = new BlankItem(); // Boots
                Inventory[(int)ITEMSLOTS.COMPANION] = new BlankItem(); // Companion
                Inventory[Inventory.Length - 1] = new InventoryDeleteItem(); // Trash
                for (int i = 6; i < Inventory.Length-1; i++)
                {
                    Inventory[i] = new BlankItem();
                }
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(TextureHelper.Blank(Color.Brown), location, Color.White);
            foreach (Item i in Inventory)
            {
                if (i != null) i.Sprite.Draw(sp);
            }
            if (SelectedItem != null) SelectedItem.Sprite.Draw(sp);
        }

        // Active updating is required due to player interaction with items.
        readonly int ItemSideLength = (int)Game.ScreenSize.X / 35;

        public bool Open { get; set; }

        public void Update()
        {
            int SpaceBuffer = (int)Game.ScreenSize.X / 400;
            location = new Rectangle(
                (int)RenderHandler.ScreenOffset.X + ((int)Game.ScreenSize.X / 4), // X Position
                (int)RenderHandler.ScreenOffset.Y + (((int)Game.ScreenSize.Y - ((int)Game.ScreenSize.X / 2)) / 2), // Y Position
                (int)Game.ScreenSize.X / 2, // Width
                (int)Game.ScreenSize.X / 2  // Height
                );

            int _BufferFromTop = ItemSideLength + SpaceBuffer;
            // Set-Item setup
            if (Inventory[(int)ITEMSLOTS.WEAPON] != null)
                Inventory[(int)ITEMSLOTS.WEAPON].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Inventory[(int)ITEMSLOTS.ARMOR_HELMET] != null)
                Inventory[(int)ITEMSLOTS.ARMOR_HELMET].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 2, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Inventory[(int)ITEMSLOTS.ARMOR_CHESTPLATE] != null)
                Inventory[(int)ITEMSLOTS.ARMOR_CHESTPLATE].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 3, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Inventory[(int)ITEMSLOTS.ARMOR_LEGGINGS] != null)
                Inventory[(int)ITEMSLOTS.ARMOR_LEGGINGS].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 4, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Inventory[(int)ITEMSLOTS.ARMOR_BOOTS] != null)
                Inventory[(int)ITEMSLOTS.ARMOR_BOOTS].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 5, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Inventory[(int)ITEMSLOTS.COMPANION] != null)
                Inventory[(int)ITEMSLOTS.COMPANION].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 6, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (SelectedItem != null) SelectedItem.Sprite.ItemRectangle.Location = MouseHandler.mousePos.ToPoint();

            // Actual Inventory Setup
            int xPos = location.X + SpaceBuffer;
            int yPos = location.Y + ((int)Game.ScreenSize.X / 4);
            for (int i = 6; i < Inventory.Length; i++)
            {
                if (xPos + ItemSideLength + SpaceBuffer < location.Right)
                {
                    if (Inventory[i] != null) Inventory[i].Sprite.ItemRectangle = new Rectangle(xPos, yPos, ItemSideLength, ItemSideLength);
                    xPos += ItemSideLength + SpaceBuffer;
                }
                else
                {
                    yPos += ItemSideLength + SpaceBuffer;
                    xPos = location.X + SpaceBuffer;
                }
            }
        }

        /// <summary>
        /// Attempts to add an item to the player's inventory
        /// </summary>
        /// <param name="i">Item to be added</param>
        /// <returns>True if successful, false if the inventory is full.</returns>
        public bool AddToInventory(Item i)
        {
            for (int index = 6; index < Inventory.Length; index++)
            {
                if (Inventory[index] is BlankItem)
                {
                    Inventory[index] = i;
                    return true;
                }
            }
            return false;
        }

        public void LoadItems()
        {
            foreach (Item i in Inventory)
            {
                if (i != null) i.Sprite.Load();
            }
        }
        public bool ShouldOpen()
        {
            if (MenuHandler.ActivePopUp != this) Open = false;
            if (KeyHelper.CheckTap(Keys.I) && !Open)
            {
                Open = true;
                return true;
            }
            return false;
        }

        public bool ShouldClose()
        {
            if (KeyHelper.CheckTap(Keys.I) && Open)
            {
                Open = false;
                return true;
            }
            return false;
        }

        public void ActionAtPosition(Vector2 pos)
        {
            Rectangle checkRect = new Rectangle(pos.ToPoint(), new Point(2, 2));
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (checkRect.Intersects(Inventory[i].Sprite.ItemRectangle))
                {
                    if (Inventory[i] is InventoryDeleteItem)
                    {
                        SelectedItem = null;
                        break;
                    }
                    if (Inventory[i] is BlankItem)
                    {
                        if (SelectedItem == null) continue;
                        else
                        {
                            Inventory[i] = SelectedItem;
                            SelectedItem = null;
                            break;
                        }
                    }
                    if (SelectedItem == null)
                    {
                        SelectedItem = Inventory[i];
                        Inventory[i] = new BlankItem();
                        break;
                    } else
                    {
                        Item temp1 = SelectedItem;
                        Item temp2 = Inventory[i];
                        SelectedItem = temp2;
                        Inventory[i] = temp1;
                        break;
                    }
                }
            }
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
