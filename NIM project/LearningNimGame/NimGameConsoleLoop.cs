using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    class NimGameConsoleLoop
    {
        public bool Run(Game game)
        {
            Console.Clear();

            Console.WriteLine("Option 1 - Player versus AI");
            Console.WriteLine("Option 2 - AI versus AI");
            Console.WriteLine("Option 3 - Shutdown");

            int menuChoiceIndex = UserConsoleInput.ForceConsoleIntegerInput("Select an Option: ", "Invalid Input. ", 1, 3);

            if (menuChoiceIndex == 1)
            {
                game.RunPlayerVersusAIGame();
                Console.ReadLine();
                return true;
            }
            else if (menuChoiceIndex == 2)
            {
                int numberOfGames = UserConsoleInput.ForceConsoleIntegerInput("How many games? ", "Invalid Input. ", 1, 1000000);

                for (int i = 0; i < numberOfGames; i++)
                    game.RunAIVersusAIGame();

                Console.ReadLine();
                return true;
            }
            else return false;
        }
    }
}
