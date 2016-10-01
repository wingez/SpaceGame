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
    public class Board
    {
        public const int Width = 16;
        public const int Height = 20;
        public Point Size
        {
            get { return new Point(Width * Tile.Width, Height * Tile.Height); }
        }

        private Point position;
        public Point Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                border.InnerBounds = Bounds;
            }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position, Size); }
        }

        public Tile[,] Tiles;

        public Texture2D TileTexture { get; set; }

        Border border;

        public Board(ContentManager content)
        {
            Tiles = new Tile[Width, Height];

            TileTexture = content.Load<Texture2D>("pixel");

            border = new Border(content);
            border.Color = Color.Blue;
            border.BorderSize = 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            border.Draw(spriteBatch);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tile tile = Tiles[x, y];
                    if (tile != null)
                        spriteBatch.Draw(TileTexture, new Rectangle(Position.X + x * Tile.Width, Position.Y + y * Tile.Height, Tile.Width, Tile.Height), tile.Color);

                }
            }
        }



    }
}
