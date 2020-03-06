using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;

namespace PurpleStyrofoam.Rendering.Text
{
    public class Dialogue
    {
        string DialogueCharacterName;
        string DialogueText;
        string CurrentDialogueText;
        Texture2D DisplayImage;
        SpriteFont font;
        Rectangle TextureImagePosition;
        Rectangle TextBoxPosition;
        Rectangle TextNamePosition;
        DIALOGUESPEED Speed;

        // TODO: Add sound

        int CurrentCharCount;

        public Dialogue(string image, string name, string text, DIALOGUELOCATION imgPos, DIALOGUESPEED spe = DIALOGUESPEED.VERYFAST)
        {
            font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);

            // Taking input
            DisplayImage = Game.GameContent.Load<Texture2D>(image);
            DialogueText = StringBreaker(text);
            if (spe == DIALOGUESPEED.INSTANT) CurrentDialogueText = DialogueText;
            DialogueCharacterName = name;
            TextBoxPosition = new Rectangle((int)RenderHandler.ScreenOffset.X, (int)((0.7 * Game.ScreenSize.Y) + RenderHandler.ScreenOffset.Y),
                                            (int)Game.ScreenSize.X, (int)(0.3 * Game.ScreenSize.Y));
            Point temp = font.MeasureString(DialogueCharacterName).ToPoint() + new Point(20,10);
            switch (imgPos)
            {
                case DIALOGUELOCATION.LEFT:
                    TextureImagePosition = new Rectangle((int)RenderHandler.ScreenOffset.X, (int)RenderHandler.ScreenOffset.Y, 
                        (int)(Game.ScreenSize.X / 3), (int)Game.ScreenSize.Y);
                    TextNamePosition = new Rectangle(new Point((int)RenderHandler.ScreenOffset.X + (int)(Game.ScreenSize.X - temp.X),
                        (int)TextBoxPosition.Y - temp.Y), temp);
                    break;
                case DIALOGUELOCATION.MIDDLE:
                    TextureImagePosition = new Rectangle((int)RenderHandler.ScreenOffset.X + (int)(Game.ScreenSize.X / 3), (int)RenderHandler.ScreenOffset.Y, 
                        (int)(Game.ScreenSize.X / 3), (int)Game.ScreenSize.Y);
                    TextNamePosition = new Rectangle(new Point((int)RenderHandler.ScreenOffset.X, (int)TextBoxPosition.Y - temp.Y), temp);
                    break;
                case DIALOGUELOCATION.RIGHT:
                    TextureImagePosition = new Rectangle((int)RenderHandler.ScreenOffset.X + (int)(Game.ScreenSize.X - (Game.ScreenSize.X / 3)), (int)RenderHandler.ScreenOffset.Y, 
                        (int)Game.ScreenSize.X, (int)Game.ScreenSize.Y);
                    TextNamePosition = new Rectangle(new Point((int)RenderHandler.ScreenOffset.X, (int)TextBoxPosition.Y - temp.Y), temp);
                    break;
                default:
                    break;
            }

            // Setting up instance variables
            CurrentCharCount = 0;
            Speed = spe;
            Update();
        }

        public void Draw(SpriteBatch sp)
        {
            // Image Position
            sp.Draw(DisplayImage, TextureImagePosition, Color.White);

            // Acutal Text Box position
            sp.Draw(TextureHelper.Blank(Color.Black), TextBoxPosition, Color.White * 0.75f);
            sp.DrawString(font, CurrentDialogueText, TextBoxPosition.Location.ToVector2() + new Vector2(10,10), Color.White);

            // Name Tag position
            sp.Draw(TextureHelper.Blank(Color.Black), TextNamePosition, Color.White * 0.75f);
            sp.DrawString(font, DialogueCharacterName, TextNamePosition.Location.ToVector2() + new Vector2(10, 5), Color.White);
        }

        public void Update()
        {
            if (CurrentCharCount < DialogueText.Length * (int)Speed) CurrentCharCount++;
            if (CurrentDialogueText != DialogueText) CurrentDialogueText = DialogueText.Substring(0, (int) Math.Floor(CurrentCharCount/(double)(int)Speed));
        }

        string StringBreaker(string input)
        {
            string newString = "";
            int lines = (int)Math.Ceiling((font.MeasureString(input).X + 10) / Game.ScreenSize.X);
            int insertionPointIndex = input.Length / lines;
            for (int i = 0; i < lines; i++)
            {
                int start = insertionPointIndex * i;
                int length = insertionPointIndex;
                if (start + length <= input.Length)
                    newString += input.Substring(start, length) + "\n";
            }
            return newString;
        }

        public void QuickComplete()
        {
            CurrentDialogueText = DialogueText;
        }

        public Boolean IsCompleted()
        {
            return CurrentDialogueText == DialogueText;
        }
    }

    public enum DIALOGUELOCATION
    {
        LEFT, MIDDLE, RIGHT
    }

    public enum DIALOGUESPEED
    {
        VERYFAST = 1, FAST = 2, MEDIUM = 3, SLOW = 4, VERYSLOW = 5, INSTANT = 0
    }
}
