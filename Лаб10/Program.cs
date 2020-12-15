using System;
using System.Collections.Generic;

namespace Лаб10
{
    class Point
    {
        public int X;
        public int Y;
        public ConsoleColor Color;
        public ConsoleColor NormalColor;
        public bool IsBlock;
        public Point(int x1, int y1)
        {
            X = x1;
            Y = y1;
            Color = ConsoleColor.Black;
        }
    }
    class Block
    {
        public List<Point> PointList = new List<Point>();
        public ConsoleColor BlockColor;
        public Block(ConsoleColor color, params Point[] points)
        {
            BlockColor = color;
            foreach (var point in points)
            {
                point.Color = color;
                point.NormalColor = color;
                point.IsBlock = true;
                PointList.Add(point);
            }
        }
        public void NormaliseColors()
        {
            for (int i = 0; i < PointList.Count; i++)
                PointList[i].Color = PointList[i].NormalColor;
        }
    }

    class Program
    {
        public static Point[,] Field = new Point[6,6];
        public static Block[] blocks = new Block[0];
        public static Block MainBlock = new Block(ConsoleColor.White);



        public static void UpdateField(Point[,] Field)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(6, 2);            
            Console.Write('E');
           

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for(int j = 0; j < Field.GetLength(1); j++)
                {
                    Console.ForegroundColor = Field[i, j].Color;
                    if (Field[i, j].IsBlock)
                        Console.Write('█');                      
                    else
                        Console.Write(' ');

                }
                Console.WriteLine();
            }

        }

        public static bool IsWin(Block MainBlock)
        {
            if (MainBlock.PointList[1].X == 2 && MainBlock.PointList[1].Y == 5)
                return true;
            else
                return false;
        }

        public static void Move(Block MovedBlock, Block MainBlock)
        {
            ConsoleKeyInfo move;
            bool MoveHorizontally = true;

            for(int i = 0; i < MovedBlock.PointList.Count-1; i++)
            {
                if (MovedBlock.PointList[i].Y == MovedBlock.PointList[i + 1].Y)
                { MoveHorizontally = false; }
            }

            for (int i = 0; i < MovedBlock.PointList.Count; i++)
                MovedBlock.PointList[i].Color = ConsoleColor.Gray;

            UpdateField(Field);
            
            if (MoveHorizontally)
            {
                while (true)
                {
                    if (IsWin(MainBlock)) return;

                    move = Console.ReadKey(true);
                    switch (move.Key)
                    {
                        case ConsoleKey.Enter:  return; 

                        case ConsoleKey.RightArrow:
                            {
                                if(MovedBlock.PointList[MovedBlock.PointList.Count-1].Y < Field.GetLength(1) - 1)
                                {
                                    if (!Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y + 1].IsBlock)
                                    {
                                        Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y + 1].IsBlock = true;
                                        MovedBlock.PointList.Add(Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y + 1]);
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].NormalColor = MovedBlock.BlockColor;
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].Color = ConsoleColor.Gray;

                                        MovedBlock.PointList[0].IsBlock = false;
                                        MovedBlock.PointList[0].NormalColor = ConsoleColor.Black;
                                        MovedBlock.PointList.RemoveAt(0);
                                    }
                                }
                                break;

                            }

                        case ConsoleKey.LeftArrow:
                            {
                                if (MovedBlock.PointList[0].Y > 0)
                                {
                                    if (!Field[MovedBlock.PointList[0].X, MovedBlock.PointList[0].Y - 1].IsBlock)
                                    {
                                        Field[MovedBlock.PointList[0].X, MovedBlock.PointList[0].Y - 1].IsBlock = true;
                                        MovedBlock.PointList.Insert(0, Field[MovedBlock.PointList[0].X, MovedBlock.PointList[0].Y - 1]);
                                        MovedBlock.PointList[0].NormalColor = MovedBlock.BlockColor;
                                        MovedBlock.PointList[0].Color = ConsoleColor.Gray;

                                        Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y].IsBlock = false;
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].NormalColor = ConsoleColor.Black;
                                        MovedBlock.PointList.RemoveAt(MovedBlock.PointList.Count - 1);
                                    }
                                }
                                break;

                            }

                    }
                    UpdateField(Field);

                }
            }

            if (!MoveHorizontally)
            {
                while (true)
                {
                    if (IsWin(MainBlock)) return;

                    move = Console.ReadKey(true);
                    switch (move.Key)
                    {
                        case ConsoleKey.Enter:  return; 

                        case ConsoleKey.DownArrow:
                            {
                                if (MovedBlock.PointList[MovedBlock.PointList.Count - 1].X < Field.GetLength(0) - 1)
                                {
                                    if (!Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X + 1, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y].IsBlock)
                                    {
                                        Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X + 1, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y].IsBlock = true;
                                        MovedBlock.PointList.Add(Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X + 1, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y]);
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].NormalColor = MovedBlock.BlockColor;
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].Color = ConsoleColor.Gray;

                                        Field[MovedBlock.PointList[0].X, MovedBlock.PointList[0].Y].IsBlock = false;
                                        MovedBlock.PointList[0].NormalColor = ConsoleColor.Black;
                                        MovedBlock.PointList.RemoveAt(0);
                                    }
                                }
                                break;

                            }

                        case ConsoleKey.UpArrow:
                            {
                                if (MovedBlock.PointList[0].X > 0)
                                {
                                    
                                    if (!Field[MovedBlock.PointList[0].X - 1, MovedBlock.PointList[0].Y].IsBlock)
                                    {
                                        Field[MovedBlock.PointList[0].X - 1, MovedBlock.PointList[0].Y].IsBlock = true;
                                        MovedBlock.PointList.Insert(0, Field[MovedBlock.PointList[0].X - 1, MovedBlock.PointList[0].Y]);
                                        MovedBlock.PointList[0].NormalColor = MovedBlock.BlockColor;
                                        MovedBlock.PointList[0].Color = ConsoleColor.Gray;

                                        Field[MovedBlock.PointList[MovedBlock.PointList.Count - 1].X, MovedBlock.PointList[MovedBlock.PointList.Count - 1].Y].IsBlock = false;
                                        MovedBlock.PointList[MovedBlock.PointList.Count - 1].NormalColor = ConsoleColor.Black;
                                        MovedBlock.PointList.RemoveAt(MovedBlock.PointList.Count - 1);

                                    }
                                }
                                break;

                            }

                    }
                    UpdateField(Field);
                }
            }

        }

        public static void SelectLvl(ConsoleKeyInfo lvl)
        {
            
            switch (lvl.Key)
            {
                case ConsoleKey.D1:
                    {
                        var blockLevel1 = new Block[]
                        {
                        new Block(ConsoleColor.DarkYellow, Field[0,1], Field[1,1], Field[2,1]),
                        new Block(ConsoleColor.Blue,Field[3,0],Field[3,1],Field[3,2]),
                        new Block(ConsoleColor.Yellow,Field[3,3],Field[4,3],Field[5,3]),
                        new Block(ConsoleColor.DarkBlue,Field[0,2],Field[1,2]),
                        new Block(ConsoleColor.Cyan,Field[0,4],Field[1,4]),
                        new Block(ConsoleColor.Magenta,Field[2,4],Field[3,4]),
                        new Block(ConsoleColor.Green,Field[1,5],Field[2,5]),
                        new Block(ConsoleColor.DarkRed,Field[5,0],Field[5,1]),
                        new Block(ConsoleColor.DarkGray,Field[5,4],Field[5,5])
                        };
                        Block MainBlocklevel1 = new Block(ConsoleColor.White, Field[2, 2], Field[2, 3]);
                        Array.Resize<Block>(ref blocks, blockLevel1.Length);
                        blocks = blockLevel1;
                        MainBlock = MainBlocklevel1;
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        var blockLevel2 = new Block[]
                        {
                            new Block(ConsoleColor.DarkYellow, Field[0,1], Field[1,1]),
                            new Block(ConsoleColor.Blue,Field[1,2],Field[2,2]),
                            new Block(ConsoleColor.Yellow,Field[0,3],Field[1,3],Field[2,3]),
                            new Block(ConsoleColor.DarkBlue,Field[1,4],Field[2,4]),
                            new Block(ConsoleColor.Cyan,Field[0,5],Field[1,5]),
                            new Block(ConsoleColor.DarkGray,Field[2,5],Field[3,5]),
                            new Block(ConsoleColor.Magenta,Field[3,1],Field[4,1]),
                            new Block(ConsoleColor.DarkRed,Field[4,2],Field[5,2]),
                            new Block(ConsoleColor.Green,Field[3,2],Field[3,3]),
                            new Block(ConsoleColor.Red,Field[5,3],Field[5,4])

                        };
                        Block MainBlocklevel2 = new Block(ConsoleColor.White, Field[2, 0], Field[2, 1]);
                        Array.Resize<Block>(ref blocks, blockLevel2.Length);
                        blocks = blockLevel2;
                        MainBlock = MainBlocklevel2;
                        break;
                    }                
                case ConsoleKey.D3:
                    {
                        var blockLevel3 = new Block[]
                        {
                            new Block(ConsoleColor.DarkYellow, Field[0,0], Field[0,1]),                            
                            new Block(ConsoleColor.Yellow,Field[1,0],Field[2,0],Field[3,0]),
                            new Block(ConsoleColor.DarkBlue,Field[0,2],Field[1,2]),                            
                            new Block(ConsoleColor.Green,Field[0,3],Field[1,3], Field[2,3]),
                            new Block(ConsoleColor.Magenta,Field[3,1],Field[3,2], Field[3,3]),
                            new Block(ConsoleColor.DarkRed,Field[5,3],Field[5,4], Field[5,5]),                            

                        };
                        Block MainBlocklevel3 = new Block(ConsoleColor.White, Field[2, 1], Field[2, 2]);
                        Array.Resize<Block>(ref blocks, blockLevel3.Length);
                        blocks = blockLevel3;
                        MainBlock = MainBlocklevel3;
                        break;
                    }
                case ConsoleKey.Q:
                {
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
        menu:
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i, j] = new Point(i, j);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Select level: 1  2  3");
            var level = Console.ReadKey();

            SelectLvl(level);
            Console.Clear();
            
            ConsoleKeyInfo move;
            int CursorPositionX = 0;
            int CursorPositionY = 0;


            while (true)
            {
                if (IsWin(MainBlock))
                {
                    UpdateField(Field);
                    Console.SetCursorPosition(3, 8);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("VICTORY !!!");
                    break;
                }

                UpdateField(Field);
                for (int i = 0; i < blocks.Length; i++)
                    blocks[i].NormaliseColors();

                MainBlock.NormaliseColors();

                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                move = Console.ReadKey(true);
                switch (move.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            for (int i = 0; i < blocks.Length; i++)
                            {                            
                                for (int j = 0; j < blocks[i].PointList.Count; j++)
                                    if (blocks[i].PointList[j].X == CursorPositionY && blocks[i].PointList[j].Y == CursorPositionX)
                                        Move(blocks[i], MainBlock);
                            }

                            for (int i = 0; i < MainBlock.PointList.Count; i++)
                            {
                                if (MainBlock.PointList[i].X == CursorPositionY && MainBlock.PointList[i].Y == CursorPositionX)
                                    Move(MainBlock, MainBlock);
                            }
                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (CursorPositionY > 0)
                            {
                                CursorPositionY--;
                                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                                UpdateField(Field);

                            }
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if(CursorPositionY < Field.GetLength(1)-1)
                            {                                
                                CursorPositionY++;
                                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                                UpdateField(Field);

                            }
                            break;
                        }

                    case ConsoleKey.LeftArrow:
                        {
                            if (CursorPositionX > 0)
                            {
                                CursorPositionX--;
                                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                                UpdateField(Field);

                            }
                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            if (CursorPositionX < Field.GetLength(0) - 1)
                            {
                                CursorPositionX++;
                                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                                UpdateField(Field);

                            }
                            break;
                        }
                    case ConsoleKey.Q:
                        {
                            Console.Clear();
                            goto menu;
                            break;
                        }
                }
            }
        }   
    }
}
