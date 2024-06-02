using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameEngine
{
    public struct HumanPlayer
    {
        private readonly string r_PlayerName;
        private int m_numOfPairs;

        public HumanPlayer(string i_playerName)
        {
            r_PlayerName = i_playerName;
            m_numOfPairs = 0;
        }

        public string PlayerName
        {
            get { return r_PlayerName; }
        }

        public int NumOfPairs
        {
            get { return m_numOfPairs; }
            set { m_numOfPairs = value; }
        }


    }
}
