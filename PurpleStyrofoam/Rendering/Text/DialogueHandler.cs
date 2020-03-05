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
            if (KeyHelper.CheckTap(Keys.Enter))
            {
                if (DialogueIndex < Dialogues.Length - 1)
                    if (!ActiveDialogue.IsCompleted()) ActiveDialogue.QuickComplete();
                    else ActiveDialogue = Dialogues[++DialogueIndex];
                else
                {
                    RenderHandler.CurrentGameState = GAMESTATE.ACTIVE;
                    ActiveDialogue = null;
                    Dialogues = new Dialogue[] { };
                    DialogueIndex = 0;
                }
            }
        }

        public static void Draw(SpriteBatch sp)
        {
            ActiveDialogue.Draw(sp);
        }

        public static void Start(Dialogue[] dias)
        {
            DialogueIndex = 0;
            Dialogues = dias;
            ActiveDialogue = dias[DialogueIndex];
            RenderHandler.CurrentGameState = GAMESTATE.PAUSED;
        }
    }
}
