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
    }
}
