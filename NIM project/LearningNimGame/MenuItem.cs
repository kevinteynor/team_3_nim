using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public delegate void InputResponse();

    class MenuItem
    {
        public string Prompt { get; private set; }
        private InputResponse Response;
        public int Trigger { get; private set; }

        public MenuItem(string prompt, InputResponse response, int trigger)
        {
            Prompt = prompt;
            Response = response;
            Trigger = trigger;
        }

        public void Execute()
        {
            Response.Invoke();
        }

        public void PrintPrompt()
        {
            Console.WriteLine(Prompt);
        }
    }
}
