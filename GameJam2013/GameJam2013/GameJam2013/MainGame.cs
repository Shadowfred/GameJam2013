using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FredLib.Utilities;
using FredLib.Input;
using GameJam2013.Objects;

namespace GameJam2013
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Game game;
        Map map;

        public event EventHandler EndGame;

        Player player1;
        Player player2;

        Texture2D heart;
        Texture2D bGround;
        Texture2D strip;

        CollideableManager cols;

        public static float fps = 0;
        int frames = 0;
        float elapsedTime = 0;

        public MainGame(Game g, SpriteBatch sB, GraphicsDeviceManager graph)
        {
            game = g;
            spriteBatch = sB;
            graphics = graph;
            cols = new CollideableManager();
        }

        public MainGame()
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
        public void Initialize()
        {
            Map.SetTileMapper();
        }

        SpriteFont font;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("tempFont");
            Texture2D dude = game.Content.Load<Texture2D>("PlayerSheet");
            Texture2D wall = game.Content.Load<Texture2D>("WallSheet");
            Texture2D rockWall = game.Content.Load<Texture2D>("RockWallSheet");
            Player.attackImage = game.Content.Load<Texture2D>("AttackSheet");
            heart = game.Content.Load<Texture2D>("HeartSheet");
            bGround = game.Content.Load<Texture2D>("Background");
            strip = game.Content.Load<Texture2D>("Strip");

            string[] data = 
            {
                "wwwwwwwwwwwwwwwwwwwwwwww",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w                      w",
                "w      w               w",
                "w                      w",
                "wwwwwwwwwwwwwwwwwwwwwwww"
            };

            map = new Map(0, 0, data[0].Length, data.Length);
            graphics.PreferredBackBufferWidth = data[0].Length * 32;
            graphics.PreferredBackBufferHeight = data.Length * 32 + 32;
            graphics.ApplyChanges();
            map.player = dude;
            map.wall = wall;
            map.rockWall = rockWall;


            map.SetMap(data, cols);
            player1 = new Player(dude, new Vector2(32 * 3, 32 * 11), 32, 32);

            Dictionary<string, Keys> controls = new Dictionary<string, Keys>();
            controls.Add("left", Keys.A);
            controls.Add("right", Keys.D);
            controls.Add("jump", Keys.W);
            controls.Add("eat", Keys.F);
            controls.Add("attack", Keys.E);
            player1.AddControls(controls);
            player1.SetColour(new Color(170, 170, 255));

            player2 = new Player(dude, new Vector2(32 * 20, 32 * 11), 32, 32);

            controls = new Dictionary<string, Keys>();
            controls.Add("left", Keys.J);
            controls.Add("right", Keys.L);
            controls.Add("jump", Keys.I);
            controls.Add("eat", Keys.H);
            controls.Add("attack", Keys.U);
            player2.AddControls(controls);
            player2.SetColour(new Color(255, 170, 170));

            cols.Add(player1, true);
            cols.Add(player2, true);

            //cols.Add(new Player(dude, new Vector2(50, 200), 32, 32), true);
            //cols.Add(new Wall(wall, new Vector2(50, 250), 32, 32), false);
            //cols.Add(new RockWall(rockWall, new Vector2(70, 228), 32, 32), false);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= 1)
            {
                fps = frames / (elapsedTime);
                frames = 0;
                elapsedTime -= 1;
            }
            cols.Update(gameTime, map);
            cols.Update(gameTime);
            if (player1.health == 0 || player2.health == 0)
            {
                if (player1.health == 0)
                {
                    Console.WriteLine("PLAYER 2 - RED - WINS");
                }
                else
                {
                    Console.WriteLine("PLAYER 1 - BLUE - WINS");
                }
                EndGame(this, new EventArgs());
            }
            map.Update();
        }

        public void HandleInput(Input input)
        {
            cols.HandleInput(input);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            //fps = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            frames++;

            spriteBatch.Begin();
            spriteBatch.Draw(bGround, Vector2.Zero, Color.White);
            spriteBatch.Draw(strip, new Vector2(0, 448), Color.White);
            cols.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, fps.ToString(), new Vector2(10, 10), Color.White);
            for (int i = 0; i < player1.health; i++)
            {
                spriteBatch.Draw(heart, new Vector2(8 + 20 * i, 456), Color.White);
            }
            spriteBatch.DrawString(font, player1.points.ToString(), new Vector2(120, 450), Color.Blue);
            for (int i = 0; i < player2.health; i++)
            {
                spriteBatch.Draw(heart, new Vector2(740 - 20 * i, 456), Color.White);
            }
            spriteBatch.DrawString(font, player2.points.ToString(), new Vector2(632, 450), Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here
        }
    }
}
