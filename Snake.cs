using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Snake
    {
        private List<Position> body;
        private ConsoleColor color = ConsoleColor.Green;
        private ConsoleKey direction;
        private int width, height;

        public Snake(int width, int height)
        {
            this.width = width;
            this.height = height;
            body = new List<Position>
            {
                new Position(width / 2, height / 2),
                new Position(width / 2 - 1, height / 2),
                new Position(width / 2 - 2, height / 2)
            };
            direction = ConsoleKey.RightArrow;
        }

        public Position Head
        {
            get { return body[0]; }
        }

        public int Length
        {
            get { return body.Count; }
        }

        public void ChangeDirection(ConsoleKey key)
        {
            if ((key == ConsoleKey.LeftArrow && direction != ConsoleKey.RightArrow) ||
                (key == ConsoleKey.RightArrow && direction != ConsoleKey.LeftArrow) ||
                (key == ConsoleKey.UpArrow && direction != ConsoleKey.DownArrow) ||
                (key == ConsoleKey.DownArrow && direction != ConsoleKey.UpArrow))
            {
                direction = key;
            }
        }

        public bool Move()
        {
            Position newHead = GetNextPosition();
            if (IsOutOfBounds(newHead) || IsInBody(newHead))
            {
                return false;
            }

            body.Insert(0, newHead);
            Console.SetCursorPosition(Head.X, Head.Y);
            Console.ForegroundColor = color;
            Console.Write("@");

            Position tail = body[body.Count - 1];
            Console.SetCursorPosition(tail.X, tail.Y);
            Console.Write(" ");
            body.RemoveAt(body.Count - 1);

            return true;
        }

        public void Grow()
        {
            Position newHead = GetNextPosition();
            body.Insert(0, newHead);
        }

        public void Draw()
        {
            foreach (Position position in body)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.ForegroundColor = color;
                Console.Write("@");
            }
        }

        private Position GetNextPosition()
        {
            Position currentHead = Head;
            switch (direction)
            {
                case ConsoleKey.LeftArrow:
                    return new Position(currentHead.X - 1, currentHead.Y);
                case ConsoleKey.RightArrow:
                    return new Position(currentHead.X + 1, currentHead.Y);
                case ConsoleKey.UpArrow:
                    return new Position(currentHead.X, currentHead.Y - 1);
                
                default:
                    return currentHead;
            }
        }

        private bool IsOutOfBounds(Position position)
        {
            return position.X < 1 || position.X >= width + 1 ||
                   position.Y < 1 || position.Y >= height + 1;
        }

        public bool IsInBody(Position position)
        {
            return body.Contains(position);
        }
    }
}
