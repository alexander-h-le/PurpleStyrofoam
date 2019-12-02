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
        protected Rectangle SpriteRectangle;
        protected int Width;
        protected int Height;
        public bool North { get; private set; }
        public bool South { get; private set; }
        public bool East { get; private set; }
        public bool West { get; private set; }
        protected Vector2 position;
        protected Vector2 velocity;
        protected const float gravity = -10f;
        protected readonly Vector2 terminalVelocity = new Vector2(400,700);
        public AIBase AI;

        public AnimatedSprite(Texture2D textIn, int rowsIn, int columnsIn, int xIn, int yIn, AIBase ai)
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
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
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
            this.X += (int)(velocity.X * (float)Game1.GameTimeSeconds);
            this.Y += (int)(velocity.Y * (float)Game1.GameTimeSeconds);
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
