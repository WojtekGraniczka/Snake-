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
        static void DrawBoard()
        {
            var upperLeft = (BoardStartX - 1, BoardStartY);
            var upperRight = (BoardEndX + 1, BoardStartY);
            var lowerLeft = (BoardStartX - 1, BoardEndY + 1);
            for (int i = 0; i < BoardEndX - BoardStartX + 2; i++)
            {
                Console.SetCursorPosition(upperLeft.Item1 + i, upperLeft.Item2);
                Console.Write("-");
                Console.SetCursorPosition(lowerLeft.Item1 + i, lowerLeft.Item2);
                Console.Write("-");
            }

            for (int i = 0; i < BoardEndY - BoardStartY + 2; i++)
            {
                Console.SetCursorPosition(upperLeft.Item1, upperLeft.Item2 + i);
                Console.Write("|");
                Console.SetCursorPosition(upperRight.Item1, upperRight.Item2 + i);
                Console.Write("|");
            }

            Console.SetCursorPosition(BoardEndX + 10, BoardStartY + 2);
            Console.WriteLine($"SCORE: {Snake.Score}");
            Console.SetCursorPosition(BoardEndX + 10, BoardStartY + 3);

        }
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
                    DrawBoard();
                    if (meal.CurrentTarget.X == snake.HeadPosition.X
                        && meal.CurrentTarget.X == snake.HeadPosition.Y)

                    {
                        snake.EatMeal();
                        meal.RegeneratePosition();
                        while (snake.Tail.Any(b => b.X == meal.CurrentTarget.X && b.Y == meal.CurrentTarget.Y))
                        {
                            meal.RegeneratePosition();
                        }

                    }
                    else
                    {
                        if (meal.Eaten)
                        {
                            meal.RegeneratePosition();
                            while (snake.Tail.Any(b => b.X == meal.CurrentTarget.X && b.Y == meal.CurrentTarget.Y))
                            {
                                meal.RegeneratePosition();
                            }
                        }
                    }


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
