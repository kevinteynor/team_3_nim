using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public static class NimLogic
    {
        /// <summary>
        /// In Nim a valid move is defined as removing from 1 to all tokens from 1 row,
        /// this methods verifies that the difference between the first parameter
        /// and the second constitutes a valid move.
        /// </summary>
        /// <param name="currentState">The current state of the game board</param>
        /// <param name="proposedState">The state which an AI or Player's move will result in</param>
        /// <returns>If the move the AI or Player is making is valid</returns>
        public static bool IsMoveValid(BoardState currentState, BoardState proposedState)
        {
            if (currentState.RowACount > proposedState.RowACount &&
                proposedState.RowACount >= 0)
            {
                if (currentState.RowBCount == proposedState.RowBCount &&
                    currentState.RowCCount == proposedState.RowCCount) return true;
            }

            if (currentState.RowBCount > proposedState.RowBCount &&
                proposedState.RowBCount >= 0)
            {
                if (currentState.RowACount == proposedState.RowACount &&
                    currentState.RowCCount == proposedState.RowCCount) return true;
            }

            if (currentState.RowCCount > proposedState.RowCCount &&
                proposedState.RowCCount >= 0)
            {
                if (currentState.RowACount == proposedState.RowACount &&
                    currentState.RowBCount == proposedState.RowBCount) return true;
            }

            return false;
        }

    }
}
