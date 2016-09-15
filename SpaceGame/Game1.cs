using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacegame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        VertexPositionTexture[] vertices;

        Effect effect;

        Log log;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionTexture[6];
            vertices[0] = new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0), Vector2.Zero);
            vertices[1] = new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0), Vector2.One);
            vertices[2] = new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0), Vector2.UnitX);
            vertices[3] = vertices[0];
            vertices[4] = new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0), Vector2.UnitY);
            vertices[5] = vertices[1];

            effect = Content.Load<Effect>("circle");
            effect.Parameters["radiusMax"].SetValue(0.25f);
            effect.Parameters["radiusMin"].SetValue(0.22f);
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            log = new Log(this);
        }


        protected override void Update(GameTime gameTime)
        {
            log.Print("hello");
            log.Print("Position", new Vector2(120, 59));
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 2);


            log.Draw();
            base.Draw(gameTime);
        }
        
    }

    struct VertexPosition : IVertexType
    {
        public Vector3 Position;

        public VertexPosition(Vector3 position)
        {
            this.Position = position;
        }

        private static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0)
            );

        public VertexDeclaration VertexDeclaration
        {
            get
            {
                return vertexDeclaration;
            }
        }
    }
}
