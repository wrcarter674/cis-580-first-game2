using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Paddle paddle;
        Paddle paddleRight;

        Texture2D Scoreboard;
        Texture2D RightScore;
        Texture2D LeftScore;
        Rectangle ScoreboardRect;
        Rectangle RightSCoreRect;
        Rectangle LeftScoreRect;


        public Random Random = new Random();        
        
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle = new Paddle(this);
            paddleRight = new Paddle(this);
            ball = new Ball(this);
        
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set the game screen size
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            ball.Initialize();
            paddle.Initialize(0);
            paddleRight.Initialize(1);

           
            ScoreboardRect.Y = 0;
            ScoreboardRect.Width = 200;
            ScoreboardRect.Height = 200;
            ScoreboardRect.X = graphics.PreferredBackBufferWidth/2-100;

            RightSCoreRect.Y = 80;
            RightSCoreRect.Width = 25;
            RightSCoreRect.Height = 25;
            RightSCoreRect.X = graphics.PreferredBackBufferWidth / 2 +60;

            LeftScoreRect.Y = 80;
            LeftScoreRect.Width = 25;
            LeftScoreRect.Height = 25;
            LeftScoreRect.X = graphics.PreferredBackBufferWidth / 2 - 95;
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
            ball.LoadContent(Content);
            paddle.LoadContent(Content);
            Scoreboard = Content.Load<Texture2D>("Scoreboard");
            RightScore = Content.Load<Texture2D>("Zero");
            LeftScore = Content.Load<Texture2D>("Zero");
            paddleRight.LoadContent(Content);
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
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            paddle.Update(gameTime, 0);
            paddleRight.Update(gameTime, 1);
            ball.Update(gameTime);

            if(paddle.Bounds.CollidesWith(ball.Bounds)) {
                ball.Velocity.X *= -1;
                var delta =  (paddle.Bounds.X + paddle.Bounds.Width) - (ball.Bounds.X - ball.Bounds.Radius);
                ball.Bounds.X += 2*delta;
            }

            if (paddleRight.Bounds.CollidesWith(ball.Bounds))
            {
                ball.Velocity.X *= -1;
                var delta = ball.Bounds.Radius;
                ball.Bounds.X -= delta;
            }

            // TODO: Add your update logic here

            oldKeyboardState = newKeyboardState;
            if(ball.LeftCount() == 1)
            {
                LeftScore = Content.Load<Texture2D>("One");
            }
            else if(ball.LeftCount() == 2)
            {
                LeftScore = Content.Load<Texture2D>("Twoa");
            }
            else if (ball.LeftCount() == 3)
            {
                LeftScore = Content.Load<Texture2D>("Three");
                Scoreboard = Content.Load<Texture2D>("LeftWinds");
                ball.Velocity = Vector2.Zero;
            }
       

            if (ball.RightCount() == 1)
            {
                RightScore = Content.Load<Texture2D>("One");
            }
            else if (ball.RightCount() == 2)
            {
                RightScore = Content.Load<Texture2D>("TwoA");

            }
            else if (ball.RightCount() == 3)
            {
                RightScore = Content.Load<Texture2D>("Three");
                Scoreboard = Content.Load<Texture2D>("RightWinds");
                ball.Velocity = Vector2.Zero;
            }
         
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
            spriteBatch.Begin();
            spriteBatch.Draw(Scoreboard, ScoreboardRect, Color.Red);
            spriteBatch.Draw(LeftScore, LeftScoreRect, Color.Red);
            spriteBatch.Draw(RightScore, RightSCoreRect, Color.Red);
            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            paddleRight.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
