using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Helpers
{
    public static class TextureHelper
    {
        public static class Sprites
        {
            public const string EnemySprite = "enemyTest";
            public const string TestImage = "testIMG";
            public const string Dog = "SmileyWalk";
            public const string DialogueTestSprite = "DialogueTestSprite";
        }

        public static class Fonts
        {
            public const string Default = "DefaultGameText";
        }

        public static Texture2D Blank(Color cl)
        {
            Texture2D tx = new Texture2D(Game.graphics.GraphicsDevice, 1, 1);
            tx.SetData(new[] { cl });
            return tx;
        }
    }
}
