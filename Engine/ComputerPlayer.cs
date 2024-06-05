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
        private eComputerPlayerLevel m_CoputerLevel;
        
        private enum eComputerPlayerLevel 
        {
            Easy, 
            Medium,
            Hard
        }

        public ComputerPlayer(int i_Difficulty)
        {
            m_numOfPairs = 0;
            m_Brain = new List<BrainCell>();
            switch (i_Difficulty)
            {
                case 1:
                    m_CoputerLevel = eComputerPlayerLevel.Easy;
                    break;
                case 2:
                    m_CoputerLevel = eComputerPlayerLevel.Medium;
                    break;
                case 3:
                    m_CoputerLevel = eComputerPlayerLevel.Hard;
                    break;
                default:
                    m_CoputerLevel = eComputerPlayerLevel.Medium;
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
            BrainCell cell = new BrainCell(i_SpotOnBoard, i_content);
            m_Brain.Add(cell);
        }

        public SpotOnBoard GetComputerChoice(Board<char> i_Board)
        {
            //TODO IMPLENT TO FIND A RANDOM EMPTY SPOT ON BOARD AND ADD IT TO THE BRAIN
            SpotOnBoard spotOnBoard = new SpotOnBoard();

            return spotOnBoard;

        }

        public SpotOnBoard FindPair(char i_content, Board<char> i_Board)
        {
            SpotOnBoard spotOnBoard = new SpotOnBoard ();
            bool foundPair = false;

            foreach(BrainCell cell in m_Brain)
            {
                if(i_content == cell.CardContent)
                {
                    spotOnBoard = cell.SpotOnBoard;
                    foundPair = true;
                    m_Brain.Remove(cell);
                    break;
                }
            }

            if(!foundPair)
            {
                spotOnBoard = GetComputerChoice(i_Board);
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
        public bool Turn()
        {
            return false;
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
