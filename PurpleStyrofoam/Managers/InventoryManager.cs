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
using PurpleStyrofoam.Items.Weapons;
using PurpleStyrofoam.Items.Potions;
using PurpleStyrofoam.Items.Armors;

namespace PurpleStyrofoam.Managers
{
    public class InventoryManager : IPopUp
    {
        public Item[] Items;
        private Item SelectedItem;
        private static Item HoverItem;
        Rectangle location;

        private string[] ItemInformation;

        public InventoryManager()
        {
            Open = false;
            if (Items == null)
            {
                Items = new Item[107];
                Items[(int)ITEMSLOTS.WEAPON] = new BlankItem(); // Weapon
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

        const float ItemRarityTransparency = 0.5f;
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(TextureHelper.Blank(Color.Black), location, Color.White);
            foreach (Item i in Items)
            {
                if (i is BlankItem) sp.Draw(TextureHelper.Blank(Color.Black), i.Sprite.ItemRectangle, Color.White);
                else sp.Draw(TextureHelper.Blank(i.Rarity), i.Sprite.ItemRectangle, Color.White * ItemRarityTransparency);

                if (i != null) i.Sprite.Draw(sp);
            }
            if (SelectedItem != null) SelectedItem.Sprite.Draw(sp);
            DrawItemInfo(sp);
        }

        // Active updating is required due to player interaction with items.
        readonly int ItemSideLength = (int)Game.ScreenSize.X / 35;

        public bool Open { get; set; }

        public void Update()
        {
            SetupItemDisplay();
            CheckHovering();
            foreach (Item i in Items)
            {
                i.Sprite.animate.Angle = 0.0f;
                i.Sprite.animate.Origin = new Vector2(0, 0);
                i.Sprite.Visible = true;
                i.Update();
            }
        }

        public void SetupItemDisplay()
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
            if (i is Armor || i is Weapon)
            {
                for (int index = 6; index < Items.Length; index++)
                {
                    if (Items[index] is BlankItem)
                    {
                        Items[index] = i;
                        return true;
                    }
                }

            } else
            {
                // Find the first Stack of item
                for (int index = 6; index < Items.Length; index++)
                {
                    if (Items[index] is ItemStack)
                    {
                        Debug.WriteLine("pp");
                        if (((ItemStack)Items[index]).AddToStack(i)) return true;
                    }
                }
                // If none found, find a dupe and create a stack
                for (int index = 6; index < Items.Length; index++)
                {
                    if (Items[index].Equals(i))
                    {
                        List<Item> temp = new List<Item>();
                        temp.Add(i);
                        temp.Add(Items[index]);
                        Items[index] = new ItemStack(temp);
                        return true;
                    }
                }
                // If there isn't any, add it regularly to inventory.
                for (int index = 6; index < Items.Length; index++)
                {
                    if (Items[index] is BlankItem)
                    {
                        Items[index] = i;
                        return true;
                    }
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
                LoadItems();
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
                foreach (Item i in Items)
                {
                    i.Sprite.Visible = false;
                    if (i is Weapon)
                    {
                        i.Sprite.ItemRectangle.Width = (int)((Weapon)i).Size.X;
                        i.Sprite.ItemRectangle.Height = (int)((Weapon)i).Size.Y;
                    }
                }
                return true;
            }
            return false;
        }

        public void CheckHovering()
        {
            Rectangle mouse = new Rectangle(MouseHandler.mousePos.ToPoint(), new Point(2, 2));
            foreach (Item i in Items)
            {
                if (i is BlankItem) continue;
                if (i.Sprite.ItemRectangle.Intersects(mouse))
                {
                    HoverItem = i;
                    break;
                }
                else HoverItem = null;
            }
        }

        const float TransparencyLevel = 0.5f;
        public void DrawItemInfo(SpriteBatch sp)
        {
            if (HoverItem != null)
            {
                if (HoverItem is ItemStack)
                {
                    List<Item> stack = ((ItemStack)HoverItem).items;
                    ItemInformation = new string[]
                    {
                        HoverItem.ID + " - " + HoverItem.Name + " (x" + stack.Count + ")",
                        stack[0] is Weapon ? "Damage: " + ((Weapon) stack[0]).Damage.ToString() : "",
                        stack[0] is Potion ? "Effect: " + ((Potion) stack[0]).EffectDescription : "",
                        stack[0] is Potion ? "Duration: " + GameMathHelper.FramesToStringTime(((Potion) stack[0]).Duration) : "",
                        "Description: " + HoverItem.Description
                    };
                } else
                {
                    ItemInformation = new string[]
                    {
                        HoverItem.ID + " - " + HoverItem.Name,
                        HoverItem is Weapon ? "Damage: " + ((Weapon) HoverItem).Damage.ToString() : "",
                        HoverItem is Potion ? "Effect: " + ((Potion) HoverItem).EffectDescription : "",
                        HoverItem is Potion ? "Duration: " + GameMathHelper.FramesToStringTime(((Potion) HoverItem).Duration) : "",
                        "Description: " + HoverItem.Description
                    };
                }

                Point position = MouseHandler.mousePos.ToPoint();
                SpriteFont font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);

                sp.Draw(TextureHelper.Blank(Color.Black), position.ToVector2(), new Rectangle(position,
                        (font.MeasureString(ItemInformation[0]) * new Vector2(0.7f, 1f) ).ToPoint()), Color.White * TransparencyLevel);
                sp.DrawString(font, ItemInformation[0] , position.ToVector2(), Color.LightGray, 0f, new Vector2(), 0.7f, SpriteEffects.None, 1f);
                float yPos = position.Y + font.MeasureString(ItemInformation[0]).Y;
                for (int i = 1; i < ItemInformation.Length; i++)
                {
                    if (ItemInformation[i].Length == 0) continue;
                    sp.Draw(TextureHelper.Blank(Color.Black), new Vector2(position.X, yPos), new Rectangle(new Point(position.X, (int)yPos), 
                        (font.MeasureString(ItemInformation[i]) * new Vector2(0.7f, 1f)).ToPoint()), Color.White * TransparencyLevel);
                    sp.DrawString(font, ItemInformation[i], new Vector2(position.X, yPos), 
                        Color.LightGray, 0f, new Vector2(), 0.7f, SpriteEffects.None, 1f);
                    yPos += font.MeasureString(ItemInformation[i]).Y;
                }
            }
        }

