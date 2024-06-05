using Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameEngine
{
    public struct ComputerPlayer
    {
        private int m_numOfPairs;
        private List<BrainCell> m_Brain;
        private eComputerPlayerLevel m_ComputerLevel;
        private const int k_CapacityOfBrainMedium = 14;

        public enum eComputerPlayerLevel
        {
            Easy,
            Medium,
            Hard
        }

        public eComputerPlayerLevel ComputerLevel
        {
            get { return m_ComputerLevel; }
            set { m_ComputerLevel = value; }
        }





        public ComputerPlayer(int i_Difficulty)
        {
            m_numOfPairs = 0;
            m_Brain = new List<BrainCell>();
            switch (i_Difficulty)
            {
                case 1:
                    m_ComputerLevel = eComputerPlayerLevel.Easy;
                    break;
                case 2:
                    m_ComputerLevel = eComputerPlayerLevel.Medium;
                    break;
                case 3:
                    m_ComputerLevel = eComputerPlayerLevel.Hard;
                    break;
                default:
                    m_ComputerLevel = eComputerPlayerLevel.Medium;
                    break;
            }
        }

        public struct BrainCell
        {
            private SpotOnBoard m_spotOnBoard;
            private char m_CardContent;

            public BrainCell (SpotOnBoard i_SpotOnBoard, char i_CardContent)
            {
                m_spotOnBoard = i_SpotOnBoard;
                m_CardContent = i_CardContent;
            }
            public SpotOnBoard SpotOnBoard
            {
                get 
                {
                    return m_spotOnBoard;
                }

                set
                {
                    m_spotOnBoard = value; 
                }
            }

            public char CardContent
            {
                get
                {
                    return m_CardContent;
                }

                set
                {
                    m_CardContent = value;
                }
            }
        }

        public void AddBrainCell(SpotOnBoard i_SpotOnBoard, char i_content)
        {
            bool alreadyInBrain = false;

            foreach(BrainCell cell in m_Brain)
            {
                if (cell.CardContent == i_content && i_SpotOnBoard.Equals(cell.SpotOnBoard))
                {
                    alreadyInBrain = true;
                    break;
                }
            }

            if(!alreadyInBrain)
            {
                BrainCell newCell = new BrainCell(i_SpotOnBoard, i_content);
                m_Brain.Add(newCell);
            }

            if(m_Brain.Count == k_CapacityOfBrainMedium)
            {
                m_Brain.RemoveAt(0);
            }

        }

        public SpotOnBoard FindPair(SpotOnBoard i_SpotOnBoard, Board i_Board)
        {
            SpotOnBoard spotOnBoard = new SpotOnBoard ();
            bool foundPair = false;
            char content = i_Board.SlotContent(i_SpotOnBoard.Row, i_SpotOnBoard.Col);

            foreach(BrainCell cell in m_Brain)
            {
                if(content == cell.CardContent && !cell.SpotOnBoard.Equals(i_SpotOnBoard))
                {
                    spotOnBoard = cell.SpotOnBoard;
                    foundPair = true;
                    m_Brain.Remove(cell);
                    break;
                }
            }

            if(!foundPair)
            {
                spotOnBoard = i_Board.GenerateRandomUnflippedSpotOnBoard();
            }

            return spotOnBoard;
        }
 
        public int NumOfPairs
        {
            get { return m_numOfPairs; }
            set { m_numOfPairs = value; }
        }

        public List<BrainCell> Brain
        {
            get
            {
                return m_Brain;
            }

            set
            {
                m_Brain = value;
            }
        }
        public bool Turn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot, Board io_Board)
        {
            char firstSpotContent;
            char secondSpotContent;
            bool foundPair;

            firstSpotContent = io_Board.SlotContent(i_FirstSpot.Row, i_FirstSpot.Col);
            secondSpotContent = io_Board.SlotContent(i_SecondSpot.Row, i_SecondSpot.Col);
            if (firstSpotContent == secondSpotContent)
            {
                m_numOfPairs++;
                io_Board.NumOfPairsLeftInBoard--;
                foundPair = true;
            }
            else
            {
                foundPair = false;
                AddBrainCell(i_FirstSpot, firstSpotContent);
                AddBrainCell(i_SecondSpot, secondSpotContent);
            }

            return foundPair;
        }


        public bool HasAPairInBrain(ref SpotOnBoard o_FirstSpot, ref SpotOnBoard o_SecondSpot)
        {
            Dictionary<char, int> contentCount = new Dictionary<char, int>();
            BrainCell indexForFirstOccurence = new BrainCell();
            bool foundPair = false;

            for (int i = 0; i < m_Brain.Count; i++)
            {
                BrainCell currentCell = m_Brain[i];
                if (contentCount.ContainsKey(currentCell.CardContent))
                {
                    contentCount[currentCell.CardContent]++;
                    if (contentCount[currentCell.CardContent] == 2)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (m_Brain[j].CardContent == currentCell.CardContent)
                            {
                                indexForFirstOccurence = m_Brain[j];
                                o_FirstSpot = m_Brain[j].SpotOnBoard;
                                break;
                            }
                        }

                        o_SecondSpot = currentCell.SpotOnBoard;

                        
                        m_Brain.RemoveAt(i); 
                        m_Brain.Remove(indexForFirstOccurence); 

                        foundPair = true;
                        break; 
                    }
                }
                else
                {
                    contentCount[currentCell.CardContent] = 1;
                }
            }

            return foundPair;
        }
    }
}
