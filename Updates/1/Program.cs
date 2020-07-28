using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraSnake
{
    class Program
    {
        public static int BoardStartX { get; set; }
        public static int BoardEndX { get; set; }
        public static int BoardStartY { get; set; }
        public static int BoardEndY { get; set; }
        public static Snake Snake { get; set; }
        static void Main(string[] args)
        {
            
            BoardStartX = 5;
            BoardEndX = 20;
            BoardStartY = 5;
            BoardEndY = 20;
            Console.CursorVisible = false;
            bool exit = false;
            double frameRate = 1000 / 5.0;
            DateTime lastDate = DateTime.Now;
            Meal meal = new Meal(BoardStartX, BoardEndX, BoardStartY, BoardEndY);
            Snake snake = new Snake(BoardStartX, BoardEndX, BoardStartY, BoardEndY, meal);
            Snake = snake;

            while (!exit)

            {
                if(Console.KeyAvailable)
                { 
                    ConsoleKeyInfo input = Console.ReadKey();

                    switch (input.Key)
                    {
                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.Left;
                            break;

                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.Right;
                            break;

                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.Up;
                            break;

                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.Down;
                            break;

                    }

                }

                if((DateTime.Now - lastDate).TotalMilliseconds >= frameRate)
                {
                    //game action
                    snake.Move();


                    if (snake.GameOver)
                    {
                        Console.Clear();
                        Console.WriteLine($"GAME OVER. YOUR SCORE: {snake.Score}");
                        exit = true;
                        Console.ReadLine();
                    }

                    lastDate = DateTime.Now;
                }
            }


        }
    }
}