        public void ActionAtPosition(Vector2 pos)
        {
            Rectangle checkRect = new Rectangle(pos.ToPoint(), new Point(2, 2));
            for (int i = 0; i < Items.Length; i++)
            {
                if (checkRect.Intersects(Items[i].Sprite.ItemRectangle))
                {
                    // Delete an item
                    if (Items[i] is InventoryDeleteItem)
                    {
                        SelectedItem = null;
                        break;
                    }
                    if (Items[i] is BlankItem)
                    {
                        // Selected item and target are blank so do nothing
                        if (SelectedItem == null) continue;

                        // Target is blank, but is holding item so put item in that blank slot
                        else
                        {
                            if (i == (int)ITEMSLOTS.WEAPON) Game.PlayerManager.EquippedWeapon = (Weapon) SelectedItem;
                            else Items[i] = SelectedItem;
                            SelectedItem = null;
                            break;
                        }
                    }

                    // There is nothing selected, but target has something so pick item up
                    if (SelectedItem == null)
                    {
                        SelectedItem = Items[i];
                        SelectedItem.Sprite.Load();
                        if (i == (int)ITEMSLOTS.WEAPON) Game.PlayerManager.EquippedWeapon = null;
                        else Items[i] = new BlankItem();
                        break;
                    }
                    //} 

                    // Something is selected and target has something so switch the items
                    else
                    {
                        Item temp1 = SelectedItem;
                        Item temp2 = Items[i];
                        SelectedItem = temp2;
                        SelectedItem.Sprite.Load();
                        if (i == (int)ITEMSLOTS.WEAPON) Game.PlayerManager.EquippedWeapon = (Weapon) temp1;
                        else Items[i] = temp1;
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
            Rectangle checkRect = new Rectangle(pos.ToPoint(), new Point(2, 2));
            foreach (Item i in Items)
            {
                if (i.Sprite.ItemRectangle.Intersects(checkRect)) i.OnInventoryUse();
            }
        }

        public void DeleteItem(Item item)
        {
            for (int i = 6; i < Items.Length; i++)
            {
                if (Items[i] is ItemStack)
                {
                    if (((ItemStack)Items[i]).items[0].Equals(item)) ((ItemStack)Items[i]).items.Remove(item);
                }
                else
                {
                    if (Items[i] == item)
                    {
                        Items[i] = new BlankItem();
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
