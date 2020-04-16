using System;
using System.Diagnostics;
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
        public float Angle;
        public Vector2 Origin;
        public bool Flipped;
        public Animation(string texture, int rows, int columns)
        {
            image = texture;
            Rows = rows;
            Columns = columns;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Paused = true;
            Origin = new Vector2(0, 0);
            Angle = 0;
            Flipped = false;
        }

        public void Draw(SpriteBatch sp, Rectangle location)
        {
            if (Texture == null) Load();
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((double)CurrentFrame / Columns);
            int column = CurrentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            try
            {
                sp.Draw(Texture, location, sourceRectangle, Color.White, Angle, Origin,
                Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Texture was not properly loaded at: " + this);
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

        public void Rotate(float Radians)
        {
            Angle += Radians;
        }

        public int CurrentAngleInDegrees()
        {
            return (int) Math.Round( (180/Math.PI) * Angle );
        }

        public float CurrentAngleInRadians()
        {
            return Angle;
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
