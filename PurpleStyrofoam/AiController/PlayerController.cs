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
using PurpleStyrofoam.Items.Weapons;

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
        public Weapon HeldWeapon { get; set; }
        public PlayerController(ContentManager content, int rows = 1, int columns = 1,  int xIn = 0, int yIn = 0, Weapon weapIn = null) : base(content.Load<Texture2D>(basePlayerSpriteName), rows, columns, xIn, yIn)
        {
            PlayerSprite = content.Load<Texture2D>(basePlayerSpriteName);
            Content = content;
            HeldWeapon = weapIn;
        }

        public override void Update()
        {
            CheckKeys();
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                if (!InAir)
                {
                    currentFrame = 0;
                } else if (currentSprite == jumpingDPlayerSprite)
                {
                    SwitchSprite(jumpingSPlayerSprite);
                    currentFrame = 0;
                } else
                {
                    currentFrame = 0;
                }
            }
        }

        private string currentSprite;
        public void SwitchSprite(string nameOfFile, int rowsIn = 1, int columnsIn = 1)
        {
            //Debug.WriteLine(nameOfFile + " " + !nameOfFile.Equals(currentSprite) + " " + currentSprite);
             if (!nameOfFile.Equals(currentSprite))
            {
                Rows = rowsIn;
                Columns = columnsIn;
                totalFrames = Rows * Columns;
                Texture = Content.Load<Texture2D>(nameOfFile);
                currentSprite = nameOfFile;
            }
        }
        private KeyboardState oldState;
        private KeyboardState newState;
        private const int moveSpeed = 5;
        public void CheckKeys()
        {
            newState = Keyboard.GetState();
            int moveX = 0;
            int moveY = 0;
           if (!RenderHandler.IsLoading)
            {
                if (newState.IsKeyDown(Keys.A))
                {
                    moveX -= moveSpeed;
                    if (!InAir && oldState.IsKeyUp(Keys.A))
                    {
                        SwitchSprite(movingPlayerSprite, 4, 4);
                    }
                }
                if (newState.IsKeyDown(Keys.D))
                {
                    moveX += moveSpeed;
                    if (!InAir && oldState.IsKeyUp(Keys.D))
                    {
                        SwitchSprite(movingPlayerSprite, 4, 4);
                    }
                }
                if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    moveY -= moveSpeed*2;
                    if (!InAir)
                    {
                        InAir = true;
                        moveY -= 10;
                        SwitchSprite(jumpingDPlayerSprite, 1, 1);
                    }
                }
                if (newState.IsKeyDown(Keys.S))
                {
                    moveY += moveSpeed;
                    InAir = false;
                }
                if (newState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
                {
                }
                if (newState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
                {
                }
            }
            if (moveX == 0 && !InAir) SwitchSprite(basePlayerSpriteName,10,10);
            this.X += moveX;
            this.Y += moveY;
            oldState = newState;
        }
    }
}
