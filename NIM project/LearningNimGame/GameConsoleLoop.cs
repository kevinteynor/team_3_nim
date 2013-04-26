using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    class GameConsoleLoop
    {
        private List<MenuItem> Menu;
        private bool running;

        public GameConsoleLoop()
        {
            Menu = new List<MenuItem>();
            running = true;
        }

        public void AddMenuItem(MenuItem itemToAdd)
        {
            Menu.Add(itemToAdd);
        }

        public bool Run()
        {
            Console.Clear();

            foreach (var m in Menu)
            {
                m.PrintPrompt();
            }

            int menuInput = UserConsoleInput.ForceConsoleIntegerInput("Select an Option: ", "Invalid Input. ", 1, 3);
            
            foreach (var m in Menu)
            {
                if (menuInput == m.Trigger)
                {
                    m.Execute();
                }
            }

            return running;
        }

        public void End()
        {
            running = false;
        }
    }
}
