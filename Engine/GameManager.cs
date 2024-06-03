namespace MemoryGameEngine
{
    public class GameManager
    {
        private Board m_Board;

        public Board Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void CreateAndInitializeBoard(int i_numOfRows, int i_numOfCols)
        {
            m_Board = new Board(i_numOfRows, i_numOfCols);
            m_Board.InitializeBoard();
        }
    }
}
