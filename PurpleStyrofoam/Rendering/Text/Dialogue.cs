using System;
using System.Collections.Generic;
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
        Vector2 Position;
        Rectangle TextureImagePosition;

        readonly Vector2 TextBoxPosition = new Vector2(0f,(float) (0.7*Game.ScreenSize.Y));
        Rectangle TextNamePosition;

        // TODO: Add sound

        int CurrentCharCount;

        public Dialogue(string image, string name, string text, Rectangle imgPos)
        {
            // Taking input
            DisplayImage = Game.GameContent.Load<Texture2D>(image);
            DialogueText = text;
            DialogueCharacterName = name;
            TextureImagePosition = imgPos;

            // Setting up instance variables
            CurrentCharCount = 0;
            font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);
            TextNamePosition = new Rectangle(new Point(0, (int) TextBoxPosition.Y - 10), font.MeasureString(DialogueCharacterName).ToPoint());
        }

        public void Draw(SpriteBatch sp)
        {
            // Image Position
            sp.Draw(DisplayImage, TextureImagePosition, Color.White);

            // Acutal Text Box position
            sp.Draw(TextureHelper.Blank(Color.Black), TextBoxPosition, Color.Black);
            string[] words = GetFoldedString();
            for (int i = 0; i < words.Length; i++)
            {
                sp.DrawString(font, words[i], new Vector2(TextBoxPosition.X, TextBoxPosition.Y + (10 * i)), Color.White);
            }

            // Name Tag position
            sp.Draw(TextureHelper.Blank(Color.Black), TextNamePosition, Color.Black);
        }

        public void Update()
        {
            if (CurrentCharCount != DialogueText.Length - 1) CurrentCharCount++;
            CurrentDialogueText = DialogueText.Substring(0, CurrentCharCount);
        }

        public string[] GetFoldedString()
        {
            string[] arr = CurrentDialogueText.Split(' ');
            List<String> strings = new List<string>();

            int tempTotalLength = 0;
            int index = 0;
            string temp = "";
            while (index < arr.Length)
            {
                while (tempTotalLength < Game.ScreenSize.X)
                {
                    tempTotalLength += (int)font.MeasureString(arr[index]).X;
                    temp += arr[index] + " ";
                }
                index++;
                strings.Add(temp);
                temp = "";
            }
            return strings.ToArray();
        }
    }

    public static class LOCATION
    {
        public static Rectangle LEFT = new Rectangle(0, 0, (int)(Game.ScreenSize.X/3), (int)Game.ScreenSize.Y);
        public static Rectangle MIDDLE = new Rectangle((int)(Game.ScreenSize.X / 3), 0, (int)(Game.ScreenSize.X / 3), (int)Game.ScreenSize.Y);
        public static Rectangle RIGHT = new Rectangle((int) (Game.ScreenSize.X - (Game.ScreenSize.X / 3)), 0, (int) Game.ScreenSize.X, (int) Game.ScreenSize.Y);
    }
}
