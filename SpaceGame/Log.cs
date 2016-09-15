using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacegame
{
    public class Log
    {
        private SpriteBatch spriteBatch;

        private List<string> messages = new List<string>();
        private SpriteFont font;

        public Log(Game game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");
            Flush();
        }

        public void Print(string text)
        {
            messages.Add(text);
        }
        public void Print(string text, object obj)
        {
            Print(text + ": " + obj.ToString());
        }
        public void Print(object obj)
        {
            Print(obj.ToString());
        }


        private void Flush()
        {
            messages.Clear();
        }

        public void Draw()
        {
            int screenPos = 20;
            int lineSpace = 20;
            spriteBatch.Begin();
            foreach (var message in messages)
            {
                spriteBatch.DrawString(font, message, new Vector2(10, screenPos), Color.Black);
                screenPos += lineSpace;
            }
            spriteBatch.End();
            Flush();
        }
    }
}
