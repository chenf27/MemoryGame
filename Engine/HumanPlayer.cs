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

        public void Turn(int[,] i_playersSelectedSlots, Board io_Board)
        {
            char firstSlotContent;
            char secondSlotContent;

            firstSlotContent = io_Board.SlotContent(i_playersSelectedSlots[0,0], i_playersSelectedSlots[0,1]);
            secondSlotContent = io_Board.SlotContent(i_playersSelectedSlots[1,0], i_playersSelectedSlots[1,1]);
            if (firstSlotContent == secondSlotContent)
            {
                m_numOfPairs++;
                io_Board.NumOfPairsLeftInBoard--;
            }

            else
            {
                io_Board.FlipSlot(i_playersSelectedSlots[0,0], i_playersSelectedSlots[0,1]);
                io_Board.FlipSlot(i_playersSelectedSlots[1,0], i_playersSelectedSlots[1,1]);
            }
        }

    }
}
