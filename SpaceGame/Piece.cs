using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacegame
{
    public class Piece
    {
        public Point BasePosition { get; set; }
        public Point Position { get; set; }

        public Tile[,] tiles;

        public Texture2D TileTexture { get; set; }

        public Piece()
        {
            tiles = new Tile[4, 4];
            tiles[1, 1] = new Tile() { Color = Color.Red };
            tiles[1, 2] = new Tile() { Color = Color.Blue };
        }


        public Tile[,] Rotate()
        {
            Tile[,] tmpTiles = new Tile[4, 4];
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                        tmpTiles[y, 3 - x] = tile;
                }

            return tmpTiles;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                        spriteBatch.Draw(TileTexture, new Rectangle(BasePosition.X + (Position.X + x) * Tile.Width, BasePosition.Y + (Position.Y + y) * Tile.Height, Tile.Width, Tile.Height), tile.Color);
                }
            }
        }
    }
}
