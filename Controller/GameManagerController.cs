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
    }
}
