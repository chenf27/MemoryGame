using Ex02.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;

namespace MemoryGameEngine
{
    public class Program
    {
        public static void Main()
        {
            run();
        }

        private static void run()
        {
            bool playAgain;
            int userInput;

            Console.WriteLine(@"Hello!
Welcome to our Memory Game!!!");
            do
            {
                playAgain = false;
                GameFlowManager gameFlowManager = new GameFlowManager();
                gameFlowManager.GameSetUp();
                gameFlowManager.PlayGame();
                gameFlowManager.FinishGame();
                Console.WriteLine(@"Please select from the options below:
1 - Another game
Else - Exit");
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput == 1)
                    {
                        playAgain = true;
                    }
                    else
                    {
                        Screen.Clear();
                    }
                } 
            } while (playAgain);
        }
    }
}
