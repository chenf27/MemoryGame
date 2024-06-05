using MemoryGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GameManager<T>
    {
        private Board<T> m_Board;
        private HumanPlayer[] m_HumanPlayers;
        private ComputerPlayer[] m_ComputerPlayers;
        private int m_HumanPlayersCount = 0;
        private int m_ComputerPlayersCount = 0;

        public GameManager(int i_NumberOfPlayers)
        {
            m_HumanPlayers = new HumanPlayer[i_NumberOfPlayers];
            m_ComputerPlayers = new ComputerPlayer[i_NumberOfPlayers];
        }

        public Board<T> Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void CreateBoard(int i_numOfRows, int i_numOfCols)
        {
            m_Board = new Board<T>(i_numOfRows, i_numOfCols);
        }

        public void CreateHumanPlayer(string i_name)
        {
            m_HumanPlayers[m_HumanPlayersCount++] = new HumanPlayer(i_name);
        }

        public void CreateComputerPlayer(int i_Difficulty)
        {
            m_ComputerPlayers[m_ComputerPlayersCount++] = new ComputerPlayer(i_Difficulty);
        }

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

        public void CreateAndInitializeBoard(T[] i_ElementsForBoard, int i_numOfRows, int i_numOfCols)
        {
            m_Board = new Board<T>(i_numOfRows, i_numOfCols);
            m_Board.InitializeBoard(i_ElementsForBoard);
        }

        public void EndUnsuccsessfulTurn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot)
        {
            m_Board.FlipSlot(i_FirstSpot.Row, i_FirstSpot.Col);
            m_Board.FlipSlot(i_SecondSpot.Row, i_SecondSpot.Col);
        }
    }
}
