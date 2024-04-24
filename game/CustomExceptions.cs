using System;
using System.ComponentModel;

namespace game
{
    public class InvalidBoardFillFactor : Exception
    {
        public InvalidBoardFillFactor() { }

        public InvalidBoardFillFactor(double fillFactor)
            : base(String.Format("Invalid Fill factor either less than 0.01 or greater than 1.0 {0}", fillFactor))
        {

        }
    }
    public class InvalidCellReference : Exception
    {

        public InvalidCellReference(string comment)
            : base(String.Format(comment))
        {

        }
    }
    public class InvalidMineCount : Exception
    {
        public InvalidMineCount(int mineCount)
            : base(String.Format("Invalid mine count - must be > 0 and < 64. It is {0}", mineCount))
        {
        }
    }

        public class InvalidColumn : Exception
    {
        public InvalidColumn(char column)
            : base(String.Format("Invalid column - {}", column))
        {
        }
    }

}
