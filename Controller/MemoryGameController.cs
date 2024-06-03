using MemoryGameEngine;
using System;

namespace Controller
{
    public class MemoryGameController
    {
        private readonly GameManagerController m_gameManagerController;
        private readonly int r_totalNumberOfPlayers;
        private HumanPlayerController[] m_humanPlayers;
        private ComputerPlayerController[] m_computerPlayers;
        private int m_numberOfHumanPlayers = 0;
        private int m_numberOfComputerPlayers = 0;

        public MemoryGameController(int i_numOfPlaters)
        {
            m_gameManagerController = new GameManagerController();
            r_totalNumberOfPlayers = i_numOfPlaters;
            m_humanPlayers = new HumanPlayerController[r_totalNumberOfPlayers];
            m_computerPlayers = new ComputerPlayerController[r_totalNumberOfPlayers];
        }

        public GameManagerController GameManagerController
        {
            get { return m_gameManagerController;  }
        }

        public int ToTalNumberOfPlayers
        {
            get { return r_totalNumberOfPlayers; }
        }

        public HumanPlayerController[] HumanPlayers
        {
            get { return m_humanPlayers; }
        }

        public ComputerPlayerController[] ComputerPlayers
        {
            get { return m_computerPlayers; }
        }

        public void AddHumanPlayer(string i_name)
        {
            if (m_numberOfHumanPlayers < r_totalNumberOfPlayers)
            {
                m_humanPlayers[m_numberOfHumanPlayers++] = new HumanPlayerController(i_name);
            }
            else
            {
                throw new InvalidOperationException("Cannot add more human players. The array is full.");
            }
        }

        public void AddComputerPlayer()
        {
            if (m_numberOfComputerPlayers < r_totalNumberOfPlayers)
            {
                m_computerPlayers[m_numberOfComputerPlayers++] = new ComputerPlayerController();
            }
            else
            {
                throw new InvalidOperationException("Cannot add more computer players. The array is full.");
            }
        }
    }
}
