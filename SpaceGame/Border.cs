using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacegame
{
    public class Border
    {
        public Rectangle InnerBounds { get; set; }
        public int BorderSize { get; set; }

        public Rectangle OuterBounds
        {
            get
            {
                Rectangle result = InnerBounds;
                result.Inflate(BorderSize, BorderSize);
                return result;
            }
        }

        public Color Color { get; set; }

        Texture2D texture;

        public Border(ContentManager content)
        {
            texture = content.Load<Texture2D>("border");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle bounds = OuterBounds;
            //top
            spriteBatch.Draw(texture, new Rectangle(InnerBounds.Left, bounds.Top, InnerBounds.Width, BorderSize), new Rectangle(15, 0, 1, 16), Color);
            //left
            spriteBatch.Draw(texture, new Rectangle(bounds.X, InnerBounds.Top, BorderSize, InnerBounds.Height), new Rectangle(0, 15, 16, 1), Color);
            //bottom
            spriteBatch.Draw(texture, new Rectangle(InnerBounds.Left, InnerBounds.Bottom, InnerBounds.Width, BorderSize), new Rectangle(15, 0, 1, 16), Color);
            //right
            spriteBatch.Draw(texture, new Rectangle(InnerBounds.Right, InnerBounds.Top, BorderSize, InnerBounds.Height), new Rectangle(0, 15, 16, 1), Color);

            Vector2 origin = new Vector2(0,0);

            //top-left
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, BorderSize, BorderSize), null, Color, 0, origin, SpriteEffects.None, 0);
            //top-right
            spriteBatch.Draw(texture, new Rectangle(InnerBounds.Right+BorderSize, bounds.Top, BorderSize, BorderSize), null, Color, MathHelper.PiOver2, origin, SpriteEffects.None, 0);
            //bottom-left
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Bottom, BorderSize, BorderSize), null, Color, MathHelper.Pi+MathHelper.PiOver2, origin, SpriteEffects.None, 0);
            //bottom-right
            spriteBatch.Draw(texture, new Rectangle(bounds.Right, bounds.Bottom, BorderSize, BorderSize), null, Color, MathHelper.Pi, origin, SpriteEffects.None, 0);

        }


    }
}
