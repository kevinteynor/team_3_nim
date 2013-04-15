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
        BoardState[] BoardData = new BoardState[192];
        List<BoardState> TempList;
        bool playing = true;
        Random random;

        private BoardState currentBoard;

        public Game()
        {
            random = new Random();
            TempList = new List<BoardState>();

            int count = 0;

            foreach (var b in NimLogic.EveryPossibleBoardState())
            {
                BoardData[count] = b;
                count++;
            }
        }

        public void Start()
        {
            do { playing = RunMainMenu(); } while (playing);
        }

        private bool RunMainMenu()
        {
            Console.Clear();

            Console.WriteLine("1 - Player vs. Computer");
            Console.WriteLine("2 - Computer vs. Computer");
            Console.WriteLine("3 - Exit");

            int choice = ForceConsoleChoice("Select an option: ", "not a valid choice. select game mode:", 1, 3);

            if (choice == 1)
            {
                Console.Clear();
                PlayerVsComputer();
                Console.ReadLine();
                return true;
            }
            else if (choice == 2)
            {
                int games = ForceConsoleChoice("Number of games (1-10000):", "not a valid choice. number of games (1-10000):", 1, 10000);

                for (int i = 0; i < games; i++)
                    ComputerVsComputer();

                Console.ReadLine();
                return true;
            }
            else return false;
        }

        //Replaced by ForceConsoleIntegerInput in UserConsoleInput.cs
        private static int ForceConsoleChoice(string prompt, string errormsg, int min, int max)
        {
            Console.WriteLine(prompt);
            int val = -1;

            while (val < min || val > max)
            {
                if (Int32.TryParse(Console.ReadLine(), out val))
                    if (val >= min && val <= max)
                        return val;
                    else Console.WriteLine(errormsg);
                else Console.WriteLine(errormsg);
            }

            throw new Exception("Input choice failed, unable to force acceptable choice.");
        }

        private void PlayerVsComputer()
        {
            currentBoard = new BoardState();

            // randomize who starts.
            if (random.Next(2) % 2 == 0)
                RunComputerTurn();


            while (currentBoard.TotalLeft > 0)
            {
                Console.WriteLine();
                currentBoard.PrintBoard();
                RunPlayerTurn();

                if (currentBoard.TotalLeft <= 0)
                {
                    GameOver("Computer");
                    return;
                }

                Console.Clear();
                currentBoard.PrintBoard();
                RunComputerTurn();

                if (currentBoard.TotalLeft <= 0)
                {
                    GameOver("Player");
                    return;
                }
            }
        }
        private void ComputerVsComputer()
        {
            currentBoard = new BoardState();

            // randomize who starts.
            if (random.Next(2) % 2 == 0)
                RunComputerTurn();


            while (currentBoard.TotalLeft > 0)
            {
                RunComputerTurn();
                if (currentBoard.TotalLeft <= 0)
                {
                    GameOver("Computer 2");
                    return;
                }

                RunComputerTurn();
                if (currentBoard.TotalLeft <= 0)
                {
                    GameOver("Computer 1");
                    return;
                }
            }
        }

        private void RunPlayerTurn()
        {
            char currentRow = (char)0;
            int currentRowCount = 0;
            int amountTaken = 0;

            #region select row
            do
            {
                Console.WriteLine("Select the row: ");

                string input = Console.ReadLine();

                if (input.Length > 0)
                {
                    currentRow = input[0];

                    if ((currentRow.Equals('a') || currentRow.Equals('A')) && currentBoard.RowACount > 0)
                    {
                        currentRowCount = currentBoard.RowACount;
                    }
                    else if ((currentRow.Equals('b') || currentRow.Equals('B')) && currentBoard.RowBCount > 0)
                    {
                        currentRowCount = currentBoard.RowBCount;
                    }
                    else if ((currentRow.Equals('c') || currentRow.Equals('C')) && currentBoard.RowCCount > 0)
                    {
                        currentRowCount = currentBoard.RowCCount;
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid row letter with 1 or more left to take.\n");
                    }
                }
            } while (currentRowCount == 0);

            #endregion select row

            BoardState playerChoice;

            #region select cells
            do
            {
               playerChoice = new BoardState(currentBoard);

               amountTaken = ForceConsoleChoice("\nSelect amount to take from row " + currentRow + ": ", "Please select a valid number to take.", 1, currentRowCount);

               if ((currentRow.Equals('a') || currentRow.Equals('A')))
               {
                   playerChoice.RowACount -= amountTaken;
               }
               else if ((currentRow.Equals('b') || currentRow.Equals('B')))
               {
                   playerChoice.RowBCount -= amountTaken;
               }
               else if ((currentRow.Equals('c') || currentRow.Equals('C')))
               {
                   playerChoice.RowCCount -= amountTaken;
               }
                
                
            }while(!NimLogic.IsMoveValid(currentBoard, playerChoice));
            #endregion select cells

            takeTurn(playerChoice);
            // promt player for which row, and how many tiles.
            // make sure choice is valid.

        }        
        //Replaced by TakeTurn in NimLearningAI.cs
        private void RunComputerTurn()
        {
            takeTurn(chooseBestTurn());
        }

        //Replaced by GetAllValidMoves in NimLogic.cs
        //private List<BoardState> compileListOfValidMoves()
        //{
        //    List<BoardState> ret = new List<BoardState>();

        //    for (int i = 0; i < BoardData.Length; i++)
        //        if (NimLogic.IsMoveValid(currentBoard, BoardData[i]))
        //            ret.Add(BoardData[i]);

        //    return ret;
        //}

        //Replaced by GetCatalogBoardStates in NimLearningAI.cs
        private BoardState chooseBestTurn()
        {
            List<BoardState> possibilities = new List<BoardState>();
            foreach (var b in NimLogic.GetAllValidMoves(currentBoard))
            {
                possibilities.Add(b);
            }

            if (possibilities.Count == 0)
                return NimLogic.ChooseRandomMoveWithinGameConstraints(currentBoard);

            List<BoardState> mostValuable = new List<BoardState>();
            mostValuable.Add(possibilities[0]);

            for (int i = 1; i < possibilities.Count; ++i)
            {
                if (possibilities[i].StateValue > mostValuable[0].StateValue)
                {
                    mostValuable.Clear();
                    mostValuable.Add(possibilities[i]);
                }
                else if (possibilities[i].StateValue == mostValuable[0].StateValue)
                    mostValuable.Add(possibilities[i]);
            }

            return mostValuable[random.Next(0, mostValuable.Count)];
        }

        //Moved to NimLogic as ChooseRandomMoveWithinGameConstraints(BoardState currentBoard)
        //private BoardState chooseRandomMove()
        //{
        //    BoardState newState = new BoardState(currentBoard);
        //    bool removed = false;
        //    do
        //    {
        //        int row = random.Next(0, 3);
        //        if (row == 0)
        //        {
        //            if (newState.RowACount <= 0)
        //                continue;
        //            newState.RowACount -= random.Next(1, newState.RowACount + 1);
        //            removed = true;
        //        }
        //        else if (row == 1)
        //        {
        //            if (newState.RowBCount <= 0)
        //                continue;
        //            newState.RowBCount -= random.Next(1, newState.RowBCount + 1);
        //            removed = true;
        //        }
        //        else if (row == 2)
        //        {
        //            if (newState.RowCCount <= 0)
        //                continue;
        //            newState.RowCCount -= random.Next(1, newState.RowCCount + 1);
        //            removed = true;
        //        }
        //    } while (!removed);
        //    return newState;
        //}

        private void takeTurn(BoardState newBoardState)
        {
            //determine if move taken is valid
            //if not throw exception
            //if so add currentBoardState to TempList
            //set the currentBoardState to newBoardState
            TempList.Add(currentBoard);
            currentBoard = newBoardState;
        }

        public void GameOver(string winner)
        {
            TempList.Add(currentBoard);
            IncorperateData();
            currentBoard = new BoardState();

            Console.WriteLine(winner + " won.");
            Console.WriteLine("Any Key to Continue");
        }

        //Replaced by ApplyGameDataToCatalog in NimLearningAI.cs
        public void IncorperateData()
        {
            int sign = TempList.Count % 2 == 0 ? 1 : -1;

            for (int i = 0; i < TempList.Count; i++)
            {
                float weight = (sign * i) / (float)TempList.Count;

                TempList[i].StateValue = weight;

                var bs = BoardData.First(b => b == TempList[i]);
                bs.ApplyNewData(TempList[i]);

                // flip sign for next turn
                sign *= -1;
            }

            TempList.Clear();
        }
    }
}
