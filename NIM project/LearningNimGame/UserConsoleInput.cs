﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public static class UserConsoleInput
    {
        /// <summary>
        /// Forces the user to provide valid input, otherwise throws exception.
        /// </summary>
        /// <param name="prompt">String to be printed to prompt user for input.</param>
        /// <param name="errormsg">Message that displays when invalid input is provided</param>
        /// <param name="minChoice">Minimum value input</param>
        /// <param name="maxChoice">Maximum value input</param>
        /// <returns>Acceptable integer input</returns>
        public static int ForceConsoleIntegerInput(string prompt, string errormsg,
            int minChoice, int maxChoice)
        {
            Console.WriteLine(prompt);

            int val = -1;
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out val))
                    if (val >= minChoice && val <= maxChoice)
                        return val;
                    else Console.WriteLine(errormsg);
                else Console.WriteLine(errormsg);
            } while (val < minChoice || val > maxChoice);

            throw new Exception("Input choice failed, unable to force acceptable choice.");
        }

        /// <summary>
        /// Runs through the logic required to create a new BoardState based upon
        /// the currentState and user input. First queries the user for which row using
        /// ForceConsoleIntegerInput and checks that the row has more than 0 tokens.
        /// Then asks for how many tokens to remove using ForceConsoleIntegerInput.
        /// </summary>
        /// <param name="currentState">Current game's BoardState</param>
        /// <returns>A BoardState created from the currentState and UserInput</returns>
        public static BoardState GetPlayerTurnInput(BoardState currentState)
        {
            BoardState newState = new BoardState(currentState);

            do
            {
                int row = 0;
                bool validRow = false;

                do
                {
                    row = ForceConsoleIntegerInput("Choose a Row(1-3): ", "Invalid Input.", 1, 3);

                    switch (row)
                    {
                        case 1:
                            if (newState.RowACount != 0)
                                validRow = true;
                            break;
                        case 2:
                            if (newState.RowBCount != 0)
                                validRow = true;
                            break;
                        case 3:
                            if (newState.RowCCount != 0)
                                validRow = true;
                            break;
                        default:
                            Console.WriteLine("That row has 0 tokens. ");
                            validRow = false;
                            break;
                    }
                }
                while (!validRow);


                int val = 0;

                switch (row)
                {
                    case 1:
                        val = ForceConsoleIntegerInput("Choose an amount(1-" + currentState.RowACount + "): ",
                            "Invalid Input.", 1, currentState.RowACount);
                        newState.RowACount -= val;
                        break;
                    case 2:
                        val = ForceConsoleIntegerInput("Choose an amount(1-" + currentState.RowBCount + "): ",
                            "Invalid Input.", 1, currentState.RowBCount);
                        newState.RowBCount -= val;
                        break;
                    case 3:
                        val = ForceConsoleIntegerInput("Choose an amount(1-" + currentState.RowCCount + "): ",
                            "Invalid Input.", 1, currentState.RowCCount);
                        newState.RowCCount -= val;
                        break;
                }
            }
            while (!NimLogic.IsMoveValid(currentState, newState));

            return newState;
        }
    }
}
