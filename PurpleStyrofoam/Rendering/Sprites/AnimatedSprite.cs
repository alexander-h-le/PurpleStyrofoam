using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.Rendering;
using System.Diagnostics;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Managers;
using PurpleStyrofoam.Rendering.Animations;
using PurpleStyrofoam.Buffs;
using PurpleStyrofoam.Rendering.Text;
using PurpleStyrofoam.Buffs.CommonBuffs;
using PurpleStyrofoam.Helpers;

namespace PurpleStyrofoam
{
    public class AnimatedSprite : GameObject
    {
        public Rectangle SpriteRectangle;
        private Point xy;
        public bool North { get; protected set; }
        public bool South { get; protected set; }
        public bool East { get; protected set; }
        public bool West { get; protected set; }
        public Animation animate;
        public Vector2 velocity;
        protected const float gravity = -20f;
        public Vector2 terminalVelocity = new Vector2(DefaultTerminalVelocity.X,DefaultTerminalVelocity.Y);
        public AIBase AI;
        public BaseManager Manager;
        public BuffHandler Buffs;
        public DamageIndicator damageIndicator;

        public static Vector2 DefaultTerminalVelocity = new Vector2(400,700);

        public AnimatedSprite(string TextureName, int rows, int columns, int xIn, int yIn, AIBase ai, BaseManager manIn)
        {
            animate = new Animation(TextureName, rows, columns);
            AI = ai;
            xy = new Point(xIn,yIn);
            Manager = manIn;
            Buffs = new BuffHandler();
            damageIndicator = new DamageIndicator();
        }
        public virtual void DetectCollision()
        {
            bool[] array = CollisionDetection.DetectCollisionArrayMap(SpriteRectangle);
            North = array[0];
            South = array[1];
            East = array[2];
            West = array[3];

            AnimatedSprite[] temp;
            CollisionDetection.DetectCollisionSprites(this, SpriteRectangle, out temp);
            if (temp.Contains(Game.PlayerCharacter)) Game.PlayerCharacter.AddHealth(-Manager.Damage);
        }
        public override void Update()
        {
            if (velocity.X != 0 || velocity.Y != 0) DetectCollision();
            if (South) velocity.Y = 0;
            if (North && velocity.Y < 0) velocity.Y = 0;
            if (East && velocity.X > 0) velocity.X = 0;
            if (West && velocity.X < 0) velocity.X = 0;
            UpdateVelocity();
            AI.NextMove();
            damageIndicator.Update(SpriteRectangle);
        }

        public virtual void UpdateVelocity()
        {
            velocity.Y -= gravity;
            if (velocity.Y > terminalVelocity.Y) velocity.Y = terminalVelocity.Y;
            if (velocity.Y < -terminalVelocity.Y) velocity.Y = -terminalVelocity.Y;


            SpriteRectangle.X += (int)(velocity.X * (float)Game.GameTimeSeconds);
            SpriteRectangle.Y += (int)(velocity.Y * (float)Game.GameTimeSeconds);
        }

        public virtual void AddHealth(int amt)
        {
            if (Buffs.HasBuff(typeof(InvincibleBuff))) return;
            if (Manager.Health + amt <= Manager.MaxHealth)
            {
                if (Manager.Health + amt > 0)
                {
                    Manager.Health += amt;
                    if (amt > 0) damageIndicator.NewDamage(amt, Color.Green);
                    else
                    {
                        damageIndicator.NewDamage(amt, Color.Red);
                        Buffs.AddBuff(new InvincibleBuff(25));
                    }
                }
                else if (this != Game.PlayerCharacter) Delete();
            }
            else Manager.Health = Manager.MaxHealth;
        }

        public virtual void AddHealthIgnoreInvincibility(int amt)
        {
            if (Manager.Health + amt <= Manager.MaxHealth)
            {
                if (Manager.Health + amt > 0)
                {
                    Manager.Health += amt;
                    if (amt > 0) damageIndicator.NewDamage(amt, Color.Green);
                    else
                    {
                        damageIndicator.NewDamage(amt, Color.Red);
                        Buffs.AddBuff(new InvincibleBuff(50));
                    }
                }
                else if (this != Game.PlayerCharacter) Delete();
            }
            else Manager.Health = Manager.MaxHealth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animate.Draw(spriteBatch, SpriteRectangle);
            damageIndicator.Draw(spriteBatch);
        }

        public override void Load()
        {
            animate.Load();
            SpriteRectangle = new Rectangle(xy.X, xy.Y, animate.Texture.Width / animate.Columns, animate.Texture.Height / animate.Rows);
            ObjectMapper.AddSpriteObject(this);
        }

        public void Delete()
        {
            RenderHandler.purgeSprites.Add(this);
        }
    }
}
