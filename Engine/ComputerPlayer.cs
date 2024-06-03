namespace MemoryGameEngine
{
    public struct ComputerPlayer
    {
        private int m_numOfPairs;

        public ComputerPlayer(int i_numOfPairs = 0)
        {
            m_numOfPairs = i_numOfPairs;
        }

        public int NumOfPairs
        {
            get { return m_numOfPairs; }
            set { m_numOfPairs = value; }
        }

        public bool turn(Board i_board) //TODO: inplement function
        {
            return false;
        }

    }
}
