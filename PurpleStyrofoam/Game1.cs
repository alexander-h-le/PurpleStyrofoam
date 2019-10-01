using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Maps;
using System.Diagnostics;
using PurpleStyrofoam.AiController;

namespace PurpleStyrofoam
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D testIMG;
        ItemSprite rotationSprite;
        SpriteFont font;
        private PlayerController player;
        TestMap tM;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            // Create a new SpriteBatch, which can be used to draw textures.
            Debug.WriteLine("Loading Objects...");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testIMG = Content.Load<Texture2D>("testIMG");
            font = Content.Load<SpriteFont>("test");
            //Texture2D playerIMG = Content.Load<Texture2D>("SmileyWalk");
            player = new PlayerController(this.Content);
            tM = new TestMap(this.Content);
            RenderHandler.selectedMap = tM;
            rotationSprite = new ItemSprite(Content.Load<Texture2D>("testIMG"), new Vector2(0,0), 400, 240);
            rotationSprite.Origin = new Vector2(rotationSprite.Texture.Width / 2, rotationSprite.Texture.Height / 2);
            Debug.WriteLine("Objects Loaded");
            Debug.WriteLine("Adding objects to RenderHandler...");
            RenderHandler.Add(rotationSprite);
            RenderHandler.Add(player);
            Debug.WriteLine("Finished adding objects to RenderHandler");
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update();
            MouseHandler.Update();
            //rotationSprite.Angle += 0.01f;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (RenderHandler.selectedMap != null) RenderHandler.selectedMap.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Get dunked on", new Vector2(100, 100), Color.Black);
            spriteBatch.End();
            foreach (AnimatedSprite item in RenderHandler.allCharacterSprites)
            {
                item.Draw(spriteBatch);
            }
            foreach (ItemSprite item in RenderHandler.allItemSprites)
            {
                item.Draw(spriteBatch);
            }
            if (RenderHandler.selectedMap != null) RenderHandler.selectedMap.DrawForeground(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
