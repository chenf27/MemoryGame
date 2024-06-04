using System;
using MemoryGameEngine;
using Ex02.ConsoleUtils;
using Engine;

namespace UI
{
    internal class GameFlowManager
    {
        private GameManager m_manager = new GameManager();
        private const int k_numOfPlayers = 2;
        public const int k_MinFrameSize = 4;
        public const int k_MaxFrameSize = 6;
        private const int k_FirstHumanPlayer = 0;
        private const int k_SecondHumanPlayer = 1;
        private const int k_FirstComputerPlayer = 0;
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

            m_manager.CreateAndInitializeBoard(numOfRowsFromUser, numOfColsFromUser);
        }

        private int GetValidFrameForBoard(string i_prompt)
        {
            int inputFromUser = 0;
            bool validInputFromUser = false;

            while (!validInputFromUser)
            {
                Console.Write(i_prompt);
                if (int.TryParse(Console.ReadLine(), out inputFromUser) && inputFromUser >= k_MinFrameSize && inputFromUser <= k_MaxFrameSize)
                {
                    validInputFromUser = true;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a number between {k_MinFrameSize} and {k_MaxFrameSize}.");
                }
            }
            return inputFromUser;
        }

        private void SetPlayersInfo()
        {
            string name;
            bool validInputFromUser = false;
            int choice;
            bool isHumanPlayer;

            Console.WriteLine(@"Hello!
Welcome to our Memory Game!!!
Please enter your name:");
            name = Console.ReadLine();
            m_manager.CreatePlayer(name, true); //TODO CHECK

            Console.WriteLine("For Player vs Player press 1, for Player vs Computer press 2: ");
            while (!validInputFromUser)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    validInputFromUser = true;
                    isHumanPlayer = choice == 1;
                    if (choice == 1)
                    {
                        Console.WriteLine("Please enter the name of the player: ");
                        name = Console.ReadLine();
                    }
                    else if (choice != 2)
                    {
                        Console.WriteLine("Invalid Input!! the number should be either 1 or 2");
                        validInputFromUser = false;
                    }

                    if (validInputFromUser)
                    {
                        m_manager.CreatePlayer(name, isHumanPlayer); //TODO SENDS STAM SHEM
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

                clearScreenAndPrintBoard();
                Console.WriteLine(m_manager.Board.NumOfPairsLeftInBoard);     //TODO DELETE LATER!!!!!!!!!!! only for debugging
                if (turn % 2 != 0 && m_manager.HumanPlayers.Length == k_numOfPlayers)
                {
                    Console.WriteLine("Computer's turn: ");
                    foundPair = m_manager.ComputerPlayers[k_FirstComputerPlayer].Turn();
                }
                else
                {
                    if (turn % 2 == 0) //TODO append player's name as format (use variable)
                    {
                        Console.Write(m_manager.HumanPlayers[k_FirstHumanPlayer].PlayerName);
                    }
                    else
                    {
                        Console.Write(m_manager.HumanPlayers[k_SecondHumanPlayer].PlayerName);
                    }

                    Console.WriteLine("'s turn: ");
                    firstSpot = getPlayerChoice();
                    m_manager.Board.FlipSlot(firstSpot.Row, firstSpot.Col);
                    clearScreenAndPrintBoard();
                    secondSpot = getPlayerChoice();
                    m_manager.Board.FlipSlot(secondSpot.Row, secondSpot.Col);
                    clearScreenAndPrintBoard();
                    System.Threading.Thread.Sleep(2000);
                    if (turn % 2 == 0)      //TODO switch to bool?
                    {
                        foundPair = m_manager.HumanPlayers[k_FirstHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                    else
                    {
                        foundPair = m_manager.HumanPlayers[k_SecondHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                }

                if (!foundPair)
                {
                    turn++;
                }
            }
        }

        private void clearScreenAndPrintBoard()
        {
            Screen.Clear();
            printBoard();
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
                selectedRowAsChar = slot[1]; //TODO add input validation in case of mismatching format (e.g. "a")
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

        private void printBoard() //TODO change i and j to row and col
        {
            int numOfRowsInBoard = m_manager.Board.NumOfRowsInBoard;
            int numOfColsInBoard = m_manager.Board.NumOfColsInBoard;
            SpotOnBoard spotToCheck = new SpotOnBoard();

            Console.Write("   ");
            for (int i = 0; i < numOfColsInBoard; i++)
            {
                char letter = (char)('A' + i);
                Console.Write(@"  {0} ", letter);
            }

            Console.WriteLine();
            printPanelPartition(numOfColsInBoard);

            for (int rowIndex = 0; rowIndex < numOfRowsInBoard; rowIndex++)
            {
                Console.Write(@" {0} ", rowIndex + 1);
                Console.Write("|");
                for (int colIndex = 0; colIndex < numOfColsInBoard; colIndex++)
                {
                    spotToCheck.Row = rowIndex;
                    spotToCheck.Col = colIndex;

                    if (m_manager.Board.IsSpotTaken(spotToCheck.Row, spotToCheck.Col))
                    {
                        Console.Write(@" {0} ", m_manager.Board.SlotContent(spotToCheck.Row, spotToCheck.Col));
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.Write("|");
                }
                //Console.Write("|");
                Console.WriteLine();
                printPanelPartition(numOfColsInBoard);
            }
        }

        private void printPanelPartition(int i_numOfCols)
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
