using Engine;
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
        private readonly int r_NumOfPairsAtTheStartOfTheGame;
        private int m_NumOfPairsLeftInBoard;
        public const int k_MaxFrameSize = 6;
        public const int k_MinFrameSize = 4;

        private struct BoardSlot
        {
            private readonly char r_CardInSlot;
            private bool m_CardFlippedByPlayer;

            public BoardSlot(char i_CardInSlot)
            {
                r_CardInSlot = i_CardInSlot;
                m_CardFlippedByPlayer = false;
            }

            public char CardInSlot
            {
                get 
                {
                    return r_CardInSlot;
                }
            }

            public bool CardFlippedByPlayer
            {
                get
                {
                    return m_CardFlippedByPlayer;
                }
                set
                {
                    m_CardFlippedByPlayer = value;
                }
            }
        }

        public Board(int i_numOfRowsInBoard, int i_numOfColsInBoard) 
        {
            r_NumOfRowsInBoard = i_numOfRowsInBoard;
            r_NumOfColsInBoard = i_numOfColsInBoard;
            m_Board = new BoardSlot[i_numOfRowsInBoard, i_numOfColsInBoard];
            m_Board[0,0] = new BoardSlot('A'); //for checkkksssss
            m_Board[0,1] = new BoardSlot('A'); //for checkkksssss
            r_NumOfPairsAtTheStartOfTheGame = (i_numOfRowsInBoard * i_numOfColsInBoard) / 2;
            m_NumOfPairsLeftInBoard = r_NumOfPairsAtTheStartOfTheGame;
        }

        public int NumOfColsInBoard
        {
            get 
            {
                return r_NumOfColsInBoard;
            }
        }

        public int NumOfRowsInBoard
        {
            get
            {
                return r_NumOfRowsInBoard;
            } 
        }

        public int NumOfPairsAtTheStartOfGame
        {
            get 
            {
                return r_NumOfPairsAtTheStartOfTheGame;
            }
        }

        public int NumOfPairsLeftInBoard
        {
            get
            {
                return m_NumOfPairsLeftInBoard;
            }
            set 
            {
                m_NumOfPairsLeftInBoard = value;
            }
        }

        public bool IsSpotTaken(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].CardFlippedByPlayer;
        }

        public char SlotContent(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].CardInSlot;
        }

        public void FlipSlot(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].CardFlippedByPlayer = !m_Board[i_Row, i_Col].CardFlippedByPlayer;
        }
    }
}
