using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Maps;
using System.Diagnostics;
using PurpleStyrofoam.AiController;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.Rendering.Menus;
using PurpleStyrofoam.Rendering.Menus.FullScreenMenus;
using PurpleStyrofoam.AiController.AIs;
using System;
using PurpleStyrofoam.Maps.Dungeon_Areas;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Rendering.Text;

namespace PurpleStyrofoam
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static double GameTimeMilliseconds;
        public static double GameTimeSeconds;
        public static Vector2 ScreenSize;
        public static ContentManager GameContent;
        public static PlayerController PlayerCharacter;
        
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false;
            graphics.ApplyChanges();
            GameContent = this.Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            setScreenSize(graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            GameSaveHandler.Initialize();
            MenuHandler.ActiveFullScreenMenu = new GameStartMenu();
            RenderHandler.Initialize();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // -------------------------------------------------------------------------------------------------------------------

            // Create a new SpriteBatch, which can be used to draw textures.
            Debug.WriteLine("Loading Objects...");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //rotationSprite = new ItemSprite(Content.Load<Texture2D>("testIMG"), new Vector2(0,0), 400, 240, scale: 0.5f);
            //rotationSprite.Origin = new Vector2(rotationSprite.Texture.Width / 2, rotationSprite.Texture.Height / 2);
            Debug.WriteLine("Objects Loaded");

            // --------------------------------------------------------------------------------------------------------------------

            Debug.WriteLine("Adding objects to RenderHandler...");
            Debug.WriteLine("Finished adding objects to RenderHandler");

            // ---------------------------------------------------------------------------------------------------------------------

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected void setScreenSize(float w, float h)
        {
            ScreenSize = new Vector2(w,h);
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            graphics.ApplyChanges();

        }

        public static bool ShouldClose = false;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ShouldClose) Exit();
            KeyHelper.Update();
            RenderHandler.Update();
            MouseHandler.Update();
            if (KeyHelper.CheckTap(Keys.OemTilde)) DialogueHandler.Start(TestHelper.GetTestDialogues());
            GameTimeMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
            GameTimeSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            setScreenSize(graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            RenderHandler.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
