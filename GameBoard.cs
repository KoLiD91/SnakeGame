using System;
using System.Threading;
using System.Xml.Linq;

namespace SnakeGame
{
    class GameBoard
    {
        private int width = 30;
        private int height = 20;
        private Snake snake;
        private Food food;

        public GameBoard()
        {
            snake = new Snake(width, height);
            food = new Food(width, height, snake);
            DrawBoard();
        }

        public void Run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    snake.ChangeDirection(keyInfo.Key);
                }

                if (snake.Move())
                {
                    DrawBoard();
                    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                    {
                        snake.Grow();
                        food = new Food(width, height, snake);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine($"Your score: {snake.Length}");
                    break;
                }

                Thread.Sleep(100);
            }
        }

        private void DrawBoard()
        {
            Console.Clear();
            DrawBorder();
            snake.Draw();
            food.Draw();
        }

        private void DrawBorder()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < width + 2; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, height + 1);
                Console.Write("#");
            }

            for (int i = 0; i < height + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(width + 1, i);
                Console.Write("#");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
