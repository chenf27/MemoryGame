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

        public bool Turn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot, Board i_Board)
        {
            bool foundPair = false;
            char firstSlotContent = i_Board.SlotContent(i_FirstSpot.Row, i_FirstSpot.Col);
            char secondSlotContent = i_Board.SlotContent(i_SecondSpot.Row, i_SecondSpot.Col);
 
            if (firstSlotContent == secondSlotContent)
            {
                m_numOfPairs++;
                i_Board.NumOfPairsLeftInBoard--;
                foundPair = true;
            }
            else
            {
                i_Board.FlipSlot(i_FirstSpot.Row, i_FirstSpot.Col);
                i_Board.FlipSlot(i_SecondSpot.Row, i_SecondSpot.Col);
            }

            return foundPair;
        }
    }
}
