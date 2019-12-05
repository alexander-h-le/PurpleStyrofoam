﻿using Microsoft.Xna.Framework;
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

namespace PurpleStyrofoam
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static double GameTimeMilliseconds;
        public static double GameTimeSeconds;
        public static readonly Vector2 ScreenSize = new Vector2(1920,1080);
        public static ContentManager GameContent;
        
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (int) ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int) ScreenSize.Y;
            //graphics.IsFullScreen = true;
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
            // TODO: Add your initialization logic here
            GameSaveHandler.Initialize();
            Debug.WriteLine(GameSaveHandler.PathDirectory);
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
            // TODO: use this.Content to load your game content here

            // ---------------------------------------------------------------------------------------------------------------------

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && Keyboard.GetState().IsKeyDown(Keys.LeftShift)) Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.N) && Keyboard.GetState().IsKeyDown(Keys.LeftShift)) GameSaveHandler.CreateSave();
            if (Keyboard.GetState().IsKeyDown(Keys.L) && Keyboard.GetState().IsKeyDown(Keys.LeftShift)) GameSaveHandler.LoadSave(GameSaveHandler.PathDirectory + "TestDocument.json");
            // TODO: Add your update logic here
            RenderHandler.Update();
            MouseHandler.Update(this.Content);
            GameTimeMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
            GameTimeSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            //rotationSprite.Angle += 0.01f;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //spriteBatch.DrawString(font, "Get dunked on", new Vector2(100, 100), Color.Black);
            //spriteBatch.End();
            RenderHandler.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
