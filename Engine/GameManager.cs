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
        private HumanPlayer[] m_HumanPlayer;
        private ComputerPlayer[] m_ComputerPlayer = null;

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

        public void CreatePlayer(string i_name, bool i_isHuman)
        {
            int lenOfArr;

            if (i_isHuman)
            {
                lenOfArr = m_HumanPlayer.Length;
                m_HumanPlayer[lenOfArr] = new HumanPlayer(i_name);
            }
            else
            {
                lenOfArr = m_ComputerPlayer.Length;
                m_ComputerPlayer[lenOfArr] = new ComputerPlayer();
            }
        }

        public HumanPlayer[] HumanPlayer
        {
            get { return m_HumanPlayer; }
            set { m_HumanPlayer = value; }
        }

        public ComputerPlayer[] ComputerPlayer
        {
            get { return m_ComputerPlayer; }
            set { m_ComputerPlayer = value; }
        }
    }
}
