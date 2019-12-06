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
        private int x;
        public int X {
            get
            {
                return x;
            }
            set
            {
                x = value;
                SpriteRectangle.X = value;
                CenterX = X + (Width / 2);
            }
        }
        private int y;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                SpriteRectangle.Y = value;
                CenterY = Y + (Height / 2);
            }
        }
        protected int CenterX;
        protected int CenterY;
        public Rectangle SpriteRectangle;
        protected int Width;
        protected int Height;
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

        public AnimatedSprite(Texture2D textIn, int rowsIn, int columnsIn, int xIn, int yIn, AIBase ai, IManager manIn)
        {
            Texture = textIn;
            Rows = rowsIn;
            Columns = columnsIn;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            X = xIn;
            Y = yIn;
            SpriteRectangle = new Rectangle(X, Y, Width, Height);
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
            DetectCollision();
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

        public void UpdateVelocity()
        {
            velocity.Y -= gravity;
            velocity.X -= velocity.X == 0 ? 0 : velocity.X > 0 ? 5 : -5;
            if (velocity.X > terminalVelocity.X) velocity.X = terminalVelocity.X;
            if (velocity.X < -terminalVelocity.X) velocity.X = -terminalVelocity.X;
            if (velocity.Y > terminalVelocity.Y) velocity.Y = terminalVelocity.Y;
            if (velocity.Y < -terminalVelocity.Y) velocity.Y = -terminalVelocity.Y;
            this.X += (int)(velocity.X * (float)Game.GameTimeSeconds);
            this.Y += (int)(velocity.Y * (float)Game.GameTimeSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);

            spriteBatch.Draw(Texture, SpriteRectangle, sourceRectangle, Color.White);
        }
    }
}
