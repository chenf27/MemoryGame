using System;
using MemoryGameEngine;
using Ex02.ConsoleUtils;

namespace UI
{
    internal class GameFlowManager
    {
        //TODO TADPIS BETSEVA
        private GameManager<char> m_manager = new GameManager<char>(k_numOfPlayers);
        private const int k_numOfPlayers = 2;
        public const int k_MinFrameSize = 4;
        public const int k_MaxFrameSize = 6;
        private const int k_FirstHumanPlayer = 0;
        private const int k_SecondHumanPlayer = 1;
        private const int k_FirstComputerPlayer = 0;
        private const int k_DefaultComputerLevel = 2;
        private const char k_Exit = 'Q';
        private const int k_NumOfLettersInABC = 26;

        public void GameSetUp()
        {
            SetPlayersInfo();
            SetStartingBoard();
        } //DONE

        private void SetStartingBoard() //TODO: MAYBE APPEND FORMAT FOR THE REPEATED MESSAGES?
        {
            int numOfColsFromUser = 0, numOfRowsFromUser = 0;
            bool validInputFromUser = false;
            char[] elementsForBoard = new char[k_NumOfLettersInABC];

            for (int i = 0; i < k_NumOfLettersInABC; i++)
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

        private int GetValidFrameForBoard(string i_prompt) //TODO: APPEND FORMAT?
        {
            int inputFromUser = 0;
            bool validInputFromUser = false;
            //TODO APPEND FORMAT
            while (!validInputFromUser)
            {
                Console.Write(i_prompt);
                if (int.TryParse(Console.ReadLine(), out inputFromUser) && inputFromUser >= k_MinFrameSize && inputFromUser <= k_MaxFrameSize)
                {
                    validInputFromUser = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}.", k_MinFrameSize, k_MaxFrameSize);
                }
            }

            return inputFromUser;
        }

        private void SetPlayersInfo() //DONE
        {
            string playersName;
            int computerPlayerLevel = 0; 
            int userChoice;
            bool validInputFromUser = false;

            Console.WriteLine("Please enter your name:");
            playersName = Console.ReadLine();
            m_manager.CreateHumanPlayer(playersName); 

            Console.WriteLine("For Player vs Player press 1, for Player vs Computer press 2: ");
            while (!validInputFromUser)
            {
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    validInputFromUser = true;
                    if (userChoice == 1)
                    {
                        Console.WriteLine("Please enter the name of the player: ");
                        playersName = Console.ReadLine();
                        m_manager.CreateHumanPlayer(playersName);
                    }
                    else if(userChoice == 2)
                    {
                        Console.WriteLine(@"Please enter the computer's level
Enter 1 for easy, 2 for medium and 3 for hard:");
                        if (int.TryParse(Console.ReadLine(), out computerPlayerLevel) && (computerPlayerLevel >= 1 && computerPlayerLevel <= 3))
                        {
                            m_manager.CreateComputerPlayer(computerPlayerLevel);        
                        }
                        else
                        {
                            Console.WriteLine("Wrong input! Level sets to medium by default");
                            m_manager.CreateComputerPlayer(k_DefaultComputerLevel);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input!! the number should be either 1 or 2");
                        validInputFromUser = false;
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
            bool foundPair;
            SpotOnBoard firstSpot, secondSpot;

            do
            {
                foundPair = false;
                clearScreenAndPrintBoard();
                Console.WriteLine(m_manager.Board.NumOfPairsLeftInBoard);     //TODO DELETE LATER!!!!!!!!!!! only for 
                if(m_manager.CurrentTurn == 2 && m_manager.HumanPlayerCounter < 2)
                {
                    foundPair = m_manager.ComputerPlayers[k_FirstComputerPlayer].Play(out firstSpot, out secondSpot, m_manager.Board);
                }
                else
                {
                    //NOT A MUST TO WRITE THE PLAYER NAME
                    if (m_manager.CurrentTurn == 1) //TODO append player's name as format (use variable)
                    {
                        Console.Write(m_manager.HumanPlayers[k_FirstHumanPlayer].PlayerName);
                    }
                    else
                    {
                        Console.Write(m_manager.HumanPlayers[k_SecondHumanPlayer].PlayerName);
                    }

                    Console.WriteLine("'s turn: ");
                    firstSpot = getPlayerChoice();
                    m_manager.Board.FlipCard(firstSpot.Row, firstSpot.Col);
                    clearScreenAndPrintBoard();
                    secondSpot = getPlayerChoice();
                    m_manager.Board.FlipCard(secondSpot.Row, secondSpot.Col);
                    if (m_manager.CurrentTurn == 1)
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

                if(!foundPair)
                {
                    m_manager.EndUnsuccsessfulTurn(firstSpot, secondSpot);
                    if(m_manager.CurrentTurn == 1 && m_manager.HumanPlayerCounter == 1)
                    {
                        //TODO SHORTER AND WRITE FUNCTION
                        m_manager.ComputerPlayers[k_FirstComputerPlayer].AddBrainCell(firstSpot, m_manager.Board.CardContent(firstSpot.Row, firstSpot.Col));
                        m_manager.ComputerPlayers[k_FirstComputerPlayer].AddBrainCell(secondSpot, m_manager.Board.CardContent(secondSpot.Row, secondSpot.Col));
                    }
                }
                if(foundPair && m_manager.CurrentTurn == 1 && m_manager.HumanPlayerCounter == 1)
                {
                    m_manager.ComputerPlayers[k_FirstComputerPlayer].DeleteBrainCell(m_manager.Board.CardContent(firstSpot.Row, firstSpot.Col));
                }

                m_manager.TurnGenarator(foundPair);
            } while (m_manager.Board.NumOfPairsLeftInBoard > 0);
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

                //TODO A42 CATCH
                if (slot.Length < 2)
                {
                    Console.WriteLine("Input error! Value too short, must enter a character and an integer");
                }
                else
                {
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
            }

            return spotOnBoard;
        }

        public void FinishGame() 
        {
            Console.WriteLine("Score Board:");
            Console.WriteLine("{0}: {1} points ", m_manager.HumanPlayers[k_FirstHumanPlayer].PlayerName, m_manager.HumanPlayers[k_FirstHumanPlayer].NumOfPairs);
            if (m_manager.HumanPlayerCounter == 2)
            {
                Console.WriteLine("{0}: {1} points ", m_manager.HumanPlayers[k_SecondHumanPlayer].PlayerName, m_manager.HumanPlayers[k_SecondHumanPlayer].NumOfPairs);
            }
            else
            {
                Console.WriteLine("Computer: {0} points", m_manager.ComputerPlayers[k_FirstComputerPlayer].NumOfPairs);
            }
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
                        Console.Write(@" {0} ", m_manager.Board.CardContent(spotToCheck.Row, spotToCheck.Col));
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
