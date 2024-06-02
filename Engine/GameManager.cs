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
        //private BoardSlot[,] m_GamePiece;
        private string m_winnerName;
        private bool m_isWinner = false;

        public void CreateBoard(int i_numOfCols, int i_numOfRows)
        {
            int check;
            
        }

        public void CreatePlayer(string i_name, bool i_isHuman)
        {

        }
    }
}
