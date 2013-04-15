using System;
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
    }
}
