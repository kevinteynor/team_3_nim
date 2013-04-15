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
    }
}
