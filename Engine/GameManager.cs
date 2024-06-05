using MemoryGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GameManager
    {
        private Board m_Board;
        private HumanPlayer[] m_HumanPlayers;
        private ComputerPlayer[] m_ComputerPlayers;
        private int m_HumanPlayersCount = 0;
        private int m_ComputerPlayersCount = 0;

        public GameManager(int i_NumberOfPlayers)
        {
            m_HumanPlayers = new HumanPlayer[i_NumberOfPlayers];
            m_ComputerPlayers = new ComputerPlayer[i_NumberOfPlayers];
        }

        //public void FindWinner()
        //{
        //    bool foundWinner = true;

        //    if (m_ComputerPlayer == null)
        //    {
        //        if (m_HumanPlayer.NumOfPairs > m_2ndHumanPlayer.Value.NumOfPairs)
        //        {
        //            m_winnerName = m_HumanPlayer.PlayerName;
        //        }
        //        else if (m_HumanPlayer.NumOfPairs < m_2ndHumanPlayer.Value.NumOfPairs)
        //        {
        //            m_winnerName = m_2ndHumanPlayer.Value.PlayerName;
        //        }
        //        else
        //        {
        //            foundWinner = false;
        //        }
        //    }
        //    else
        //    {
        //        if (m_HumanPlayer.NumOfPairs > m_ComputerPlayer.Value.NumOfPairs)
        //        {
        //            m_winnerName = m_HumanPlayer.PlayerName;
        //        }
        //        else if (m_HumanPlayer.NumOfPairs < m_ComputerPlayer.Value.NumOfPairs)
        //        {
        //            m_winnerName = "Computer";
        //        }
        //        else
        //        {
        //            foundWinner = false;
        //        }
        //    }

        //    if (!foundWinner)
        //    {
        //        m_winnerName = "Tie";
        //    }
        //}



        //public string WinnerName
        //{
        //    get
        //    {
        //        if (m_isWinner)
        //        {
        //            return m_winnerName;
        //        }
        //        else
        //        {
        //            throw new Exception("The game has not ended yet!");
        //        }
        //    }
        //    set
        //    {
        //        m_winnerName = value;
        //        m_isWinner = true;
        //    }
        //}

        //public bool IsWinner
        //{
        //    get;
        //}



        public Board Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void CreateBoard(int i_numOfRows, int i_numOfCols)
        {
            m_Board = new Board(i_numOfRows, i_numOfCols);
        }

        public void CreateHumanPlayer(string i_name)
        {
            m_HumanPlayers[m_HumanPlayersCount++] = new HumanPlayer(i_name);
        }

        public void CreateComputerPlayer(int i_Difficulty)
        {
            m_ComputerPlayers[m_ComputerPlayersCount++] = new ComputerPlayer(i_Difficulty);
        }


        /*public void CreatePlayer(string i_name, bool i_isHuman) //TODO FIX, NEED THE SIZE OF THE ARRAY UPFRONT AND STAM SHEM
        {
            int lenOfArr;

            if (i_isHuman)
            {
                if (m_HumanPlayer == null)
                {
                    lenOfArr = 0;
                    m_HumanPlayer = new HumanPlayer[2];
                    m_HumanPlayer[0] = new HumanPlayer(i_name);
                }
                else
                {
                    lenOfArr = m_HumanPlayer.GetLength(0);
                }

                m_HumanPlayer[1] = new HumanPlayer(i_name);
            }
            else
            {
                if (m_ComputerPlayer == null)
                {
                    lenOfArr = 0;
                    m_ComputerPlayer = new ComputerPlayer[lenOfArr];
                }
                else
                {
                    lenOfArr = m_ComputerPlayer.GetLength(0);
                }   
                
                m_ComputerPlayer[lenOfArr] = new ComputerPlayer();
            }
        }*/

        public HumanPlayer[] HumanPlayers
        {
            get { return m_HumanPlayers; }
            set { m_HumanPlayers = value; }
        }

        public ComputerPlayer[] ComputerPlayers
        {
            get { return m_ComputerPlayers; }
            set { m_ComputerPlayers = value; }
        }

        public void CreateAndInitializeBoard(int i_numOfRows, int i_numOfCols)
        {
            m_Board = new Board(i_numOfRows, i_numOfCols);
            m_Board.InitializeBoard();
        }

        public void EndUnsuccsessfulTurn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot)
        {
            m_Board.FlipSlot(i_FirstSpot.Row, i_FirstSpot.Col);
            m_Board.FlipSlot(i_SecondSpot.Row, i_SecondSpot.Col);
        }
    }
}
