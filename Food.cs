using System;
using System.Xml.Linq;

namespace SnakeGame
{
    class Food
    {
        public int X { get; set; }
        public int Y { get; set; }
        private ConsoleColor color = ConsoleColor.Red;
        private Random random;
        private Snake snake;
        private int width, height;

        public Food(int width, int height, Snake snake)
        {
            this.width = width;
            this.height = height;
            this.snake = snake;
            random = new Random();
            GenerateFood();
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = color;
            Console.Write("$");
        }

        private void GenerateFood()
        {
            X = random.Next(1, width);
            Y = random.Next(1, height);

            while (snake.IsInBody(new Position(X, Y)))
            {
                X = random.Next(1, width);
                Y = random.Next(1, height);
            }
        }
    }
}
