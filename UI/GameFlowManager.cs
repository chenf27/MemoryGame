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

            //m_manager.CreatePlayer(name, true); //CREATE PLAYER?
            m_manager.FirstHumanPlayer = new HumanPlayer(name);
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
            bool foundPair;
            SpotOnBoard firstSpot, secondSpot;
            
            while (m_manager.Board.NumOfPairsLeftInBoard > 0)
            {
                foundPair = false;
                Screen.Clear();
                PrintBoard();
                Console.WriteLine(m_manager.Board.NumOfPairsLeftInBoard); //DELETA LATER
                if (turn % 2 != 0 && m_manager.Computer != null)
                {
                    Console.WriteLine("Computer's turn: ");
                    foundPair = m_manager.Computer.Value.turn();
                }
                else
                {
                    if (turn % 2 == 0)
                    {
                        Console.Write(m_manager.FirstHumanPlayer.PlayerName);
                    }
                    else
                    {
                        Console.Write(m_manager.SecondHumanPlayer.Value.PlayerName);
                    }

                    Console.WriteLine("'s turn: ");
                    firstSpot = getPlayerChoice();
                    m_manager.Board.FlipSlot(firstSpot.Row, firstSpot.Col);
                    Screen.Clear();
                    PrintBoard();
                    secondSpot = getPlayerChoice();
                    m_manager.Board.FlipSlot(secondSpot.Row, secondSpot.Col);
                    Screen.Clear();
                    PrintBoard();
                    //System.Threading.Thread.Sleep(2000);
                    if (turn % 2 == 0)
                    {
                        foundPair = m_manager.FirstHumanPlayer.Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                    else
                    {
                        foundPair = m_manager.SecondHumanPlayer.Value.Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                }

                if (!foundPair)
                {
                    turn++;
                }
            }
        }

        private SpotOnBoard getPlayerChoice()
        {
            bool validInput = false;
            string slot;
            char selectedColAsChar, selectedRowAsChar;
            SpotOnBoard spotOnBoard = new SpotOnBoard();
            
            Console.WriteLine("Please enter the slot you would like to flip (Format: A2)");
            while (!validInput)
            {
                slot = Console.ReadLine();
                if (slot == 'Q'.ToString())
                {
                    Environment.Exit(0);
                }

                selectedColAsChar = slot[0];
                selectedRowAsChar = slot[1]; //NEED TO CHECK
                if (!char.IsDigit(slot[1]) || !char.IsLetter(selectedColAsChar))
                {
                    Console.WriteLine("Format error! Please enter a slot in the selected format.");
                }
                else
                {
                    spotOnBoard.Row = selectedRowAsChar - '0' - 1;
                    selectedColAsChar = char.ToUpper(selectedColAsChar);
                    spotOnBoard.Col = selectedColAsChar - 'A';
                    if (isInFrame(m_manager.Board.NumOfRowsInBoard, spotOnBoard.Row) && isInFrame(m_manager.Board.NumOfColsInBoard, spotOnBoard.Col))
                    {
                        if (m_manager.Board.IsSpotTaken(spotOnBoard.Row, spotOnBoard.Col))
                        {
                            Console.WriteLine("Input Error!, The slot you selected was already flipped");
                        }
                        else
                        {
                            validInput = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Range Error!, The slot you selected wasn't in range");
                    }
                }   
            }

            return spotOnBoard;
        }

        public bool FinishGame()
        {
            return false;


        }
        private bool isInFrame(int i_Range, int i_UserInput)
        {
            return i_UserInput >= 0 && i_UserInput < i_Range;
        }

        public void PrintBoard() //PRIVATE MAYBE?
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
