using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Managers.Classes
{
    public class Manipulator : GameClass
    {
        PlayerController SpriteSource;
        public bool ActivePlatform = false;
        MapObject Platform;
        public Manipulator()
        {
            SpriteSource = Game.PlayerCharacter;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SpriteSource = (PlayerController) spIN;
        }

        public override void EAction()
        {
            if (!ActivePlatform)
            {
                Platform = new MapObject(PlayerManager.movingPlayerSprite,
                    new Vector2(MouseHandler.mousePos.X, MouseHandler.mousePos.Y), 200,20);
                Platform.Load();
                ObjectMapper.AddMapObject(Platform);
                RenderHandler.extras.Add(Platform);
                ActivePlatform = true;
            } else
            {
                ObjectMapper.DeleteMapObject(Platform);
                RenderHandler.extras.Remove(Platform);
                Platform = null;
                ActivePlatform = false;
            }
        }

        public override void RClick()
        {
            throw new NotImplementedException();
        }
    }
}
