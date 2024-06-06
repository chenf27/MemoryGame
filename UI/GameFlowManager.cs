﻿using System;
using MemoryGameEngine;
using Ex02.ConsoleUtils;

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
        private const int k_DefaultComputerLevel = 2;
        private const int k_MaximumNumberOfPairsInBoard = 18;
        private const char k_Exit = 'Q';

        public void GameSetUp() //DONE
        {
            SetPlayersInfo();
            SetStartingBoard();
        } 

        private void SetStartingBoard() //DONE
        {
            int numOfColsFromUser = 0, numOfRowsFromUser = 0;
            bool validInputFromUser = false;
            char[] elementsForBoard = new char[k_MaximumNumberOfPairsInBoard];

            for(int i = 0; i < k_MaximumNumberOfPairsInBoard; i++)
            {
                elementsForBoard[i] = (char)('A' + i);
            }

            Console.WriteLine("Enter the size of the board, the size need to be between 4 and 6 but can't be overall of odd slots (5X5 is illegal)");
            while(!validInputFromUser)
            {
                numOfRowsFromUser = GetValidFrameForBoard("Enter the number of rows (4-6): ");
                numOfColsFromUser = GetValidFrameForBoard("Enter the number of columns (4-6): ");
                if(numOfColsFromUser == 5 && numOfRowsFromUser == 5)
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

        private int GetValidFrameForBoard(string i_MessageToUser) //DONE
        {
            int inputFromUser = 0;
            bool validInputFromUser = false;

            while(!validInputFromUser)
            {
                Console.Write(i_MessageToUser);
                if(int.TryParse(Console.ReadLine(), out inputFromUser) && inputFromUser >= k_MinFrameSize && inputFromUser <= k_MaxFrameSize)
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
            int computerPlayerLevel; 
            bool validInputFromUser = false;

            Console.WriteLine("Please enter your name:");
            playersName = Console.ReadLine();
            m_manager.CreateHumanPlayer(playersName); 

            Console.WriteLine("For Player vs Player press 1, for Player vs Computer press 2: ");
            while(!validInputFromUser)
            {
                if(int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    validInputFromUser = true;
                    if(userChoice == 1)
                    {
                        Console.WriteLine("Please enter the name of the player: ");
                        playersName = Console.ReadLine();
                        m_manager.CreateHumanPlayer(playersName);
                    }
                    else if(userChoice == 2)
                    {
                        Console.WriteLine(@"Please enter the computer's level
Enter 1 for easy, 2 for medium and 3 for hard:");
                        if(int.TryParse(Console.ReadLine(), out computerPlayerLevel) && (computerPlayerLevel >= 1 && computerPlayerLevel <= 3))
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

        public bool PlayGame()
        {
            string playerName;
            bool foundPair;
            bool computerPlayerTurn = false;
            bool userPressedQ = false;
            SpotOnBoard firstSpot, secondSpot;
            SpotOnBoard dummySpot = new SpotOnBoard(k_MaxFrameSize + 1, k_MaxFrameSize + 1);

            do
            {
                computerPlayerTurn = m_manager.CurrentTurn == 2 && m_manager.HumanPlayerCounter < 2;
                foundPair = false;
                clearScreenAndPrintBoard(dummySpot, dummySpot, false);
                Console.WriteLine(m_manager.Board.NumOfPairsLeftInBoard);     //TODO DELETE LATER!!!!!!!!!!! only for writing phase
                if(computerPlayerTurn)
                {
                    foundPair = m_manager.ComputerPlayers[k_FirstComputerPlayer].Play(out firstSpot, out secondSpot, m_manager.Board);
                }
                else
                {
                    if(m_manager.CurrentTurn == 1) 
                    {
                        playerName = m_manager.HumanPlayers[k_FirstHumanPlayer].PlayerName;
                    }
                    else
                    {
                        playerName = m_manager.HumanPlayers[k_SecondHumanPlayer].PlayerName;
                    }

                    Console.WriteLine("{0}'s turn: ", playerName);
                    firstSpot = getPlayerChoice(ref userPressedQ);
                    if(userPressedQ)
                    {
                        break;
                    }

                    m_manager.Board.FlipCard(firstSpot.Row, firstSpot.Col);
                    clearScreenAndPrintBoard(firstSpot, dummySpot, computerPlayerTurn);
                    secondSpot = getPlayerChoice(ref userPressedQ);
                    if (userPressedQ)
                    {
                        break;
                    }

                    m_manager.Board.FlipCard(secondSpot.Row, secondSpot.Col);
                    if(m_manager.CurrentTurn == 1)
                    {
                        foundPair = m_manager.HumanPlayers[k_FirstHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                    else
                    {
                        foundPair = m_manager.HumanPlayers[k_SecondHumanPlayer].Turn(firstSpot, secondSpot, m_manager.Board);
                    }
                }

                clearScreenAndPrintBoard(firstSpot, secondSpot, computerPlayerTurn);
                System.Threading.Thread.Sleep(2000);

                if(!foundPair)
                {
                    m_manager.EndUnsuccsessfulTurn(firstSpot, secondSpot);
                    if(m_manager.CurrentTurn == 1 && m_manager.HumanPlayerCounter == 1)
                    {
                        ComputerPlayer<char> computerPlayer = m_manager.ComputerPlayers[k_FirstComputerPlayer];
                        addBrainCellToComputerPlayer(ref computerPlayer, firstSpot);
                        addBrainCellToComputerPlayer(ref computerPlayer, secondSpot);
                    }
                }
                if(foundPair && m_manager.CurrentTurn == 1 && m_manager.HumanPlayerCounter == 1)
                {
                    m_manager.ComputerPlayers[k_FirstComputerPlayer].DeleteBrainCell(m_manager.Board.CardContent(firstSpot.Row, firstSpot.Col));
                }

                m_manager.TurnGenarator(foundPair);
            } while(m_manager.Board.NumOfPairsLeftInBoard > 0);

            return userPressedQ;
        }

        private void addBrainCellToComputerPlayer(ref ComputerPlayer<char> io_ComputerPlayer, SpotOnBoard i_CardToAdd)
        {
            io_ComputerPlayer.AddBrainCell(i_CardToAdd, m_manager.Board.CardContent(i_CardToAdd.Row, i_CardToAdd.Col));
        }

        private void clearScreenAndPrintBoard(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot, bool i_ComputerTurn)
        {
            Screen.Clear();
            printBoard(i_FirstSpot, i_SecondSpot, i_ComputerTurn);
        }

        private SpotOnBoard getPlayerChoice(ref bool o_UserPressedQ)
        {
            bool validInput = false;
            string slot;
            char selectedColAsChar;
            int selectedRow;
            SpotOnBoard spotOnBoard = new SpotOnBoard();

            Console.WriteLine(@"Please enter the slot you would like to flip (Format: A2)
To exit press Q");
            while(!validInput)
            {
                slot = Console.ReadLine();
                if(slot.Length == 1 && char.ToUpper(slot[0]) == k_Exit)
                {
                    o_UserPressedQ = true;
                    break;
                }

                if(slot.Length < 2)
                {
                    Console.WriteLine("Input error! Value too short, must enter a character and an integer.");
                }
                else
                {
                    selectedColAsChar = slot[0];
                    string rowPart = slot.Substring(1);

                    if(!char.IsLetter(selectedColAsChar) || !int.TryParse(rowPart, out selectedRow))
                    {
                        Console.WriteLine("Format error! Please enter a slot in the correct format.");
                    }
                    else
                    {
                        spotOnBoard.Row = selectedRow - 1;
                        selectedColAsChar = char.ToUpper(selectedColAsChar);
                        spotOnBoard.Col = selectedColAsChar - 'A';

                        if(isInFrame(m_manager.Board.NumOfRowsInBoard, spotOnBoard.Row) &&
                            isInFrame(m_manager.Board.NumOfColsInBoard, spotOnBoard.Col))
                        {
                            if(m_manager.Board.IsSpotTaken(spotOnBoard.Row, spotOnBoard.Col))
                            {
                                Console.WriteLine("Input Error! The slot you selected was already flipped.");
                            }
                            else
                            {
                                validInput = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Range Error! The slot you selected wasn't in range.");
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
            if(m_manager.HumanPlayerCounter == 2)
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

        private void printBoard(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot, bool i_ComputerTurn)
        {
            int numOfRowsInBoard = m_manager.Board.NumOfRowsInBoard;
            int numOfColsInBoard = m_manager.Board.NumOfColsInBoard;
            SpotOnBoard spotToCheck = new SpotOnBoard();

            Console.Write("   ");
            for(int i = 0; i < numOfColsInBoard; i++)
            {
                char letter = (char)('A' + i);
                Console.Write(@"  {0} ", letter);
            }

            Console.WriteLine();
            printPanelPartition(numOfColsInBoard);

            for(int rowIndex = 0; rowIndex < numOfRowsInBoard; rowIndex++)
            {
                Console.Write(@" {0} ", rowIndex + 1);
                Console.Write("|");
                for(int colIndex = 0; colIndex < numOfColsInBoard; colIndex++)
                {
                    spotToCheck.Row = rowIndex;
                    spotToCheck.Col = colIndex;

                    if(i_ComputerTurn)
                    {
                        if(spotToCheck.IsEqual(i_FirstSpot) || spotToCheck.IsEqual(i_SecondSpot))
                        {
                            if (m_manager.Board.CardContent(i_FirstSpot.Row, i_FirstSpot.Col) == m_manager.Board.CardContent(i_SecondSpot.Row, i_SecondSpot.Col))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                    }

                    if(m_manager.Board.IsSpotTaken(spotToCheck.Row, spotToCheck.Col))
                    {
                        Console.Write(@" {0} ", m_manager.Board.CardContent(spotToCheck.Row, spotToCheck.Col));
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
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
            for(int i = 0; i < lengthToPrint; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();

        }
    }
}
