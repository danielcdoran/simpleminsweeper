using System;
using System.Data;

namespace game
{
    public class Cell
    {
        private int _i;  // Backing store
        private int _j;
        const string letters = "abcdefgh";
        const string numbers = "12345678";
        public int i
        {
            get => _i;
            set
            {
                _i = value;
            }
        }

        public int j
        {
            get => _j;
            set
            {
                _j = value;
            }
        }
        public Cell(int i, int j)
        {
                _i = i;
                _j = j;
        }

        public bool isInRange() {
            if (i < 0) { return false; }
            if (i > 7) { return false; }
            if (j < 0) { return false; }
            if (j > 7) { return false; }
            return true;
        }

        public string position(){
            return "Position " + letters[_j] + numbers[_i];
        }
                public override bool Equals(object obj)
        {
            var other = obj as Cell;
            if (other == null)
                return false;
            // private Cell _currentPosition;
            if (_i != other.i) return false;
            if (_j != other.j) return false;
            return true;
        }
    }
}
