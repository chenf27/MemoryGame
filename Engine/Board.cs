using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameEngine
{
    public class Board<T>
    {
        private readonly int r_NumOfColsInBoard;
        private readonly int r_NumOfRowsInBoard;
        private BoardSlot[,] m_Board;
        private readonly int r_NumOfPairsAtTheStartOfTheGame;
        private int m_NumOfPairsLeftInBoard;

        private struct BoardSlot
        {
            private readonly T r_CardInSlot;
            private bool m_CardFlippedByPlayer;

            public BoardSlot(T i_CardInSlot)
            {
                r_CardInSlot = i_CardInSlot;
                m_CardFlippedByPlayer = false;
            }

            public T CardInSlot
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

        public void InitializeBoard(T[] i_ElementsForBoard)
        {
            int totalSlotsInBoard = r_NumOfRowsInBoard * r_NumOfColsInBoard;
            T[] cardsDeck = new T[totalSlotsInBoard];

            for (int i = 0; i < r_NumOfPairsAtTheStartOfTheGame; i++)
            {
                cardsDeck[2 * i] = i_ElementsForBoard[i];
                cardsDeck[2 * i + 1] = i_ElementsForBoard[i];
            }

            ShuffleDeckOfCards(cardsDeck);

            for (int rowIndex = 0, cardsDeckIndex = 0; rowIndex < r_NumOfRowsInBoard; rowIndex++)
            {
                for (int colIndex = 0; colIndex < r_NumOfColsInBoard; colIndex++)
                {
                    m_Board[rowIndex, colIndex] = new BoardSlot(cardsDeck[cardsDeckIndex]);
                    cardsDeckIndex++;
                }
            }
        }

        private void ShuffleDeckOfCards(T[] io_array)
        {
            Random random = new Random();
            int arrayLength = io_array.Length;
            T temporaryCardSymbol;
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

        public T SlotContent(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].CardInSlot;
        }

        public void FlipSlot(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].CardFlippedByPlayer = !m_Board[i_Row, i_Col].CardFlippedByPlayer;
        }

        public SpotOnBoard GenerateRandomUnflippedSpotOnBoard()
        {
            List<SpotOnBoard> unflippedSpotsOnBoard = new List<SpotOnBoard>();
            Random randomIndexGenerator = new Random();

            for(int rowIndex = 0; rowIndex < NumOfRowsInBoard; rowIndex++)
            {
                for(int colIndex = 0; colIndex < NumOfColsInBoard; colIndex++)
                {
                    if (!(m_Board[rowIndex, colIndex].CardFlippedByPlayer))
                    {
                        unflippedSpotsOnBoard.Add(new SpotOnBoard(rowIndex, colIndex));
                    }
                }
            }

            int randomIndex = randomIndexGenerator.Next(0, unflippedSpotsOnBoard.Count);

            return unflippedSpotsOnBoard.ElementAt(randomIndex);
        }
    }
}
