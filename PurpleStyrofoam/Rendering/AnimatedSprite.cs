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
        protected const float gravity = -9.8f;
        protected readonly Vector2 terminalVelocity = new Vector2(300,500);

        public AnimatedSprite(Texture2D textIn, int rowsIn, int columnsIn, int xIn, int yIn)
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
        }
        public void DetectCollision()
        {
            foreach (MapObject map in RenderHandler.selectedMap.ActiveLayer)
            {
                if (SpriteRectangle.Intersects(map.MapRectangle))
                {
                    //Debug.Write($"{map.name} intersects {this.GetType().Name}");

                    //EAST & WEST
                    if (SpriteRectangle.Right >= map.MapRectangle.Left && CenterX < map.MapRectangle.Left)
                    {
                        //Debug.Write(" at east\n");
                        East = true;
                    } else if (SpriteRectangle.Left <= map.MapRectangle.Right && CenterX > map.MapRectangle.Right)
                    {
                        //Debug.Write(" at west\n");
                        West = true;
                    } else if (SpriteRectangle.Top <= map.MapRectangle.Bottom && CenterY > map.MapRectangle.Top)
                    {
                        //Debug.Write(" at north\n");
                        North = true;
                    } else if (SpriteRectangle.Bottom >= map.MapRectangle.Top && CenterY < map.MapRectangle.Bottom)
                    {
                        //Debug.Write(" at south\n");
                        South = true;
                    }
                    //Debug.WriteLine($"CenterX: {CenterX}, CenterY: {CenterY}, Left: {SpriteRectangle.Left}, Right: {SpriteRectangle.Right}, Top: {SpriteRectangle.Top}, Bottom: {SpriteRectangle.Bottom} " +
                        //$"\nmapLeft: {map.MapRectangle.Left}, mapRight: {map.MapRectangle.Right}, mapTop: {map.MapRectangle.Top}, mapBottom: {map.MapRectangle.Bottom}");
                    break;
                } else
                {
                    East = false;
                    West = false;
                    North = false;
                    South = false;
                }
            }
        }
        public virtual void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
            UpdateVelocity();
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
