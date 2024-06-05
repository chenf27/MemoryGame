using Engine;
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

        public bool Turn(SpotOnBoard i_FirstSpot,SpotOnBoard i_SecondSpot, Board<char> io_Board)
        {
            char firstSlotContent;
            char secondSlotContent;
            bool foundPair;

            firstSlotContent = io_Board.SlotContent(i_FirstSpot.Row, i_FirstSpot.Col);
            secondSlotContent = io_Board.SlotContent(i_SecondSpot.Row, i_SecondSpot.Col);
            if (firstSlotContent == secondSlotContent)
            {
                m_numOfPairs++;
                io_Board.NumOfPairsLeftInBoard--;
                foundPair = true;
            }
            else
            {
                foundPair = false;
            }

            return foundPair;
        }
    }
}
