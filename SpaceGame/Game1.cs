using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacegame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Screen screen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 400;
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;

            screen = new GameScreen(this);
            Components.Add(screen);

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
