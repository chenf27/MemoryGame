using System;

namespace MemoryGameEngine
{
    public class Board
    {
        private readonly int r_NumOfColsInBoard;
        private readonly int r_NumOfRowsInBoard;
        private BoardSlot[,] m_Board;
        private readonly int r_NumOfPairsAtTheStartOfTheGame;
        private int m_NumOfPairsLeftInBoard;

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
            r_NumOfPairsAtTheStartOfTheGame = (i_numOfRowsInBoard * i_numOfColsInBoard) / 2;
            m_NumOfPairsLeftInBoard = r_NumOfPairsAtTheStartOfTheGame;
        }

        public void InitializeBoard()
        {
            int totalSlotsInBoard = r_NumOfRowsInBoard * r_NumOfColsInBoard;
            char[] cardsDeck = new char[totalSlotsInBoard];
            char cardSymbol = 'A';

            for (int i = 0; i < r_NumOfPairsAtTheStartOfTheGame; i++)
            {
                cardsDeck[2 * i] = cardSymbol;
                cardsDeck[2 * i + 1] = cardSymbol;
                cardSymbol++;
            }

            ShuffleDeckOfCards(cardsDeck);

            for(int rowIndex = 0, cardsDeckIndex = 0; rowIndex < r_NumOfRowsInBoard; rowIndex++)
            {
                for(int colIndex = 0; colIndex < r_NumOfColsInBoard; colIndex++)
                {
                    m_Board[rowIndex, colIndex] = new BoardSlot(cardsDeck[cardsDeckIndex]);
                    cardsDeckIndex++;
                }
            }
        }

        private void ShuffleDeckOfCards(char[] io_array)
        {
            Random random = new Random();
            int arrayLength = io_array.Length;
            char temporaryCardSymbol;
            int randomIndex;

            while (arrayLength > 1)
            {
                randomIndex = random.Next(arrayLength--);
                temporaryCardSymbol = io_array[arrayLength];
                io_array[arrayLength] = io_array[randomIndex];
                io_array[randomIndex] = temporaryCardSymbol;
            }
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
