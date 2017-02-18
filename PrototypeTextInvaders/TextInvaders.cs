using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using monOPPAI_Engine.UserInput;
using monOPPAI_Engine.Content;
using monOPPAI_Engine.ScreenStates;

using PrototypeTextInvaders.ScreenStates;

namespace PrototypeTextInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TextInvaders : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int SCREEN_WIDTH = 1024;
        private const int SCREEN_HEIGHT = 768;

        public TextInvaders()
        {
            //initialize graphics manager
            graphics = new GraphicsDeviceManager(this);
            //initialize Content Manager location at "Content" Folder
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
            this.Window.Title = "Prototype Text Invaders w/ C# & Monogame";

            Components.Add(new ControlHandler(this));
            //Font Dictionary needs a rewrite as MonoGame throws a non-implemented exception
            //Components.Add(new FontDictionary(Content, this));
            Components.Add(new AudioManager(this, null));
            Components.Add(new GameScreenManager(this, new SplashScreen()));
            new GameScreens(this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
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


            //GSM global instace needs to be drawn so all GS inheritors will appear on screen
            GameScreenManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
