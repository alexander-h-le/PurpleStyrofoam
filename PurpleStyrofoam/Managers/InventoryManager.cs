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
        public Item[] Items;
        private Item SelectedItem;
        Rectangle location;
        public InventoryManager()
        {
            Open = false;
            if (Items == null)
            {
                Items = new Item[107];
                Items[(int)ITEMSLOTS.WEAPON] = new Lithiel(); // Weapon
                Items[(int)ITEMSLOTS.ARMOR_HELMET] = new BlankItem(); // Helmet
                Items[(int)ITEMSLOTS.ARMOR_CHESTPLATE] = new BlankItem(); // Chestplate
                Items[(int)ITEMSLOTS.ARMOR_LEGGINGS] = new BlankItem(); // Leggings
                Items[(int)ITEMSLOTS.ARMOR_BOOTS] = new BlankItem(); // Boots
                Items[(int)ITEMSLOTS.COMPANION] = new BlankItem(); // Companion
                Items[Items.Length - 1] = new InventoryDeleteItem(); // Trash
                for (int i = 6; i < Items.Length-1; i++)
                {
                    Items[i] = new BlankItem();
                }
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(TextureHelper.Blank(Color.Brown), location, Color.White);
            foreach (Item i in Items)
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
            if (Items[(int)ITEMSLOTS.WEAPON] != null)
                Items[(int)ITEMSLOTS.WEAPON].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Items[(int)ITEMSLOTS.ARMOR_HELMET] != null)
                Items[(int)ITEMSLOTS.ARMOR_HELMET].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 2, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Items[(int)ITEMSLOTS.ARMOR_CHESTPLATE] != null)
                Items[(int)ITEMSLOTS.ARMOR_CHESTPLATE].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 3, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Items[(int)ITEMSLOTS.ARMOR_LEGGINGS] != null)
                Items[(int)ITEMSLOTS.ARMOR_LEGGINGS].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 4, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Items[(int)ITEMSLOTS.ARMOR_BOOTS] != null)
                Items[(int)ITEMSLOTS.ARMOR_BOOTS].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 5, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (Items[(int)ITEMSLOTS.COMPANION] != null)
                Items[(int)ITEMSLOTS.COMPANION].Sprite.ItemRectangle = new Rectangle(
                    location.Left + (location.Width / 5), // X
                    location.Top + _BufferFromTop * 6, // Y
                    ItemSideLength, // Width
                    ItemSideLength); // Height
            if (SelectedItem != null) SelectedItem.Sprite.ItemRectangle.Location = MouseHandler.mousePos.ToPoint();

            // Actual Items Setup
            int xPos = location.X + SpaceBuffer;
            int yPos = location.Y + ((int)Game.ScreenSize.X / 4);
            for (int i = 6; i < Items.Length; i++)
            {
                if (xPos + ItemSideLength + SpaceBuffer < location.Right)
                {
                    if (Items[i] != null) Items[i].Sprite.ItemRectangle = new Rectangle(xPos, yPos, ItemSideLength, ItemSideLength);
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
        /// Attempts to add an item to the player's Items
        /// </summary>
        /// <param name="i">Item to be added</param>
        /// <returns>True if successful, false if the Items is full.</returns>
        public bool AddToInventory(Item i)
        {
            for (int index = 6; index < Items.Length; index++)
            {
                if (Items[index] is BlankItem)
                {
                    Items[index] = i;
                    return true;
                }
            }
            return false;
        }

        public void LoadItems()
        {
            foreach (Item i in Items)
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
            for (int i = 0; i < Items.Length; i++)
            {
                if (checkRect.Intersects(Items[i].Sprite.ItemRectangle))
                {
                    if (Items[i] is InventoryDeleteItem)
                    {
                        SelectedItem = null;
                        break;
                    }
                    if (Items[i] is BlankItem)
                    {
                        if (SelectedItem == null) continue;
                        else
                        {
                            Items[i] = SelectedItem;
                            SelectedItem = null;
                            break;
                        }
                    }
                    if (SelectedItem == null)
                    {
                        SelectedItem = Items[i];
                        Items[i] = new BlankItem();
                        break;
                    } else
                    {
                        Item temp1 = SelectedItem;
                        Item temp2 = Items[i];
                        SelectedItem = temp2;
                        Items[i] = temp1;
                        break;
                    }
                }
            }
        }

        public bool SwitchItems(Item source, Item target)
        {
            int indexSource = -1;
            int indexTo = -1;

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == source) indexSource = i;
                if (Items[i] == target) indexTo = i;
                if (indexSource > -1 && indexTo > -1) break;
            }

            if (indexSource == -1 || indexTo == -1) return false;

            Items[indexSource] = target;
            Items[indexTo] = source;

            return true;
        }

        public void InventoryUseAtPosition(Vector2 pos)
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
