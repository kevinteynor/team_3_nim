﻿using System;
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
            NimGameConsoleLoop loop = new NimGameConsoleLoop();

            while (loop.Run(game)) ;
        }
    }
}
