using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraSnake
{
   public class Meal 
    {
        public int BoardStartX { get; set; }
        public int BoardEndX { get; set; }
        public int BoardStartY { get; set; }
        public int BoardEndY { get; set; }
        public Random Random { get; set; }
        public bool Eaten { get; set; }

        public Meal(int boardStartX, int boardEndX, int boardStartY, int boardEndY)
        {
            Random = new Random();
            BoardStartX = boardStartX + 1 ;
            BoardEndX = boardEndX - 1;
            BoardStartY = boardStartY + 1;
            BoardEndY = boardEndY - 1;
            var x = Random.Next(BoardStartX, BoardEndX);
            var y = Random.Next(BoardStartY, BoardEndY);
            CurrentTarget = new Coordinate(x, y);
            Draw();
        }
        public void RegeneratePosition()
        {
            var x = Random.Next(BoardStartX, BoardEndX);
            var y = Random.Next(BoardStartY, BoardEndY);
            CurrentTarget = new Coordinate(x, y);
            Eaten = false;
            Draw();
        }

        public Coordinate CurrentTarget { get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(CurrentTarget.X, CurrentTarget.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("$");
        }

    }
}
