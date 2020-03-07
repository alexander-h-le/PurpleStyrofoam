using PurpleStyrofoam.Rendering.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Helpers
{
    public class TestHelper
    {
        public static Dialogue[] GetTestDialogues()
        {
            return new Dialogue[] {
                new Dialogue(TextureHelper.Sprites.Dog, 
                "Captain Vor", "Look at them, they come to this place when they know they are not pure. Tenno use the keys, but they are mere tresspassers. " +
                "Only I, Vor, know the true power of the void. I was cut in half, destroyed, but through its Janus Key, the Void called to me. " +
                "It brought me here and here I was reborn. We cannot blame these creatures, they are being led by a false prophet, an imposter who knows not the secrets of the void. " +
                "Behold the Tenno, come to scavenge and desecrate this sacred realm. My brothers, did I not tell of this day? Did I not prophesize this moment? " +
                "Now I will stop them. Now I am changed, reborn through the energy of the Janus Key. Forever bound to the void. " +
                "Let it be known, if the Tenno want true salvation, they will lay down their arms, and wait for the baptism of my Janus key. " +
                "It is time. I will teach these trespassers the redemptive power of my Janus key. They will learn it's simple truth. " +
                "The Tenno are lost, and they will resist. But I, Vor, will cleanse this palce of their impurity.",
                DIALOGUELOCATION.RIGHT, DIALOGUESPEED.MEDIUM, true),

                new Dialogue(PlayerManager.jumpingDPlayerSprite,
                "Tenno (Rhino Prime)", "Be quiet, Vor. I'm sick of this speech. Give me my argon crystals already.",
                DIALOGUELOCATION.LEFT, DIALOGUESPEED.SLOW),

                new Dialogue(PlayerManager.basePlayerSpriteName,
                "Captain Vor", "Silence Tenno, for you are lo--",
                DIALOGUELOCATION.RIGHT, DIALOGUESPEED.VERYSLOW),

                new Dialogue(PlayerManager.basePlayerSpriteName,
                "Captain Vor", "*dies*",
                DIALOGUELOCATION.RIGHT, DIALOGUESPEED.FAST),

                new Dialogue(TextureHelper.Sprites.DialogueTestSprite,
                "Tenno (Gauss)", "Finally, some silence...",
                DIALOGUELOCATION.LEFT, DIALOGUESPEED.VERYFAST),

                new Dialogue(PlayerManager.movingPlayerSprite,
                "Tenno (Titania)", "Only one argon?! Come ON!",
                DIALOGUELOCATION.MIDDLE, DIALOGUESPEED.INSTANT)
            };
        }
    }
}
