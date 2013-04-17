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

        /// <summary>
        /// Used to provide a list of all possible BoardStates.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BoardState> EveryPossibleBoardState()
        {
            for (int rowA = 0; rowA <= 3; rowA++)
            {
                for (int rowB = 0; rowB <= 5; rowB++)
                {
                    for (int rowC = 0; rowC <= 7; rowC++)
                    {
                        BoardState b = new BoardState();
                        b.Frequency = 0;
                        b.StateValue = 0;
                        b.RowACount = rowA;
                        b.RowBCount = rowB;
                        b.RowCCount = rowC;

                        yield return b;
                    }
                }
            }
        }

        /// <summary>
        /// Used to determine all possible moves based upon the current BoardState
        /// </summary>
        /// <param name="currentState">Current Game's BoardState</param>
        /// <param name="weightedStateList">List to look for possible moves from</param>
        /// <returns>Each valid move from the provided list</returns>
        public static IEnumerable<BoardState> GetAllValidMoves(BoardState currentState, List<BoardState> weightedStateList)
        {
            foreach (var bs in weightedStateList)
            {
                if (IsMoveValid(currentState, bs))
                    yield return bs;
            }
        }

        public static void WeightBoardStates(ref List<BoardState> gameMovesList)
        {
            int weightSign = gameMovesList.Count % 2 == 0 ? 1 : -1;

            for (int i = 0; i < gameMovesList.Count; i++)
            {
                float weight = (weightSign * i) / (float)gameMovesList.Count;
                gameMovesList[i].StateValue = weight;
                weightSign *= -1;
            }
        }
    }
}
