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
using System.Timers;
using PurpleStyrofoam.AiController.AIs;

namespace PurpleStyrofoam.AiController
{
    public class PlayerController : AnimatedSprite
    {
        private const string basePlayerSpriteName = "playerSprite";
        private const string movingPlayerSprite = "playerSpriteMoving";
        private const string jumpingDPlayerSprite = "playerSpriteJumpingDynamic";
        private const string jumpingSPlayerSprite = "playerSpriteJumpingStatic";
        public bool InAir { get; private set; }
        private ContentManager Content;
        public Weapon HeldWeapon { get; set; }
        public PlayerController(ContentManager content, int rows = 1, int columns = 1,  int xIn = 0, int yIn = 0, Weapon weapIn = null) 
            : base(content.Load<Texture2D>(basePlayerSpriteName), rows, columns, xIn, yIn, new PlayerControlledAI(), new PlayerManager())
        {
            Texture = content.Load<Texture2D>(basePlayerSpriteName);
            Content = content;
            HeldWeapon = weapIn;
        }

        private readonly int ScreenMovementMargin = (int) (Game.ScreenSize.X/5f);
        private const int ScreenMoveSpeed = 6;
        public override void Update()
        {
            base.DetectCollision();
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
            UpdateVelocity();
        }

        private string currentSprite;
        public void SwitchSprite(string nameOfFile, int rowsIn = 1, int columnsIn = 1)
        {
             if (!nameOfFile.Equals(currentSprite))
            {
                Rows = rowsIn;
                Columns = columnsIn;
                totalFrames = Rows * Columns;
                Texture = Content.Load<Texture2D>(nameOfFile);
                currentFrame = 0;
                Width = Texture.Width / Columns;
                Height = Texture.Height / Rows;
                SpriteRectangle.Width = Texture.Width / Columns;
                SpriteRectangle.Height = Texture.Height / Rows;
                currentSprite = nameOfFile;
            }
        }
        private KeyboardState oldState;
        private KeyboardState newState;
        private const int moveSpeed = 20;
        private const int jumpSpeed = 500;
        public void CheckKeys()
        {
            newState = Keyboard.GetState();
            if (South)
            {
                InAir = false;
                velocity.Y = 0;
            }
            else
            {
                velocity.Y += 1;
            }
            if (RenderHandler.CurrentGameState == GAMESTATE.ACTIVE)
            {
                if (newState.IsKeyDown(Keys.A))
                {
                    velocity.X = !West ? velocity.X - moveSpeed : 0;
                    if (!InAir && oldState.IsKeyUp(Keys.A))
                    {
                        SwitchSprite(movingPlayerSprite);
                    }
                }
                if (newState.IsKeyDown(Keys.D))
                {
                    velocity.X = !East ? velocity.X + moveSpeed : 0;
                    if (!InAir && oldState.IsKeyUp(Keys.D))
                    {
                        SwitchSprite(movingPlayerSprite);
                    }
                }
                if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    if (!InAir)
                    {
                        InAir = true;
                        velocity.Y -= jumpSpeed;
                        SwitchSprite(jumpingDPlayerSprite, 1, 1);
                    }
                }
                //if (newState.IsKeyDown(Keys.S)) { }
                //if (newState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q)){}
                //if (newState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E)){}
                
            }
            if (North) velocity.Y = -velocity.Y;
            if (velocity.X == 0 && !InAir) SwitchSprite(basePlayerSpriteName,1,1);
            oldState = newState;
        }
    }
}
