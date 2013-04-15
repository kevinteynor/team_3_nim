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
            Refresh();
        }

        /// <summary>
        /// Resents the AI's BoardStateCatalog and GamesPlayed, reseting it to a
        /// completely "dumb" state.
        /// </summary>
        public void Refresh()
        {
            BoardStateCatalog = new List<BoardState>();

            foreach (var b in NimLogic.EveryPossibleBoardState())
                BoardStateCatalog.Add(b);

            GamesPlayed = 0;
        }

        /// <summary>
        /// Returns a list of moves from BoardStateCatalog which match
        /// one of the validMoves passed in as a parameter.
        /// </summary>
        /// <param name="validMoves">A list of all valid moves from the current BoardState</param>
        /// <returns>An equivelant list from BoardStateCatalog</returns>
        private List<BoardState> GetCatalogBoardStates(List<BoardState> validMoves)
        {
            List<BoardState> valuableMoves = new List<BoardState>();

            for (int i = 0; i < BoardStateCatalog.Count; ++i)
            {
                for (int j = 0; j < validMoves.Count; ++j)
                {
                    if (BoardStateCatalog[i].Equals(validMoves[j]))
                    {
                        valuableMoves.Add(new BoardState(BoardStateCatalog[i]));
                    }
                }
            }
            return valuableMoves;
        }

        /// <summary>
        /// This method takes in all BoardStates that occured in a game, in reverse order,
        /// and applies a weight to each, and then applies that new data to the corresponding state
        /// in BoardStateCatalog.
        /// </summary>
        /// <param name="boardStates">A list of states from a game, in reverse order</param>
        public void ApplyGameDataToCatalog(List<BoardState> boardStates)
        {
            int weightSign = boardStates.Count % 2 == 0 ? 1 : -1;

            for (int i = 0; i < boardStates.Count; i++)
            {
                float weight = (weightSign * i) / (float)boardStates.Count;
                boardStates[i].StateValue = weight;
                BoardStateCatalog.First(b => b == boardStates[i]).ApplyNewData(boardStates[i]);
                weightSign *= -1;
            }

            GamesPlayed++;
            Console.WriteLine("Games Played: " + GamesPlayed);
        }

        /// <summary>
        /// Runs the logic for an AI to make a move. If the AI has not played enough games
        /// it will simply pick a random move using NimLogic.ChooseRandomMoveWithingameConstraints.
        /// Otherwise it will create a list of BoardStates from BoardStateCatalog that 
        /// are valid moves, and choose the best one.
        /// </summary>
        /// <param name="currentState">Current Game's BoardState</param>
        /// <returns>The AI's chosen move, guaranteed to be a valid move.</returns>
        public BoardState TakeTurn(BoardState currentState)
        {
            if (GamesPlayed <= 50)
                return NimLogic.ChooseRandomMoveWithinGameConstraints(currentState);

            IEnumerable<BoardState> validMoves = NimLogic.GetAllValidMoves(currentState);

            List<BoardState> mostValuableMoves = GetCatalogBoardStates(validMoves.ToList<BoardState>());

            BoardState moveToMake = new BoardState(mostValuableMoves[0]);

            for (int i = 1; i < mostValuableMoves.Count; ++i)
            {
                if (mostValuableMoves[i].StateValue > moveToMake.StateValue)
                    moveToMake = new BoardState(mostValuableMoves[i]);
            }

            return moveToMake;
        }
    }
}
