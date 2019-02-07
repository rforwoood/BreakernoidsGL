using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace BreakernoidsGL
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public Paddle paddle;
        public Block testBlock;
        public Ball ball;
        public float SlidingHue = 0.0f;
        public bool UpDownBool = true;
        public bool BallCentered = false;
        public bool BallLeftRight = false;
        public bool AboveBelowBlock = false;
        public bool LeftRightBlock = false;
        public int CollisionClock = 0;
        public List<Block> Blocks = new List<Block>();
        public Vector2 CurrentBlockPos;

        /// <summary>
        /// These variables were used to try and do randomized colors
        /// </summary>
        //public Random rnd = new Random();
        //public float RandomHue1;
        //public float RandomHue2;
        //public Vector4 ColorRGB1;
        //public Vector4 ColorRGB2;
        //public Color RandomColor1;
        //public Color RandomColor2;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // bg sprite
        Texture2D bgTexture;

        /// <summary>
        /// Not working color randomizer, learn more about classes thewn come back to this
        /// </summary>
        //public class RandomRGB
        //{
        //    Random rnd = new Random();

        //    public Color RandomColor()
        //    {
        //        byte R = (byte)rnd.Next(0, 255);
        //        byte G = (byte)rnd.Next(0, 255);
        //        byte B = (byte)rnd.Next(0, 255);
        //        return new Color(R, G, B);
        //    }
        //}

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
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

            base.Initialize();
        }

        protected void CheckCollisionPos()
        {
            if ((ball.position.X) < (paddle.position.X - paddle.Width/3))
            {
                // if ball is left of center
                BallCentered = false;
                BallLeftRight = false;
            }
            if ((ball.position.X) > (paddle.position.X + paddle.Width / 3))
            {
                // if ball is left of center
                BallCentered = false;
                BallLeftRight = true;
            }
            if (((ball.position.X) > (paddle.position.X - paddle.Width / 3)) && ((ball.position.X) < (paddle.position.X + paddle.Width / 3)))
            {
                // if ball is centered
                BallCentered = true;
            }
        }
        protected void CheckCollisionPosBlock()
        {
            if (((ball.position.X) <= (CurrentBlockPos.X - testBlock.Width / 2)) || ((ball.position.X) >= (CurrentBlockPos.X + testBlock.Width / 2)))
            {
                // if ball is left or right of block
                LeftRightBlock = true;
            }
            else if (((ball.position.Y) <= (CurrentBlockPos.Y - testBlock.Height / 2)) || ((ball.position.Y) >= (CurrentBlockPos.Y + testBlock.Height / 2)))
            {
                // if ball is above or below block
                AboveBelowBlock = true;
            }
            else
            {
                // if something goes wrong, just do both
                LeftRightBlock = true;
                AboveBelowBlock = true;
            }
        }
        

        protected void LoseLife()
        {
            //reset ball
            ball.Direction = new Vector2(0.707f, -0.707f);
            ball.position = new Vector2((paddle.position.X), (paddle.position.Y - ball.Height - paddle.Height));

            //reset paddle
            paddle.position = new Vector2(512, 740);

        }

        protected void CheckCollisions()
        {
            float radius = ball.Width / 2;

            //paddle collision
            if ((ball.position.X > (paddle.position.X - radius - paddle.Width / 2)) && (ball.position.X < (paddle.position.X + radius + paddle.Width / 2)) && (ball.position.Y < paddle.position.Y) && (ball.position.Y > (paddle.position.Y - radius - paddle.Height / 2)) && (CollisionClock == 0))
            {
                CollisionClock = 20;
                CheckCollisionPos();
                if (BallCentered == true)
                {
                    // if ball hits center
                    ball.Direction = Vector2.Reflect(ball.Direction, new Vector2(0, -1)); ;
                    BallCentered = false;
                }
                else if ((BallCentered != true) && (BallLeftRight == false))
                {
                    // if ball hits left
                    ball.Direction = Vector2.Reflect(ball.Direction, new Vector2(-0.196f, -0.981f));
                }
                else if ((BallCentered != true) && (BallLeftRight == true))
                {
                    // if ball hits right
                    ball.Direction = Vector2.Reflect(ball.Direction, new Vector2(0.196f, -0.981f));
                }               
            }

            //block collisions
            foreach (Block b in Blocks)
            {
                if ((ball.position.X > (b.position.X - radius - b.Width / 2)) && (ball.position.X < (b.position.X + radius + b.Width / 2)) && (ball.position.Y < b.position.Y) && (ball.position.Y > (b.position.Y - radius - b.Height / 2)))
                {
                    CurrentBlockPos = new Vector2(b.position.X, b.position.Y);
                    CheckCollisionPosBlock();

                    if (AboveBelowBlock == true)
                    {
                        // if ball hits below or above invert y dir
                        ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
                    }
                    else if (LeftRightBlock == true)
                    {
                        // if ball hits left or right invert x dir
                        ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
                    }
                    else
                    {
                        //if something went wrong just invert both
                        ball.Direction = new Vector2(-ball.Direction.X, -ball.Direction.Y);
                    }
                    LeftRightBlock = false;
                    AboveBelowBlock = false;
                    Blocks.Remove(b);
                    break;
                }
            }

            // wall collisions
            if (Math.Abs(ball.position.X - 32) < radius)
            {
                // Left wall collision
                ball.Direction.X = -ball.Direction.X;

            }
            else if (Math.Abs(ball.position.X - 992) < radius)
            {
                // Right wall collision
                ball.Direction.X = -ball.Direction.X;

            }
            else if (Math.Abs(ball.position.Y - 32) < radius)
            {
                // Top wall collision
                ball.Direction.Y = -ball.Direction.Y;
            }
            else if (Math.Abs(ball.position.Y - 768) < radius)
            {
                // Bottom lost ball
                LoseLife();
            }
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
            bgTexture = Content.Load<Texture2D>("bg");

            for (int i = 0; i < 15; i++)
            {
                Block tempBlock = new Block(this);
                tempBlock.LoadContent();
                tempBlock.position = new Vector2(64 + i * 64, 200);
                Blocks.Add(tempBlock);
            }

            paddle = new Paddle(this);
            paddle.LoadContent();
            paddle.position = new Vector2(512, 740);
            ball = new Ball(this);
            ball.LoadContent();
            ball.position = new Vector2((paddle.position.X), (paddle.position.Y - ball.Height - paddle.Height));
            testBlock = new Block(this);
            testBlock.LoadContent();
            testBlock.position = new Vector2(9999, 9999);
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
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            paddle.Update(deltaTime);
            ball.Update(deltaTime);
            CheckCollisions();
            if (CollisionClock > 0)
            {
                CollisionClock -= 1;
            }
            base.Update(gameTime);
            

            //// Unused random hues
            //RandomHue1 = (float)rnd.NextDouble();
            //RandomHue2 = (float)rnd.NextDouble();

            //Sliding Hues
            if (SlidingHue >= 1.0f)
            {
                UpDownBool = false;
                SlidingHue = 0.99f;
            }
            if (SlidingHue <= 0.0f)
            {
                UpDownBool = true;
                SlidingHue = 0.01f;
            }
            if (UpDownBool == true)
            {
                SlidingHue += 0.01f;
            }
            if (UpDownBool == false)
            {
                SlidingHue -= 0.01f;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //// Unused, random Hues between 2 nmanual colors
            //// DO NOT USE; EPILEPSY WARNING!!!
            //GraphicsDevice.Clear(Color.Lerp(Color.Lavender, Color.MediumPurple, RandomHue1));

            // Incomplete, bounces hues between 2 random colors
            //GraphicsDevice.Clear(Color.Lerp(RandomColor1, RandomColor2, SlidingHue));

            // TODO: Add your drawing code here

            // Bounces hues between 2 manual colors
            GraphicsDevice.Clear(Color.Lerp(Color.LightPink, Color.Aquamarine, SlidingHue));

            // Draw background
            spriteBatch.Begin();
            // Draw all sprites here
            spriteBatch.Draw(bgTexture, new Vector2(0, 0), Color.White);
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            foreach (Block b in Blocks)
            {
                b.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
