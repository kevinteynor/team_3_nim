using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            GameConsoleLoop loop = new GameConsoleLoop();

            loop.AddMenuItem(new MenuItem("Option 1 - Player Vs. AI", game.RunPlayerVersusAIGame, 1));
            loop.AddMenuItem(new MenuItem("Option 2 - AI Vs. AI", game.RunAIVersusAIGame, 2));
            loop.AddMenuItem(new MenuItem("Option 3 - Exit", loop.End, 3));

            while (loop.Run()) ;
        }
    }
}
