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
        private HumanPlayer m_HumanPlayer;
        private HumanPlayer? m_2ndHumanPlayer = null;
        private ComputerPlayer? m_ComputerPlayer = null;
        private string m_winnerName;
        private bool m_isWinner = false;

        public string WinnerName
        {
            get
            {
                if (m_isWinner)
                {
                    return m_winnerName;
                }
                else
                {
                    throw new Exception("The game has not ended yet!");
                }
            }
            set
            {
                m_winnerName = value;
                m_isWinner = true;
            }
        }

        public bool IsWinner
        {
            get;
        }

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
            if(i_isHuman)
            {
                m_2ndHumanPlayer = new HumanPlayer(i_name);
            }
            else
            {
                m_ComputerPlayer = new ComputerPlayer(0);
            }
        }

        public HumanPlayer Human
        {
            get { return m_HumanPlayer; }
            set { m_HumanPlayer = value; }
        }

        public ComputerPlayer Computer
        {
            get { return (ComputerPlayer)m_ComputerPlayer; }
            set { m_ComputerPlayer = value; }
        }
    }
}
