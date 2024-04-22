using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace game
{
    public class Mines
    {
        const int boardSize = 8;
        bool[,] _board;
        private int _numberMinesHit;
        private int _mineCount;
        private int _minesInBoard;
        private double _mineFactor;



        public Mines(double mineFactor, int mineCount)
        {

            if (mineFactor < 0.01) { throw new InvalidBoardFillFactor(mineFactor); } // if fillFactor is 0 mines will never be created
            if (mineFactor >= 1.0) { throw new InvalidBoardFillFactor(mineFactor); }

            if (mineCount < 1) { throw new InvalidMineCount(mineCount); }
            if (mineCount > 64) { throw new InvalidMineCount(mineCount); } // can't be bigger than number of squares on board

            initialise();
            _mineCount = mineCount;
            _mineFactor = mineFactor;
            makeBoardWithMines();


        }
        private void initialise()
        {
            _board = new bool[boardSize, boardSize]; // each cell initialised as false
            _numberMinesHit = 0;
            _minesInBoard = 0;

        }
        // if less than mineCount mines in the board then run random again to vcreate more.
        // And remember  - do NOT delete existing mines
        void makeBoardWithMines()
        {
            bool enoughMines;
            do
            {
                _minesInBoard = 0;
                var rand = new Random();
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        var result = rand.NextDouble();
                        if (isMineCell(new Cell(i, j)))
                        {
                            if (result < _mineFactor)
                            {
                                setCellAsMine(new Cell(i, j));
                                _minesInBoard++;
                            }
                        }
                    }
                }
                enoughMines = (_minesInBoard >= _mineCount);
            }

            while (!enoughMines);
        }
        public Mines(Cell[] cellsSetAsMine, int mineCount)
        {
            initialise();
            _mineCount = mineCount;
            foreach (Cell cell in cellsSetAsMine)
            {
                setCellAsMine(cell);
            }
        }

        public bool isMineCell(Cell cell)
        {
            var i = cell.i;
            var j = cell.j;
            if (!cell.isInRange()) return false;
            return _board[cell.i, cell.j];
        }


        public bool getMineData(int i, int j)
        {
            if (i < 0) { return false; }
            if (i > 7) { return false; }
            if (j < 0) { return false; }
            if (j > 7) { return false; }
            return _board[i, j];
        }
        private void setCellAsMine(Cell cell)
        {

            _board[cell.i, cell.j] = true;
            _numberMinesHit++;
        }

        public string toString()
        {

            StringBuilder val = new StringBuilder();
            val.Append(string.Format("Mine count ={} ", _minesInBoard));
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    appendCellReferenceAsString(val, i, j);
                }
            }

            return val.Remove(val.Length - 1, 1).ToString();
        }

        private void appendCellReferenceAsString(StringBuilder val, int i, int j)
        {
            val.Append("(" + i + "," + j + ")=" + _board[i, j] + ",");
        }

        // public string toStringMines()
        // {
        //     StringBuilder val = new StringBuilder();
        //     if (_numberMinesHit == 0)
        //     {
        //         val.Append("Mine count = " + _numberMinesHit + " No cells filled");
        //         return val.ToString();
        //     }
        //     val.Append("Mine count " + _numberMinesHit + "in cells ");
        //     for (int i = 0; i < boardSize; i++)
        //     {
        //         for (int j = 0; j < boardSize; j++)
        //         {
        //             if (_board[i, j]) { appendCellReferenceAsString(val, i, j); ; }
        //         }
        //     }
        //     return val.Remove(val.Length - 1, 1).ToString();
        // }

        public int NumberMines
        {
            get { return _numberMinesHit; }
        }
        public int minesInBoard
        {
            get => _minesInBoard;
        }
        public bool isInRange(int row, int column)
        {
            if (row < 0) { return false; }
            if (row > 7) { return false; }
            if (column < 0) { return false; }
            if (column > 7) { return false; }
            return true;
        }
        // if out of range Cell reurns (0,0) pooint as default
        public Cell getCell(int i, int j)
        {
            if (isInRange(i, j)) { return new Cell(i, j); }
            return new Cell(0, 0);
        }

        private Player notValidMove(Player player)
        {
            return new Player(this, player.getCurrentPosition(), player.Moves, player.LivesRemaining, false);
        }

        public Cell Up(Cell position)
        {
            return getCell(position.i + 1, position.j);
        }
        public Player Up2(Player playerBeforeMove)
        {
            var cell = playerBeforeMove.getCurrentPosition();
            var moves = playerBeforeMove.Moves;
            var lives = playerBeforeMove.LivesRemaining;
            var movedCell = new Cell(cell.i, cell.j+1);

            if (isInRange(cell.i , cell.j+1))
            {
                if (isMineCell(movedCell))
                {
                    lives--;
                }
                    moves++;
            }
            else
            {
                return notValidMove(playerBeforeMove);
            }
            bool gameOver = gameTerminates(movedCell, lives);
            return new Player(this, movedCell, moves, lives,gameOver);
        }

        private bool gameTerminates(Cell movedCell, int lives)
        {
            if (lives == 0) { return true; }
            if (movedCell.j == boardSize - 1) { return true; }
            return false;
        }
        public bool gameOverPlayerWins(Cell position)
        {
            return position.j == 7;
        }
    }

}
