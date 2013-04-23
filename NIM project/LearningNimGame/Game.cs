using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading;

namespace LearningNimGame
{
    class Game
    {
        BoardState GameState;

        bool Playing
        {
            get { return GameState.TotalLeft > 0; }
        }

        NimLearningAI NimAI_1 = new NimLearningAI();
        NimLearningAI NimAI_2 = new NimLearningAI();

        List<BoardState> MoveList = new List<BoardState>();

        public void RunPlayerVersusAIGame()
        {
            GameState = new BoardState();
            MoveList.Clear();

            Random randomGen = new Random();

            //Randomly choose the AI to take first turn.
            if (randomGen.Next(2) % 2 == 0)
            {
                GameState = NimAI_1.TakeTurn(GameState);
                MoveList.Add(GameState);
                Console.WriteLine();
                GameState.PrintBoard();
                Console.WriteLine("After AI Turn. Any Key To Continue");
                Console.ReadLine();
            }

            do
            {
                Console.Clear();
                GameState.PrintBoard();

                GameState = UserConsoleInput.GetPlayerTurnInput(GameState);
                MoveList.Add(GameState);
                Console.WriteLine();
                Console.WriteLine("After Player Turn.");
                GameState.PrintBoard();

                if (GameState.TotalLeft <= 0)
                {
                    GameOver("AI_1");
                    break;
                }


                GameState = NimAI_1.TakeTurn(GameState);
                MoveList.Add(GameState);
                Console.WriteLine();
                Console.WriteLine("After AI Turn");
                GameState.PrintBoard();

                if (GameState.TotalLeft <= 0)
                {
                    GameOver("Player");
                    break;
                }

                Console.WriteLine("Press Any Key To Continue. ");
                Console.ReadLine();
            }
            while (Playing);

            NimLogic.WeightBoardStates(ref MoveList);

            NimAI_1.IntegrateIntoCatalog(MoveList);
        }

        public void RunAIVersusAIGame()
        {
            GameState = new BoardState();
            MoveList.Clear();

            Random randomGen = new Random();

            if (randomGen.Next(2) % 2 == 0)
            {
                GameState = NimAI_2.TakeTurn(GameState);
                MoveList.Add(GameState);
            }

            do
            {
                GameState = NimAI_1.TakeTurn(GameState);
                MoveList.Add(GameState);

                if (GameState.TotalLeft <= 0)
                {
                    GameOver("AI_2");
                    break;
                }

                GameState = NimAI_2.TakeTurn(GameState);
                MoveList.Add(GameState);

                if (GameState.TotalLeft <= 0)
                {
                    GameOver("AI_1");
                    break;
                }
            }
            while (Playing);

            NimLogic.WeightBoardStates(ref MoveList);

            NimAI_1.IntegrateIntoCatalog(MoveList);
            NimAI_2.IntegrateIntoCatalog(MoveList);
        }

        public void GameOver(string winner)
        {
            Console.WriteLine(winner + " Won this game.");
            Console.WriteLine("Any Key to Continue");
        }
    }
}
