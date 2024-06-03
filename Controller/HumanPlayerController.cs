using MemoryGameEngine;
using System.Security.Policy;

namespace Controller
{
    public struct HumanPlayerController
    {
        public HumanPlayer m_hunanPlayer;

        public HumanPlayerController(string i_name)
        {
            m_hunanPlayer = new HumanPlayer(i_name);
        }

        public string PlayerName
        {
            get { return m_hunanPlayer.PlayerName; }
        }

        public int NumOfPairs
        {
            get { return m_hunanPlayer.NumOfPairs; }
            set { m_hunanPlayer.NumOfPairs = value; }
        }

        public bool turn(int[] i_firstSpot, int[] i_secondSpot, Board i_board)
        {
            SpotOnBoard firstSpot = new SpotOnBoard(i_firstSpot[0], i_firstSpot[1]);
            SpotOnBoard secondSpot = new SpotOnBoard(i_secondSpot[0], i_secondSpot[1]);

            return m_hunanPlayer.Turn(firstSpot, secondSpot, i_board);
        }
    }
}
