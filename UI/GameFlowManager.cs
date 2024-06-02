using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MemoryGameEngine;
using Ex02.ConsoleUtils;

namespace UI
{
    internal class GameFlowManager
    {
        private GameManager m_manager = new GameManager();
        private const char k_Exit = 'Q';

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
                numOfRowsFromUser = GetValidFrameForBoard("Enter the number of rows (4-6): ");
                numOfColsFromUser = GetValidFrameForBoard("Enter the number of columns (4-6): ");
                if (numOfColsFromUser == 5 && numOfRowsFromUser == 5)
                {
                    Console.WriteLine("Invalid number. Must be an even number of slots.");
                }
                else
                {
                    validInputFromUser = true;
                }
            }

            m_manager.CreateBoard(numOfRowsFromUser, numOfColsFromUser);

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
            string slot;
            char col;
            int row;
            int[] firstSlot = new int[2];
           

           while(m_manager.Board.NumOfPairsLeftInBoard > 0)
           {
                Screen.Clear();
                PrintBoard();
                validInput = false;
                if (turn % 2 != 0 && !object.Equals(m_manager.Computer, null))
                {
                    m_manager.Computer.turn();
                }

                if (turn % 2 == 0)
                {
                    Console.WriteLine("Please enter the slot you would like to flip (Format: A2)");
                    while (!validInput)
                    {
                        slot = Console.ReadLine();
                        col = slot[0];
                        col = char.ToUpper(col);
                        firstSlot[0] = col - 'A' + 1;

                        if (int.TryParse(slot[1].ToString(), out row))
                        {
                            //firstSlot[0] = col - 'A' + 1;
                            firstSlot[1] = row;
                            if (isInFrame(row, m_manager.Board.NumOfRowsInBoard) && isValidCol(m_manager.Board.NumOfColsInBoard, col))
                            {
                                if (m_manager.Human.Turn(col - 'A' + 1, row))
                                {
                                    firstSlot[0] = col - 'A' + 1;
                                    firstSlot[1] = row;

                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, The slot you selected was already flipped");
                                }
                            }
                        }
          
                        else
                        {
                            Console.WriteLine("Invalid input, Please enter a col and row in the correct format");
                        }
                    }
                }
                turn++;
                //System.Threading.Thread.Sleep(2000);
           }

        }

        private int[] getPlayerChoice()
        {
            int[] playersSlot = new int[2];
            bool validInput = false;
            string slot;
            char col;
           

            Console.WriteLine("Please enter the slot you would like to flip (Format: A2)");
            while (!validInput)
            {
                slot = Console.ReadLine();
                col = slot[0];
                col = char.ToUpper(col);
                playersSlot[0] = col - 'A' + 1;
                if (!int.TryParse(slot + 1, out playersSlot[1]) || char.IsLetter(col))
                {
                    Console.WriteLine("Format error! Please enter a slot in the selected format.");
                }

                else if(isInFrame(m_manager.Board.NumOfRowsInBoard, playersSlot[1]) && isInFrame(m_manager.Board.NumOfColsInBoard, playersSlot[0])) 
                {
                    if (!m_manager.Human.Turn(playersSlot[0] , playersSlot[1]))
                    {
                        Console.WriteLine("Invalid input, The slot you selected was already flipped");
                    }
                    else
                    {
                        validInput = true;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid input, The slot you selected wasn't in range");
                }
            }



            return playersSlot;
        }

        private bool isValidCol(int i_Range, char i_Char)
        {
            char upperLimiterChar = (char)('A' + i_Range - 1);

            return i_Char >= 'A' && i_Char <= upperLimiterChar;
        }
        
        private bool isInFrame(int i_Range, int i_UserInput)
        {
            return i_UserInput >= 1 && i_UserInput <= i_Range;
        }

        public void PrintBoard()
        {
            int numOfRowsInBoard = m_manager.Board.NumOfRowsInBoard;
            int numOfColsInBoard = m_manager.Board.NumOfColsInBoard;

            Console.Write("   ");
            for (int i = 0; i < numOfColsInBoard; i++)
            {
                char letter = (char)('A' + i);
                Console.Write(@"  {0} ", letter);
            }

            Console.WriteLine();
            PrintPanelPartition(numOfColsInBoard);

            for (int i = 0; i < numOfRowsInBoard; i++)
            {
                Console.Write(@" {0} ", i + 1);
                Console.Write("|");
                for (int j = 0; j < numOfColsInBoard; j++)
                {
                    if (m_manager.Board.IsSpotTaken(i, j))
                    {
                        Console.Write(@" {0} ", m_manager.Board.SlotContent(i, j));
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.Write("|");
                }
                //Console.Write("|");
                Console.WriteLine();
                PrintPanelPartition(numOfColsInBoard);
            }
        }

        public static void PrintPanelPartition(int i_numOfCols)
        {
            int lengthToPrint = i_numOfCols * 4 + 1;
            Console.Write("   ");
            for (int i = 0; i < lengthToPrint; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();

        }
    }
}
