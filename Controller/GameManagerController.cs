using MemoryGameEngine;

namespace Controller
{
    public class GameManagerController
    {
        private GameManager m_gameManager = new GameManager();

        public void CreateAndInitializeBoard(int i_numOfRows, int i_numOfCols)
        {
            m_gameManager.CreateAndInitializeBoard(i_numOfRows, i_numOfCols);
        }

        public Board Board
        {
            get { return m_gameManager.Board; }
        }

        public int NumberOfPairsLeftInBoard
        {
            get 
            { 
                return Board.NumOfPairsLeftInBoard; 
            }
        }

        public void FlipSlot(int[] i_spotToFlip)
        {
            Board.FlipSlot(i_spotToFlip[0], i_spotToFlip[1]);
        }

        public bool IsSpotTaken(int[] i_spotToCheck)
        {
            return Board.IsSpotTaken(i_spotToCheck[0], i_spotToCheck[1]);
        }

        public char SlotContent(int[] i_spotToCheck)
        {
            return Board.SlotContent(i_spotToCheck[0], i_spotToCheck[1]);
        }

        public int NumberOfRowsInBoard
        {
            get
            {
                return Board.NumOfRowsInBoard;
            }
        }
        
        public int NumberOfColsInBoard
        {
            get
            {
                return Board.NumOfColsInBoard;
            }
        }
    }
}
