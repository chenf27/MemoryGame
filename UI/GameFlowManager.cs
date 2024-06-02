using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MemoryGameEngine;

namespace UI
{
    internal class GameFlowManager
    {
        private GameManager m_manager = new GameManager();

       public void GameSetUp()
        {
            SetPlayersInfo();
            SetStartingBoard();
        }

        private void SetStartingBoard()
        {
            int numOfColsFromUser = 0, numOfRowsFromUser = 0;
            bool validInputFromUser = false;

            Console.WriteLine("Enter the size of the board, the size need to be between 4 and 6 but can't be overall of odd slots (5X5 is illegal)");
            while (!validInputFromUser)
            {
                numOfColsFromUser = GetValidFrameForBoard("Enter the number of columns (4-6): ");
                numOfRowsFromUser = GetValidFrameForBoard("Enter the number of rows (4-6): ");
                if (numOfColsFromUser == 5 && numOfRowsFromUser == 5)
                {
                    Console.WriteLine("Invalid number. Must be an even number of slots.");
                }
                else
                {
                    validInputFromUser = true;
                }
            }


            m_manager.CreateBoard(numOfColsFromUser, numOfRowsFromUser);

        }
        
        private int GetValidFrameForBoard(string i_prompt)
        {
            int inputFromUser = 0;
            bool validInputFromUser = false;
            while (!validInputFromUser)
            {
                Console.Write(i_prompt);
                if (int.TryParse(Console.ReadLine(), out inputFromUser) && inputFromUser >= Board.k_MinFrameSize && inputFromUser <= Board.k_MaxFrameSize)
                {
                    validInputFromUser = true;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a number between {Board.k_MinFrameSize} and {Board.k_MaxFrameSize}.");
                }
            }
            return inputFromUser;
        }

        private void SetPlayersInfo()
        {
            string name;
            bool validInputFromUser = false;
            int choice;

            Console.WriteLine(@"Hello!
Welcome to our Memory Game!!!
Please enter your name:");
            name = Console.ReadLine();

            m_manager.CreatePlayer(name, true);
            Console.WriteLine("For Player vs Player press 1, for Player vs Computer press 2: ");
            while (!validInputFromUser)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    validInputFromUser = true;
                    bool isHumanPlayer = choice == 1;
                    if (choice == 1)
                    {
                        Console.WriteLine("Please enter the name of the player: ");
                        name = name = Console.ReadLine();
                    }
                    else if (choice != 2)
                    {
                        Console.WriteLine("Invalid Input!! the number should be either 1 or 2");
                        validInputFromUser= false;
                    }

                    if (validInputFromUser)
                    {
                        m_manager.CreatePlayer(name, isHumanPlayer);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input!! Please enter a valid integer");
                }
            }
        }

        public void PlayGame()
        {
           int turn = 0;
           bool validInput = false;


           while(m_manager.Board.NumOfPairsLeftInBoard > 0)
           {
                m_manager.Board.PrintBoard();
                if (turn % 2 == 0)
                {
                    Console.WriteLine("Please enter the slot you would like to flip");
                }
                turn++;

           }

        }

    }
}
