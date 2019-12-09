using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.AiController;
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
        public static void Initialize()
        {
            player = (PlayerController) RenderHandler.allCharacterSprites.Find(x => x.GetType().Name.Equals("PlayerController"));
            Manager = (PlayerManager) player.Manager;
            Location = new Vector2(0, 0);
        }

        static Texture2D HealthTexture = Game.GameContent.Load<Texture2D>("playerSpriteMoving");
        static Texture2D BarBackTexture = Game.GameContent.Load<Texture2D>("BarBack");
        static Texture2D ManaTexture = Game.GameContent.Load<Texture2D>("playerSprite");
        static double HLength;
        static double MLength;
        const int BarLength = 400;
        public static void Draw(SpriteBatch sp)
        {
            sp.Draw(BarBackTexture, new Rectangle((int)Location.X, (int)Location.Y, BarLength-10, 25), Color.White);
            sp.Draw(HealthTexture, new Rectangle((int)Location.X, (int)Location.Y, (int)HLength,25), Color.White);

            sp.Draw(BarBackTexture, new Rectangle((int)Location.X, (int)Location.Y + 50, BarLength - 10, 25), Color.White);
            sp.Draw(ManaTexture, new Rectangle((int)Location.X, (int)Location.Y + 50, (int)MLength, 25), Color.White);
        }
        public static void Update()
        {
            HLength = ((double)Manager.Health / (double)Manager.MaxHealth) * BarLength;
            MLength = ((double)Manager.Mana / (double)Manager.MaxMana) * BarLength;
            BaseMap map = RenderHandler.selectedMap;
            Location = new Vector2(
                RenderHandler.ScreenOffset.X < map.maxBounds.Left ? map.maxBounds.Left + 100: RenderHandler.ScreenOffset.X + 100,
                RenderHandler.ScreenOffset.Y < map.maxBounds.Top ? map.maxBounds.Top + 50: RenderHandler.ScreenOffset.Y + 50
                );
        }
    }
}
