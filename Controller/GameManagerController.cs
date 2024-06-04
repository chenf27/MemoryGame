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

        public void FlipSlot(SpotOnBoard i_spotToFlip)
        {
            Board.FlipSlot(i_spotToFlip.Row, i_spotToFlip.Col);
        }

        public bool IsSpotTaken(SpotOnBoard i_spotToCheck)
        {
            return Board.IsSpotTaken(i_spotToCheck.Row, i_spotToCheck.Col);
        }

        public char SlotContent(SpotOnBoard i_spotToCheck)
        {
            return Board.SlotContent(i_spotToCheck.Row, i_spotToCheck.Col);
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
