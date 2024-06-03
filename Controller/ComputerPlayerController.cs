using MemoryGameEngine;

namespace Controller
{
    public struct ComputerPlayerController
    {
        private ComputerPlayer m_computerPlayer;

        public ComputerPlayerController(int i_numOfPairs = 0)
        {
            m_computerPlayer = new ComputerPlayer(i_numOfPairs);
        }

        public int NumOfPairs
        {
            get
            {
                return m_computerPlayer.NumOfPairs;
            }
            set
            {
                m_computerPlayer.NumOfPairs = value;
            }
        }

        public bool turn(Board i_board)
        {
            return m_computerPlayer.turn(i_board);
        }
    }
}
