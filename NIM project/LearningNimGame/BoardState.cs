using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningNimGame
{
    public class BoardState
    {
        public int[] RowCounts { get; set; }
        public int TotalLeft
        {
            get
            {
                int value = 0;
                for (int r = 0; r < RowCounts.Length; r++)
                    value += RowCounts[r];
                return value;
            }
        }
        public int Frequency { get; set; }
        public float StateValue { get; set; }        

        public BoardState()
        {
            RowCounts = new int[3];
            RowCounts[0] = 3;
            RowCounts[1] = 5;
            RowCounts[2] = 7;
            StateValue = 0;
            Frequency = 0;
        }
        public BoardState(BoardState b)
        {
            this.RowCounts = new int[3];
            for (int r = 0; r < RowCounts.Length; r++)
                this.RowCounts[r] = b.RowCounts[r];
            this.StateValue = b.StateValue;
            this.Frequency = b.Frequency;
        }

        public void ApplyNewData(BoardState state)
        {
            float totalValue = (float)Frequency * StateValue;
            totalValue += state.StateValue;
            Frequency++;
            StateValue = totalValue / Frequency;
        }

        public void PrintBoard()
        {
            Console.WriteLine();

            for (int r = 0; r < RowCounts.Length; r++)
            {
                string line = String.Format("Row {0}: ({1}) ", r + 1, RowCounts[r]);
                for (int c = 0; c < RowCounts[r]; c++)
                    line += "*";
                Console.WriteLine(line);
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(BoardState))
                return false;

            return this.Equals(obj as BoardState);

        }
        public bool Equals(BoardState board)
        {
            if (board.RowCounts.Length != this.RowCounts.Length) return false;

            for(int r = 0; r < RowCounts.Length; r++)
                if(board.RowCounts[r] != this.RowCounts[r]) return false;

            return true;
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
