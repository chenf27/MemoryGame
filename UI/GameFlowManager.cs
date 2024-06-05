using System;
using MemoryGameEngine;
using Ex02.ConsoleUtils;
using Engine;

namespace UI
{
    internal class GameFlowManager
    {
        private GameManager<char> m_manager = new GameManager<char>(k_numOfPlayers);
        private const int k_numOfPlayers = 2;
        public const int k_MinFrameSize = 4;
        public const int k_MaxFrameSize = 6;
        private const int k_FirstHumanPlayer = 0;
        private const int k_SecondHumanPlayer = 1;
        private const int k_FirstComputerPlayer = 0;
        private const char k_Exit = 'Q';

        public void GameSetUp()
        {
            Screen.Clear();
            SetPlayersInfo();
            SetStartingBoard();
        }

        private void SetStartingBoard()
        {
            int numOfColsFromUser = 0, numOfRowsFromUser = 0;
            bool validInputFromUser = false;
            char[] elementsForBoard = new char[26];

            for (int i = 0; i < 26; i++)
            {
                elementsForBoard[i] = (char)('A' + i);
            }

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

            m_manager.CreateAndInitializeBoard(elementsForBoard, numOfRowsFromUser, numOfColsFromUser);
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
            int computerPlayerLevel = 2; //TODO: delete the 2, we need to get the value from the user
            int userChoice;
            bool validInputFromUser = false;
            bool isHumanPlayer = false;

            Console.WriteLine(@"Hello!
Welcome to our Memory Game!!!
Please enter your name:");
            name = Console.ReadLine();
            m_manager.CreateHumanPlayer(name); 

            Console.WriteLine("For Player vs Player press 1, for Player vs Computer press 2: ");
            while (!validInputFromUser)
            {
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    validInputFromUser = true;

                    if (userChoice == 1)
                    {
                        Console.WriteLine("Please enter the name of the player: ");
                        name = Console.ReadLine();
                        isHumanPlayer = true;
                    }
                    else if(userChoice == 2)
                    {
                        Console.WriteLine(@"Please enter the computer's level
Enter 1 for easy, 2 for medium and 3 for hard:");
                        int.TryParse(Console.ReadLine(), out computerPlayerLevel);
                        //take input from user and parse it to int. no need to verify range, if it's not between 1 and 3 then its medium by default
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input!! the number should be either 1 or 2");
                        validInputFromUser = false;
                    }

                    if (validInputFromUser)
                    {
                        if(isHumanPlayer)
                        {
                            m_manager.CreateHumanPlayer(name); //TODO SENDS STAM SHEM
                        }
                        else
                        {

                            m_manager.CreateComputerPlayer(computerPlayerLevel);
                        }
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
            SpotOnBoard firstSpot = new SpotOnBoard(), secondSpot = new SpotOnBoard();

            while (m_manager.Board.NumOfPairsLeftInBoard > 0)
            {
                foundPair = false;

                clearScreenAndPrintBoard();
                Console.WriteLine(m_manager.Board.NumOfPairsLeftInBoard);     //TODO DELETE LATER!!!!!!!!!!! only for debugging
                if (turn % 2 != 0)
                {
                    //Console.WriteLine("Computer's turn: ");
                    if (m_manager.ComputerPlayers[k_FirstComputerPlayer].ComputerLevel == ComputerPlayer<char>.eComputerPlayerLevel.Easy)
                    {
                        firstSpot = m_manager.Board.GenerateRandomUnflippedSpotOnBoard();
                        m_manager.Board.FlipSlot(firstSpot.Row, firstSpot.Col);
                        secondSpot = m_manager.Board.GenerateRandomUnflippedSpotOnBoard();
                    }
                    else 
                    {
                       // foundPair = m_manager.ComputerPlayers[k_FirstComputerPlayer].HasAPairInBrain(ref firstSpot,ref secondSpot);
                        if (!foundPair)
                        {
                            firstSpot = m_manager.Board.GenerateRandomUnflippedSpotOnBoard();
                            m_manager.Board.FlipSlot(firstSpot.Row, firstSpot.Col);
                            secondSpot = m_manager.ComputerPlayers[k_FirstComputerPlayer].FindPair(firstSpot, m_manager.Board);
                        }
                    }
                    
                    m_manager.Board.FlipSlot(secondSpot.Row, secondSpot.Col);

                    foundPair = m_manager.ComputerPlayers[k_FirstComputerPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
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
                    if (turn % 2 == 0)      
                    {
                        foundPair = m_manager.HumanPlayers[k_FirstHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                    else
                    {
                        foundPair = m_manager.HumanPlayers[k_SecondHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                }


                clearScreenAndPrintBoard();
                System.Threading.Thread.Sleep(2000);

                if (!foundPair)
                {
                    turn++;
                    m_manager.EndUnsuccsessfulTurn(firstSpot, secondSpot);
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

        public void FinishGame()
        {
            Console.WriteLine("Score Board:");
            Console.WriteLine(m_manager.HumanPlayers[k_FirstHumanPlayer].PlayerName + ": " + m_manager.HumanPlayers[k_FirstHumanPlayer].NumOfPairs + " pairs");
            Console.WriteLine(m_manager.HumanPlayers[k_SecondHumanPlayer].PlayerName + ": " + m_manager.HumanPlayers[k_SecondHumanPlayer].NumOfPairs + " pairs");

        }
        private bool isInFrame(int i_Range, int i_UserInput)
        {
            return i_UserInput >= 0 && i_UserInput < i_Range;
        }

        private void printBoard()
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
