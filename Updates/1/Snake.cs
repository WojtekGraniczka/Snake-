using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraSnake
{
    public class Snake : ISnake
    {
        public int Lenght { get; set; } = 5;
        public Direction Direction { get; set; } = Direction.Right;
        public Coordinate HeadPosition { get; set; } = new Coordinate();
        public List<Coordinate> Tail { get; set; } = new List<Coordinate>();
        public Meal Meal { get; set; }

        public int Score { get; set; }
        private bool outOfRange = false;

        public int BoardStartX { get; set; }
        public int BoardEndX { get; set; }
        public int BoardStartY { get; set; }
        public int BoardEndY { get; set; }

        public Snake(int boardStartX, int boardEndX, int boardStartY, int boardEndY, Meal meal)
        {
            Meal = meal;
            BoardStartX = boardStartX;
            BoardEndX = boardEndX;
            BoardStartY = boardStartY;
            BoardEndY = boardEndY;
            HeadPosition.X = (boardEndX - boardStartX) / 2;
            HeadPosition.Y = (boardEndY - boardStartY) / 2;
            Score = 0;
        }

        public bool GameOver
        {
            get {
                var a = (Tail.Where(c => c.X == HeadPosition.X
                    && c.Y == HeadPosition.Y).ToList().Count > 1) || outOfRange;
                return a;

            }
        }
        public void Move()
        {
            if (HeadPosition.X < BoardStartX || HeadPosition.X > BoardEndX || HeadPosition.Y <= BoardStartY || HeadPosition.Y > BoardEndY)
            {
                outOfRange = true;
                return;
            }
            switch (Direction)

            {
                case Direction.Left:
                    HeadPosition.X--;
                    break;

                case Direction.Right:
                    HeadPosition.X++;
                    break;

                case Direction.Up:
                    HeadPosition.Y--;
                    break;

                case Direction.Down:
                    HeadPosition.Y++;
                    break;

            }
            try
            {
                Console.SetCursorPosition(HeadPosition.X, HeadPosition.Y);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                Tail.Add(new Coordinate(HeadPosition.X, HeadPosition.Y));
                if (Tail.Count > this.Lenght)
                {
                    var endTail = Tail.First();
                    Console.SetCursorPosition(endTail.X, endTail.Y);
                    Console.Write(" ");
                    Tail.Remove(endTail);
                }

            }
            catch (ArgumentOutOfRangeException e)
            {
                outOfRange = true;
            }

        }

        
    }
    public enum Direction {Left, Right, Up, Down, }
}
