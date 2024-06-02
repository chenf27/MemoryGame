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
            bool playAgain = false;
            
            do
            {
                GameFlowManager gameFlowManager = new GameFlowManager();
                gameFlowManager.GameSetUp();
                gameFlowManager.PlayGame();



                //TODO implement restart of game and change playAgain if necessary 
            } while (playAgain);


        }

    }
}
