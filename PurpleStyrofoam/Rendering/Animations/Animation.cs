using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PurpleStyrofoam.Rendering.Animations
{
    public class Animation
    {
        public Texture2D Texture;
        string image;
        public int Rows;
        public int Columns;
        int CurrentFrame;
        int TotalFrames;
        public Boolean Paused;
        public Animation(string texture, int rows, int columns)
        {
            image = texture;
            Rows = rows;
            Columns = columns;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Paused = true;
        }

        public void Draw(SpriteBatch sp, Rectangle location)
        {
            int row = (int)((double)CurrentFrame / Columns);
            int column = (int)((double)CurrentFrame % Columns);
            Rectangle sourceRectangle = new Rectangle(location.Width * column, location.Height * row, location.Width,
                location.Height);

            try { sp.Draw(Texture, location, sourceRectangle, Color.White); }
            catch (ArgumentNullException e)
            {
                throw new Exception("Texture was not properly loaded at: " + e.InnerException);
            }
        }

        public void Update()
        {
            if (!Paused)
            {
                if (CurrentFrame++ >= TotalFrames) CurrentFrame = 0;
            }
        }

        public void Load()
        {
            Texture = Game.GameContent.Load<Texture2D>(image);
        }

        public void Switch(string NewFile, Rectangle SpriteRect, int rows = 1, int columns = 1)
        {
            if (image == NewFile) return;
            Rows = rows;
            Columns = columns;
            image = NewFile;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Load();
            SpriteRect.Width = Texture.Width / Columns;
            SpriteRect.Height = Texture.Height / Rows;
            Play();
        }

        public Boolean Finished()
        {
            return CurrentFrame == TotalFrames;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Play()
        {
            Paused = false;
        }
    }
}
