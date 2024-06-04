using MemoryGameEngine;
using System.Security.Policy;

namespace Controller
{
    public struct HumanPlayerController
    {
        private HumanPlayer m_humanPlayer;

        public HumanPlayerController(string i_name)
        {
            m_humanPlayer = new HumanPlayer(i_name);
        }

        public string PlayerName
        {
            get { return m_humanPlayer.PlayerName; }
        }

        public int NumOfPairs
        {
            get { return m_humanPlayer.NumOfPairs; }
            set { m_humanPlayer.NumOfPairs = value; }
        }

        public bool Turn(SpotOnBoard i_firstSpot, SpotOnBoard i_secondSpot, Board i_board)
        {
            return m_humanPlayer.Turn(i_firstSpot, i_secondSpot, i_board);
        }
    }
}
