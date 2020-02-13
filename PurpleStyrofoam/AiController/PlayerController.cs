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
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Helpers;

namespace PurpleStyrofoam.AiController
{
    public class PlayerController : AnimatedSprite
    {
        public bool InAir { get; private set; }

        public PlayerController(PlayerManager manager) 
            : base(PlayerManager.basePlayerSpriteName, 1, 1, 100, 100, new PlayerControlledAI(), manager)
        {
            Texture = Game.GameContent.Load<Texture2D>(PlayerManager.basePlayerSpriteName);
            Manager = manager == null ? new PlayerManager() : manager;
        }

        public override void Update()
        {
            if (velocity.X != 0 || velocity.Y != 0) base.DetectCollision();
            CheckKeys();
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                if (currentSprite == PlayerManager.jumpingDPlayerSprite) SwitchSprite(PlayerManager.jumpingSPlayerSprite);
                currentFrame = 0;
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
                Texture = Game.GameContent.Load<Texture2D>(nameOfFile);
                currentFrame = 0;
                SpriteRectangle.Width = Texture.Width / Columns;
                SpriteRectangle.Height = Texture.Height / Rows;
                currentSprite = nameOfFile;
            }
        }
        private const int moveSpeed = 20;
        private const int jumpSpeed = 1500;
        public void CheckKeys()
        {
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
                if (KeyHelper.CheckHeld(Keys.A))
                {
                    velocity.X -= velocity.X > -terminalVelocity.X ? moveSpeed : 0;
                    if (!InAir) SwitchSprite(PlayerManager.movingPlayerSprite);
                }
                if (KeyHelper.CheckHeld(Keys.D))
                {
                    velocity.X += velocity.X < terminalVelocity.X ? moveSpeed : 0;
                    if (!InAir) SwitchSprite(PlayerManager.movingPlayerSprite);
                }
                if (KeyHelper.CheckTap(Keys.Space))
                {
                    if (!InAir)
                    {
                        InAir = true;
                        velocity.Y -= jumpSpeed;
                        SwitchSprite(PlayerManager.jumpingDPlayerSprite, 1, 1);
                    }
                }
                //if (newState.IsKeyDown(Keys.S)) { }
                if (KeyHelper.CheckTap(Keys.Q)){
                    ((PlayerManager) Manager).EquippedWeapon.OnQAbility();
                }
                if (KeyHelper.CheckTap(Keys.E))
                {
                    ((PlayerManager)Manager).Class.EAction();
                }
                
            }
            if (North) velocity.Y = -velocity.Y;
            if ((velocity.X > -1 && velocity.X < 1) && !InAir) SwitchSprite(PlayerManager.basePlayerSpriteName,1,1);
        }
        public override void UpdateVelocity()
        {
            velocity.Y -= velocity.Y < terminalVelocity.Y ?  gravity : 0;
            velocity.X -= velocity.X == 0 ? 0 : velocity.X > 0 ? 5 : -5;
            if (East && velocity.X > 0) velocity.X = 0;
            if (West && velocity.X < 0) velocity.X = 0;
            if (velocity.Y < -terminalVelocity.Y) velocity.Y = -terminalVelocity.Y;
            SpriteRectangle.X += (int)(velocity.X * (float)Game.GameTimeSeconds);
            SpriteRectangle.Y += (int)(velocity.Y * (float)Game.GameTimeSeconds);
            if (KeyHelper.CheckHeld(Keys.A)) ((PlayerManager)Manager).EquippedWeapon.Sprite.ItemRectangle.X =
                    SpriteRectangle.Left - ((PlayerManager)Manager).EquippedWeapon.Sprite.ItemRectangle.Width;
            else ((PlayerManager)Manager).EquippedWeapon.Sprite.ItemRectangle.X = SpriteRectangle.Right;
            ((PlayerManager)Manager).EquippedWeapon.Sprite.ItemRectangle.Y = SpriteRectangle.Y;
        }
    }
}
