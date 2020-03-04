using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Helpers;
using System;
namespace PurpleStyrofoam.Rendering.Text
{
    public static class DialogueHandler
    {
        public static Dialogue ActiveDialogue { get; private set; }
        public static Dialogue[] Dialogues {get; private set;}
        public static int DialogueIndex { get; private set; }

        public static void Update()
        {
            ActiveDialogue.Update();
            if (KeyHelper.CheckTap(Keys.Enter)) ActiveDialogue = Dialogues[++DialogueIndex];
        }

        public static void Draw(SpriteBatch sp)
        {
            ActiveDialogue.Draw(sp);
        }

        public static void Start(Dialogue[] dias)
        {
            DialogueIndex = 0;
            ActiveDialogue = dias[DialogueIndex];
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
        }
    }
}
