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
        }

        public override void Update()
        {
            if (velocity.X != 0 || velocity.Y != 0) base.DetectCollision();
            CheckKeys();
            animate.Update();
            if (animate.Finished() && animate.Texture.Name == SpriteTextureHelper.Sprites.Dog) animate.Switch(PlayerManager.jumpingSPlayerSprite, SpriteRectangle, 1, 1);
            UpdateVelocity();
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
                    if (!InAir) animate.Switch(PlayerManager.movingPlayerSprite, SpriteRectangle);
                }
                if (KeyHelper.CheckHeld(Keys.D))
                {
                    velocity.X += velocity.X < terminalVelocity.X ? moveSpeed : 0;
                    if (!InAir) animate.Switch(PlayerManager.movingPlayerSprite, SpriteRectangle);
                }
                if (KeyHelper.CheckTap(Keys.Space))
                {
                    if (!InAir)
                    {
                        InAir = true;
                        velocity.Y -= jumpSpeed;
                        animate.Switch(SpriteTextureHelper.Sprites.Dog, SpriteRectangle, 4, 4);
                    }
                }
                //if (newState.IsKeyDown(Keys.S)) { }
                if (KeyHelper.CheckTap(Keys.Q)){
                    ((PlayerManager) Manager).EquippedWeapon.OnQAbility();
                }
                if (KeyHelper.CheckTap(Keys.E))
                {
                    ((PlayerManager) Manager).Class.EAction();
                }
                
            }
            if (North) velocity.Y = -velocity.Y;
            if ((velocity.X > -1 && velocity.X < 1) && !InAir) animate.Switch(PlayerManager.basePlayerSpriteName, SpriteRectangle, 1,1);
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
