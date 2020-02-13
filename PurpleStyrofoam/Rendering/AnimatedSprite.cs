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

namespace PurpleStyrofoam
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        protected int currentFrame;
        protected int totalFrames;
        public Rectangle SpriteRectangle;
        public bool North { get; private set; }
        public bool South { get; private set; }
        public bool East { get; private set; }
        public bool West { get; private set; }
        protected Vector2 position;
        public Vector2 velocity;
        protected const float gravity = -20f;
        protected readonly Vector2 terminalVelocity = new Vector2(400,700);
        public AIBase AI;
        public IManager Manager;

        public AnimatedSprite(string TextureName, int rowsIn, int columnsIn, int xIn, int yIn, AIBase ai, IManager manIn)
        {
            Texture = Game.GameContent.Load<Texture2D>(TextureName);
            Rows = rowsIn;
            Columns = columnsIn;
            currentFrame = 0;
            SpriteRectangle = new Rectangle(xIn, yIn, Texture.Width / Columns, Texture.Height / Rows);
            AI = ai;
            Manager = manIn;
        }
        public void DetectCollision()
        {
            bool[] array = CollisionDetection.DetectCollisionArrayMap(SpriteRectangle);
            North = array[0];
            South = array[1];
            East = array[2];
            West = array[3];
        }
        public virtual void Update()
        {
            if (velocity.X != 0 || velocity.Y != 0) DetectCollision();
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
            if (South) velocity.Y = 0;
            if (North) velocity.Y = -velocity.Y;
            UpdateVelocity();
            AI.NextMove();
        }

        public virtual void UpdateVelocity()
        {
            velocity.Y -= gravity;
            if (velocity.Y > terminalVelocity.Y) velocity.Y = terminalVelocity.Y;
            if (velocity.Y < -terminalVelocity.Y) velocity.Y = -terminalVelocity.Y;
            SpriteRectangle.X += (int)(velocity.X * (float)Game.GameTimeSeconds);
            SpriteRectangle.Y += (int)(velocity.Y * (float)Game.GameTimeSeconds);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(SpriteRectangle.Width * column, SpriteRectangle.Height * row, SpriteRectangle.Width, SpriteRectangle.Height);

            spriteBatch.Draw(Texture, SpriteRectangle, sourceRectangle, Color.White);
        }
    }
}
