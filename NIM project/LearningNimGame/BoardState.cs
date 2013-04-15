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

            Console.Write("Row 1: (" + RowACount + ") ");
            for (uint i = 0; i < RowACount; ++i)
                Console.Write('*');
            Console.WriteLine();

            Console.Write("Row 2: (" + RowBCount + ") ");
            for (uint i = 0; i < RowBCount; ++i)
                Console.Write('*');
            Console.WriteLine();

            Console.Write("Row 3: (" + RowCCount + ") ");
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
    }
}
