using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public class NimLearningAI
    {
        private int GamesPlayed = 0;
        List<BoardState> BoardStateCatalog;

        public NimLearningAI()
        {
            Reset();
        }

        /// <summary>
        /// Resents the AI's BoardStateCatalog and GamesPlayed, reseting it to a
        /// completely "dumb" state.
        /// </summary>
        public void Reset()
        {
            BoardStateCatalog = new List<BoardState>();

            foreach (var b in NimLogic.EveryPossibleBoardState())
                BoardStateCatalog.Add(b);

            GamesPlayed = 0;
        }

        /// <summary>
        /// Takes in all BoardStates that occured in a game, in reverse order,
        /// and applies a weight to each, and then applies that new data to the corresponding state
        /// in BoardStateCatalog.
        /// </summary>
        /// <param name="boardsToIntegrate">A list of states from a game, in reverse order</param>
        public void IntegrateIntoCatalog(List<BoardState> boardsToIntegrate)
        {
            for (int i = 0; i < boardsToIntegrate.Count; i++)
            {
                BoardStateCatalog.First(b => b == boardsToIntegrate[i]).ApplyNewData(boardsToIntegrate[i]);
            }

            GamesPlayed++;
            Console.WriteLine("Games Played: " + GamesPlayed);
        }

        /// <summary>
        /// Runs the logic for an AI to make a move. Iterates through all valid moves and returns the highest weighted possible move.
        /// </summary>
        /// <param name="currentState">Current Game's BoardState</param>
        /// <returns>The AI's chosen move, guaranteed to be a valid move.</returns>
        public BoardState TakeTurn(BoardState currentState)
        {
            BoardState moveToMake = null;
            Random random = new Random();
            foreach(var b in NimLogic.GetAllValidMoves(currentState, BoardStateCatalog))
            {
                if (moveToMake == null)
                    moveToMake = new BoardState(b);
                else if (b.StateValue > moveToMake.StateValue)
                    moveToMake = new BoardState(b);
                else if (b.StateValue == moveToMake.StateValue)
                    if(random.Next(2) == 0) moveToMake = new BoardState(b);
            }

            if (moveToMake == null)
                throw new Exception("No valid moves-- board state has to be <0, 0, 0>, at which point this method should not have been called.");

            return moveToMake;
        }
    }
}
