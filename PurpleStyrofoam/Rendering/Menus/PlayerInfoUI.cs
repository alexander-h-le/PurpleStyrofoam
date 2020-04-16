using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Buffs;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Menus
{
    public static class PlayerInfoUI
    {
        static PlayerController player;
        static PlayerManager Manager;
        static Vector2 Location;
        static Rectangle AbilityBarLocation;

        /// <summary>
        /// Initializes the Player UI. Instantiates variables that only need to be instantiated once. 
        /// </summary>
        public static void Initialize()
        {
            player = Game.PlayerCharacter;
            Manager = (PlayerManager) player.Manager;
            Location = new Vector2(0, 0);
            int ABHeight = (int)Game.ScreenSize.Y / 12;
            int ABWidth = ABHeight * 4;
            AbilityBarLocation = new Rectangle(
                (int)RenderHandler.ScreenOffset.X + ((int)Game.ScreenSize.X / 2) - (ABWidth / 2), //X
                (int)RenderHandler.ScreenOffset.Y + (int)Game.ScreenSize.Y - ABHeight, //Y
                ABWidth, //Width
                ABHeight //Height
                );
        }

        static Texture2D HealthTexture = Game.GameContent.Load<Texture2D>("playerSpriteMoving");
        static Texture2D BarBackTexture = Game.GameContent.Load<Texture2D>("BarBack");
        static Texture2D ManaTexture = Game.GameContent.Load<Texture2D>("playerSprite");
        static double HLength;
        static double MLength;
        const int BarLength = 400;
        static SpriteFont font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);
        public static void Draw(SpriteBatch sp)
        {
            //Health Bar
            sp.Draw(BarBackTexture, new Rectangle((int)Location.X, (int)Location.Y, BarLength-10, 25), Color.White);
            sp.Draw(HealthTexture, new Rectangle((int)Location.X, (int)Location.Y, (int)HLength,25), Color.White);

            //Mana Bar
            sp.Draw(BarBackTexture, new Rectangle((int)Location.X, (int)Location.Y + 50, BarLength - 10, 25), Color.White);
            sp.Draw(ManaTexture, new Rectangle((int)Location.X, (int)Location.Y + 50, (int)MLength, 25), Color.White);

            Rectangle BuffPosition = new Rectangle((int)Location.X, (int)Location.Y + 100, 50, 50);
            //Buffs
            foreach (Buff b in  Game.PlayerCharacter.Buffs.CurrentBuffs)
            {
                if (b.Texture != null) sp.Draw(b.Texture, BuffPosition , Color.White);
                else sp.Draw(TextureHelper.Blank(Color.Bisque), BuffPosition, Color.White);


                sp.DrawString(font, GameMathHelper.FramesToStringTime(b.Duration) , new Point(BuffPosition.X, BuffPosition.Bottom).ToVector2(), Color.White, 
                    0f, new Vector2(), 0.7f, SpriteEffects.None, 1);

                BuffPosition.X += 10 + BuffPosition.Width;
            }

            //Ability Bar
            sp.Draw(TextureHelper.Blank(Color.DarkOliveGreen), AbilityBarLocation, Color.White);
            sp.Draw(TextureHelper.Blank(Color.Turquoise),
                new Rectangle(AbilityBarLocation.Right - (AbilityBarLocation.Width / 4), AbilityBarLocation.Y, AbilityBarLocation.Width / 4 , AbilityBarLocation.Height),
                Color.White); // Q Ability
            sp.Draw(TextureHelper.Blank(Color.Pink),
                new Rectangle(AbilityBarLocation.Left, AbilityBarLocation.Y, AbilityBarLocation.Width / 4,
                    (int)(AbilityBarLocation.Height * Manager.Class.CooldownPercentage())),
                Color.White); // E Ability
        }
        public static void Update()
        {
            HLength = ((double)Manager.Health / Manager.MaxHealth) * BarLength;
            MLength = ((double)Manager.Mana / Manager.MaxMana) * BarLength;
            BaseMap map = RenderHandler.selectedMap;
            Manager.Class.Update();
            Location = new Vector2(
                RenderHandler.ScreenOffset.X < map.maxBounds.Left ? map.maxBounds.Left + 100: RenderHandler.ScreenOffset.X + 100,
                RenderHandler.ScreenOffset.Y < map.maxBounds.Top ? map.maxBounds.Top + 50: RenderHandler.ScreenOffset.Y + 50
                );

            int ABHeight = (int)Game.ScreenSize.Y / 12;
            int ABWidth = ABHeight * 4;
            AbilityBarLocation = new Rectangle(
                (int)RenderHandler.ScreenOffset.X + ((int)Game.ScreenSize.X / 2) - (ABWidth / 2), //X
                (int)RenderHandler.ScreenOffset.Y + (int)Game.ScreenSize.Y - ABHeight, //Y
                ABWidth, //Width
                ABHeight //Height
                );
        }
    }
}
