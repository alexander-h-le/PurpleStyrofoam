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
        public Manipulator(AnimatedSprite inp)
        {
            SpriteSource =(PlayerController) inp;
        }
        public override void AddSpriteSource(AnimatedSprite spIN)
        {
            SpriteSource =(PlayerController) spIN;
        }

        public override void EAction()
        {
            if (!ActivePlatform)
            {
                Debug.WriteLine("cheeks");
                Platform = new MapObject("Manipulator-Platform",Game.GameContent, "playerSpriteMoving",
                    new Vector2(MouseHandler.mousePos.X, MouseHandler.mousePos.Y), 200,20);
                ObjectMapper.AddMapObject(Platform);
                ActivePlatform = true;
            } else
            {
                ObjectMapper.DeleteMapObject(Platform);
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
