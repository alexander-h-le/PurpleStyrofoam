using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        protected int currentFrame;
        protected int totalFrames;
        public int X { get; set; } 
        public int Y { get; set; }
        public AnimatedSprite(Texture2D textIn, int rowsIn, int columnsIn, int xIn, int yIn)
        {
            Texture = textIn;
            Rows = rowsIn;
            Columns = columnsIn;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            X = xIn;
            Y = yIn;
        }

        public virtual void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle(X, Y,  width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
