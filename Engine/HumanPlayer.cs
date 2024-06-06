namespace MemoryGameEngine
{
    public struct HumanPlayer<T>
    {
        private readonly string r_PlayerName;
        private int m_NumOfPairs;

        public HumanPlayer(string i_playerName)
        {
            r_PlayerName = i_playerName;
            m_NumOfPairs = 0;
        }

        public string PlayerName
        {
            get
            { return r_PlayerName; }
        }

        public int NumOfPairs
        {
            get { return m_NumOfPairs; }
            set { m_NumOfPairs = value; }
        }

        public bool Turn(SpotOnBoard i_FirstSpot,SpotOnBoard i_SecondSpot, Board<T> io_Board)
        {
            T firstSlotContent;
            T secondSlotContent;
            bool foundPair;

            firstSlotContent = io_Board.CardContent(i_FirstSpot.Row, i_FirstSpot.Col);
            secondSlotContent = io_Board.CardContent(i_SecondSpot.Row, i_SecondSpot.Col);
            if (firstSlotContent.Equals(secondSlotContent))
            {
                m_NumOfPairs++;
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
