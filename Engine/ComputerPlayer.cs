﻿namespace MemoryGameEngine
{
    public struct ComputerPlayer
    {
        private int m_numOfPairs;

        public ComputerPlayer(int numOfPairs = 0)
        {
            m_numOfPairs = numOfPairs;
        }

        public int NumOfPairs
        {
            get { return m_numOfPairs; }
            set { m_numOfPairs = value; }
        }

        public bool turn()
        {
            return false;
        }

    }
}
