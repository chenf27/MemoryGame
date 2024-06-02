using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameEngine
{
    public class Board
    {
        private readonly int r_NumOfColsInBoard;
        private readonly int r_NumOfRowsInBoard;
        private BoardSlot[,] m_Board;
        private readonly char[] r_Chars; //TODO
        private readonly int r_NumOfPairsAtTheStartOfTheGame;
        private int m_NumOfPairsLeftInBoard;
        public const int k_MaxFrameSize = 6;
        public const int k_MinFrameSize = 4;

        public Board(int numOfColsInBoard, int NumOfRowsInBoard, BoardSlot[,] board, int numOfPairsAtStartOfTheGame) 
        {
            r_NumOfColsInBoard = numOfColsInBoard;
            r_NumOfRowsInBoard = NumOfRowsInBoard;
            m_Board = board;
            r_NumOfPairsAtTheStartOfTheGame = numOfPairsAtStartOfTheGame;
            m_NumOfPairsLeftInBoard = numOfPairsAtStartOfTheGame;
        }
        public int NumOfColsInBoard
        {
            get { return r_NumOfColsInBoard; }

        }

        public int NumOfRowsInBoard
        {
            get { return r_NumOfRowsInBoard; } 
        }

        public int NumOfPairsAtTheStartOfGame
        {
            get { return r_NumOfPairsAtTheStartOfTheGame; }
        }

        public int NumOfPairsLeftInBoard
        {
            get { return m_NumOfPairsLeftInBoard; }

            set { m_NumOfPairsLeftInBoard = value;}
        }

        public static void PrintBoard(BoardSlot[,] i_board)
        {
            int colFrame = i_board.GetLength(1);
            

            for (int i = 0; i < colFrame; i++)
            {
                char letter = (char)('A' +  i);
                Console.Write(" " + letter);
            }

            Console.WriteLine();

            PrintPanelPartition(colFrame);

            

            for (int i = 0 ;i < i_board.GetLength(0); i++)
            {
                Console.Write(i+1);
                Console.Write("|");
                for (int j = 0; j < i_board.GetLength(1); j++)
                {
                    if (i_board[i,j].CardFlippedByPlayer)
                    {
                        Console.Write(i_board[i,j].CardInSlot);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("|");
                }
                Console.Write(" |");
                Console.WriteLine();

                PrintPanelPartition(colFrame);

            }

        }

        public static void PrintPanelPartition(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write("===");
            }
            Console.WriteLine();

        }
        public void ReduceNumOfPairsLeftInBoard()
        {
            this.NumOfPairsLeftInBoard--;
        }
    }
}
