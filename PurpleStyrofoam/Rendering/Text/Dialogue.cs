using System;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering.Text
{
    public class Dialogue
    {
        string DialogueCharacterName;
        char[] DialogueText;
        Texture2D DisplayImage;
        // TODO: Add sound

        int CurrentCharCount;

        public Dialogue(string image, string name, string text)
        {
            DisplayImage = Game.GameContent.Load<Texture2D>(image);
            DialogueText = text.ToCharArray();
            DialogueCharacterName = name;
            CurrentCharCount = 0;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.DrawString
        }

        public void Update()
        {
            if (CurrentCharCount != DialogueText.Length - 1) CurrentCharCount++;
        }
    }

    enum LOCATION
    {
        LEFT,
        MIDDLE,
        RIGHT
    }
}
