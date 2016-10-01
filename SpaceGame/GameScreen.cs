using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacegame
{
    public class GameScreen : Screen
    {
        SpriteBatch spriteBatch;

        Texture2D pixelTexture;

        Board board;

        Piece currentPiece;

        Input input;

        Point boardPosition = new Point(50, 50);

        List<Piece> pieceQueue;

        Random random;

        TimeSpan nextDropTime, droptime;



        public GameScreen(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });

            board = new Board(Game.Content);
            board.TileTexture = pixelTexture;

            board.Position = boardPosition;

            input = new Input();
            random = new Random();

            InitPieceQueue();
            currentPiece = GetNextPiece();

            droptime = TimeSpan.FromSeconds(1.0/6.0);
            nextDropTime = TimeSpan.Zero;



            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            input.Update();

            if (input.KeyPressed(Keys.Left))
                if (PieceFit(currentPiece.tiles, currentPiece.Position.X - 1, currentPiece.Position.Y))
                    currentPiece.Position = new Point(currentPiece.Position.X - 1, currentPiece.Position.Y);

            if (input.KeyPressed(Keys.Right))
                if (PieceFit(currentPiece.tiles, currentPiece.Position.X + 1, currentPiece.Position.Y))
                    currentPiece.Position = new Point(currentPiece.Position.X + 1, currentPiece.Position.Y);

            if (gameTime.TotalGameTime>nextDropTime||input.KeyDown(Keys.Down))
            {
                nextDropTime = gameTime.TotalGameTime + droptime;

                if (PieceFit(currentPiece.tiles, currentPiece.Position.X, currentPiece.Position.Y + 1))
                {
                    currentPiece.Position = new Point(currentPiece.Position.X, currentPiece.Position.Y + 1);
                }
                else
                {
                    CopyPieceToBoard();
                    CheckForFullRows(currentPiece.Position);
                    currentPiece = GetNextPiece();
                }
            }

            if (input.KeyPressed(Keys.Up))
            {
                Tile[,] rotatedTile = currentPiece.Rotate();
                if (PieceFit(rotatedTile,currentPiece.Position.X,currentPiece.Position.Y))
                {
                    currentPiece.tiles = rotatedTile;
                }
            }


            base.Update(gameTime);
        }

        void CopyPieceToBoard()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Tile tile = currentPiece.tiles[x, y];
                    if (tile != null)
                    {
                        board.Tiles[currentPiece.Position.X + x, currentPiece.Position.Y + y] = tile;

                    }
                }
            }
        }

        void CheckForFullRows(Point positition)
        {
            int maxy = positition.Y + 4;
            if (maxy>Board.Height)
            {
                maxy = Board.Height;
            }

            for (int i = positition.Y;  i < maxy; i++)
            {
                bool fullRow = true;

                for (int x = 0; x < Board.Width; x++)
                {
                    if(board.Tiles[x,i]==null)
                    {
                        fullRow = false;
                        break;
                    }
                }

                if (fullRow)
                {
                    Tile[,] tiles = board.Tiles;
                    for (int y = i; y > 0; y--)
                    {
                        for (int x = 0; x < Board.Width; x++)
                        {
                            tiles[x, y] = tiles[x, y - 1];
                        }
                    }
                    i--;
                }


            }
        }



        bool PieceFit(Tile[,] tiles, int xbase, int ybase)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                    {
                        int basex = xbase + x;
                        int basey = ybase + y;

                        if (basey >= Board.Height)
                            return false;
                        if (basex < 0)
                            return false;
                        if (basex >= Board.Width)
                            return false;

                        if (board.Tiles[basex, basey] != null)
                            return false;

                    }
                }
            }
            return true;
        }



        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            board.Draw(spriteBatch);
            currentPiece.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);



        }



        void InitPieceQueue()
        {
            pieceQueue = new List<Piece>();
            for (int i = 0; i < 4; i++)
            {
                pieceQueue.Add(GetRandomPiece());
            }


        }

        Piece GetRandomPiece()
        {
            Piece piece = new Piece();
            piece.BasePosition = boardPosition;
            piece.Position = new Point(5, 0);
            piece.TileTexture = pixelTexture;

            Color tileColor;

            switch (random.Next(3))
            {
                case 0:
                    tileColor = Color.Green;
                    piece.tiles[1, 1] = new Tile() { Color = tileColor };
                    piece.tiles[1, 2] = new Tile() { Color = tileColor };
                    piece.tiles[2, 1] = new Tile() { Color = tileColor };
                    piece.tiles[2, 2] = new Tile() { Color = tileColor };
                    break;
                case 1:
                    tileColor = Color.Blue;
                    piece.tiles[1, 0] = new Tile() { Color = tileColor };
                    piece.tiles[1, 1] = new Tile() { Color = tileColor };
                    piece.tiles[1, 2] = new Tile() { Color = tileColor };
                    piece.tiles[1, 3] = new Tile() { Color = tileColor };
                    break;
                case 2:
                    tileColor = Color.Red;
                    piece.tiles[1, 2] = new Tile() { Color = tileColor };
                    piece.tiles[2, 2] = new Tile() { Color = tileColor };
                    piece.tiles[3, 2] = new Tile() { Color = tileColor };
                    piece.tiles[2, 2] = new Tile() { Color = tileColor };
                    break;
                default:
                    throw new Exception("This shouldnt happen...");
            }
            return piece;
        }


        Piece GetNextPiece()
        {
            Piece result = pieceQueue[0];

            for (int i = 0; i < 3; i++)
            {
                pieceQueue[i] = pieceQueue[i + 1];
            }
            pieceQueue[3] = GetRandomPiece();

            return result;
        }

        void DrawPieceQueue()
        {

        }

    }

    public abstract class Screen : DrawableGameComponent
    {
        public Screen(Game game) : base(game)
        {

        }
    }
}
