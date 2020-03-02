using System;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering.Text
{
    public class Dialogue
    {
        string DialogueText;
        Texture2D DisplayImage;
        // TODO: Add sound

        public Dialogue(string image, string text)
        {
            DisplayImage = Game.GameContent.Load<Texture2D>(image);
            DialogueText = text;
        }
    }

    enum LOCATION
    {
        LEFT,
        MIDDLE,
        RIGHT
    }
}
