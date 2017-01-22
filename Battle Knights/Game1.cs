using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Battle_Knights
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        public int turnPhase;
        public int unit;
        public Cursor cursor;
        public Rectangle waypointRectangle;
        public Rectangle cursorRectangle;
        public Texture2D cursorYellowTexture;
        public Texture2D cursorGreenTexture;
        public Texture2D cursorRedTexture;
        public Knight knight;
        public Knight knight2;
        public Knight knight3;
        public Texture2D knightTexture;
        public List<Knight> knightList;
        public Knight redKnight;
        public Knight redKnight2;
        public Knight redKnight3;
        public Texture2D goblinTexture;
        public Texture2D moveSquareTexture;
        public Texture2D healthBarTexture;
        public Rectangle movesquareRectangle;
        public InputHandler handlerInput;
        public Double distanceToWaypoint;
        public Double xDistanceToWaypoint;
        public Double yDistanceToWaypoint;
        public String playersTurn;
        public bool intersects;
        public int time;
        public String testS;
        public int testCount;

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
            Init();
            base.Initialize();
            SetTextures();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void Init()
        {
            handlerInput = new InputHandler(this);
            font = Content.Load<SpriteFont>(@"Arial");
            cursor = new Cursor(cursorRectangle, this);
            cursor.cursorRectangle = new Rectangle(380, 220, 20, 20);
            //knight = new Knight(new Vector2(100, 100), "footman", "Blue", this);
            //knight2 = new Knight(new Vector2(200,200), "archer", "Blue", this);
            //knight3 = new Knight(knightRectangle, knightTexture, this);
            //knight3.rectangle = new Rectangle(0, 100, 31, 31);
            redKnight = new Knight(new Vector2(300, 400), "footman", "Red", this);
            //redKnight2 = new Knight(new Vector2(400, 400), "footman", "Red", this);
            //redKnight3 = new Knight(knightRectangle, knightTexture, this);
            //redKnight3.rectangle = new Rectangle(0, 100, 31, 31);
            turnPhase = 0;
            unit = 0;
            playersTurn = "Blue";
            knightList = new List<Knight>();
            knightList.Add(redKnight);
            //knightList.Add(redKnight2);
            //knightList.Add(redKnight3);
            //knightList.Add(knight);
            //knightList.Add(knight2);
            //knightList.Add(knight3);
            intersects = false;
            time = 0;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            goblinTexture = Content.Load<Texture2D>(@"images\goblin");
            cursorYellowTexture = Content.Load<Texture2D>(@"images\cursoryellow");
            cursorGreenTexture = Content.Load<Texture2D>(@"images\cursorgreen");
            cursorRedTexture = Content.Load<Texture2D>(@"images\cursorred");
            moveSquareTexture = Content.Load<Texture2D>(@"images\movearea");
            healthBarTexture = Content.Load<Texture2D>(@"images\healthbar");
            SetTextures();
            // TODO: use this.Content to load your game content here
        }
        public void SetTextures()
        {
            for (int i = 0; i < knightList.Count; i++)
            {
                if (knightList[i].type == "footman")
                {
                    knightList[i].texture = knightTexture;
                }
            }
            cursor.cursorTexture = cursorYellowTexture;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        public void RemoveKnights()
        {
            int toRemove = -1;
            for (int i = 0; i < knightList.Count; i++)
            {
                if (knightList[i].health <= 0)
                    toRemove = i;
            }
            if (toRemove != -1)
            {
                knightList.RemoveAt(toRemove);
                toRemove = -1;
            }
        }
        public void CheckIntersect()
        {
            for (int i = 0; i < knightList.Count; i++)
            {
                if (knightList[unit].rectangle.Intersects(knightList[i].rectangle) && i != unit)
                {
                    intersects = true;
                }
            }
            while (intersects == true)
            {
                xDistanceToWaypoint = knightList[unit].center.X - waypointRectangle.X;
                yDistanceToWaypoint = knightList[unit].center.Y - waypointRectangle.Y;
                if (yDistanceToWaypoint > 0)
                {
                    waypointRectangle.X += 1;
                }
            }

        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            handlerInput.InputControl();
            cursor.Update();
            for (int i = 0; i < knightList.Count; i++)
            {
                knightList[i].Update();
            }
            xDistanceToWaypoint = knightList[unit].center.X - waypointRectangle.X;
            yDistanceToWaypoint = knightList[unit].center.Y - waypointRectangle.Y;
            distanceToWaypoint = Math.Sqrt(Math.Pow(xDistanceToWaypoint, 2) + Math.Pow(yDistanceToWaypoint, 2));
            if (yDistanceToWaypoint <= knightList[unit].speed && yDistanceToWaypoint >= -knightList[unit].speed)
            {
                distanceToWaypoint = xDistanceToWaypoint;
            }
            if (xDistanceToWaypoint <= knightList[unit].speed && xDistanceToWaypoint >= -knightList[unit].speed)
            {
                distanceToWaypoint = yDistanceToWaypoint;
            }
            RemoveKnights();
            time += 1;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (turnPhase == 1)
            {
                spriteBatch.Draw(moveSquareTexture, movesquareRectangle, Color.White);
            }
            for (int i = 0; i < knightList.Count; i++)
            {
                spriteBatch.Draw(knightList[i].texture, knightList[i].rectangle, knightList[i].color);
                spriteBatch.Draw(healthBarTexture, knightList[i].healthBar, Color.Red);
                //spriteBatch.DrawString(font, "" + knightList[i].health, new Vector2(knightList[i].healthBar.X, knightList[i].healthBar.Y), Color.Black);
                spriteBatch.Draw(knightList[i].weaponTexture, knightList[i].weaponRectangle, knightList[i].color);
                spriteBatch.Draw(healthBarTexture, knightList[i].hand, Color.Blue);
            }
            spriteBatch.Draw(cursor.cursorTexture, cursor.cursorRectangle, Color.White);
            testCount += 1;
            if (testCount == 20)
            {
                testS = (xDistanceToWaypoint < knightList[0].speed) + "," + (xDistanceToWaypoint > -knightList[0].speed);
                testCount = 0;
            }
            spriteBatch.DrawString(font, "" + knightList[0].hand.X + "," + knightList[0].hand.Y, Vector2.Zero, Color.Black);
            Vector2 _tempVec = new Vector2(0, 15);
            spriteBatch.DrawString(font, "" + knightList[0].rectangle.X + "," + knightList[0].rectangle.Y, _tempVec, Color.Black);
            //spriteBatch.DrawString(font, "" + time, Vector2.Zero, Color.Black);
            //spriteBatch.DrawString(font, "" + knightList[3].health, Vector2.Zero, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
