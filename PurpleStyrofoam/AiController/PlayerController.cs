using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.AiController
{
    class PlayerController : AnimatedSprite
    {
        public Texture2D PlayerSprite;
        private const string basePlayerSpriteName = "playerSprite";
        private const string movingPlayerSprite = "playerSpriteMoving";
        private const string jumpingDPlayerSprite = "playerSpriteJumpingDynamic";
        private const string jumpingSPlayerSprite = "playerSpriteJumpingStatic";
        public bool InAir { get; private set; }
        private ContentManager Content;
        public PlayerController(ContentManager content, int rows = 1, int columns = 1,  int xIn = 0, int yIn = 0) : base(content.Load<Texture2D>(basePlayerSpriteName), rows, columns, xIn, yIn)
        {
            PlayerSprite = content.Load<Texture2D>(basePlayerSpriteName);
            Content = content;
        }

        public override void Update()
        {
            CheckKeys();
            this.currentFrame++;
            if (this.currentFrame == this.totalFrames)
            {
                if (!InAir)
                {
                    this.currentFrame = 0;
                } else
                {
                    SwitchSprite(jumpingSPlayerSprite,1,1);
                    this.currentFrame = 0;
                }
            }
        }

        public void SwitchSprite(string nameOfFile, int rowsIn = 1, int columnsIn = 1)
        {
            this.Texture = Content.Load<Texture2D>(nameOfFile);
            this.Rows= rowsIn;
            this.Columns = columnsIn;
        }
        private KeyboardState oldState;
        private KeyboardState newState;
        public void CheckKeys()
        {
            newState = Keyboard.GetState();
            int moveX = 0;
            int moveY = 0;
           if (newState.IsKeyDown(Keys.A))
            {
                moveX -= 10;
                if (!InAir)
                {
                    SwitchSprite(movingPlayerSprite,1,1);
                }
            }
           if (newState.IsKeyDown(Keys.D))
            {
                moveX += 10;
                if (!InAir)
                {
                    SwitchSprite(movingPlayerSprite, 1, 1);
                }
            } 
           if (newState.IsKeyDown(Keys.Space)) {
                moveY -= 10;
                if (!InAir)
                {
                    InAir = true;
                    moveY -= 10;
                    SwitchSprite(jumpingDPlayerSprite,1,1);
                }
            }
            if (newState.IsKeyDown(Keys.S))
            {
                moveY += 10;
                InAir = false;
            }
           if (newState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
            {

            }
           if (newState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
            {

            }
            //Debug.WriteLine(moveX);
            if (moveX == 0) SwitchSprite(basePlayerSpriteName);
            this.X += moveX;
            this.Y += moveY;
            oldState = newState;
        }
    }
}
