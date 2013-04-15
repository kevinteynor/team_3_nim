using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public class BoardState
    {
        public int RowACount { get; set; }
        public int RowBCount { get; set; }
        public int RowCCount { get; set; }
        public int TotalLeft { get { return RowACount + RowBCount + RowCCount; } }
        public int Frequency { get; set; }
        public float StateValue { get; set; }        

        public BoardState()
        {
            RowACount = 3;
            RowBCount = 5;
            RowCCount = 7;
            StateValue = 0;
            Frequency = 0;
        }
        public BoardState(BoardState b)
        {
            this.RowACount = b.RowACount;
            this.RowBCount = b.RowBCount;
            this.RowCCount = b.RowCCount;
            this.StateValue = b.StateValue;
            this.Frequency = b.Frequency;
        }

        //Moved to NimLogic.cs as IsMoveValid(BoardState bs1, BoardState bs2);
        //public bool checkIfValid(BoardState change)
        //{
        //    if (this.RowACount > change.RowACount && (change.RowBCount == this.RowBCount && change.RowCCount == this.RowCCount))
        //        return true;
        //    if (this.RowBCount > change.RowBCount && (change.RowACount == this.RowACount && change.RowCCount == this.RowCCount))
        //        return true;
        //    if (this.RowCCount > change.RowCCount && (change.RowACount == this.RowACount && change.RowBCount == this.RowBCount))
        //        return true;

        //    return false;
        //}

        public void ApplyNewData(BoardState state)
        {
            float meanVal = (float)Frequency * StateValue;  // multiply frequency by the value in order to incorporate new data
            meanVal += state.StateValue;                    // add new data's value to the mean-value.
            Frequency += 1;                                 // increase the frequency to also represent the newly added data
            StateValue = meanVal / Frequency;               // re-calculate the new average value.
        }

        public void PrintBoard()
        {
            Console.WriteLine();

            Console.Write("Row A: (" + RowACount + ") ");
            for (uint i = 0; i < RowACount; ++i)
                Console.Write('*');
            Console.WriteLine();

            Console.Write("Row B: (" + RowBCount + ") ");
            for (uint i = 0; i < RowBCount; ++i)
                Console.Write('*');
            Console.WriteLine();

            Console.Write("Row C: (" + RowCCount + ") ");
            for (uint i = 0; i < RowCCount; ++i)
                Console.Write('*');
            Console.WriteLine();
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(BoardState))
                return false;

            var bs_obj = obj as BoardState;

            return (bs_obj.RowACount == this.RowACount &&
                bs_obj.RowBCount == this.RowBCount &&
                bs_obj.RowCCount == this.RowCCount);

        }
        public static bool operator ==(BoardState a, BoardState b)
        {
            if ((object)a == null)
            {
                if ((object)b == null)
                    return true;
                else return false;
            }
            else if ((object)b == null)
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(BoardState a, BoardState b)
        {
            return !(a == b);
        }

        //Replaced by NimLogic.EveryPossibleBoardState()
        //public static BoardState[] GetAllPossibleStates()
        //{
        //    BoardState[] states = new BoardState[192];
        //    int i = 0;

        //    for (int rA = 0; rA <= 3; rA++)
        //    {
        //        for (int rB = 0; rB <= 5; rB++)
        //        {
        //            for (int rC = 0; rC <= 7; rC++)
        //            {
        //                BoardState b = new BoardState();
        //                b.Frequency = 0;
        //                b.StateValue = 0;
        //                b.RowACount = rA;
        //                b.RowBCount = rB;
        //                b.RowCCount = rC;

        //                states[i] = new BoardState(b);
        //                i++;
        //            }
        //        }
        //    }

        //    return states;
        //}
    }
}
