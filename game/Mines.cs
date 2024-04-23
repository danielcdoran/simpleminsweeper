using System;
using System.ComponentModel;
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
        private int _maxMinesAllowed;
        private int _minesInBoard;
        private double _mineFactor;



        public Mines(double mineFactor, int maxMinesAllowedToHit
        )
        {
            if (mineFactor < 0.01) { throw new InvalidBoardFillFactor(mineFactor); } // if fillFactor is 0 mines will never be created
            if (mineFactor >= 1.0) { throw new InvalidBoardFillFactor(mineFactor); }

            if (maxMinesAllowedToHit < 1) { throw new InvalidMineCount(maxMinesAllowedToHit); }
            if (maxMinesAllowedToHit > 64) { throw new InvalidMineCount(maxMinesAllowedToHit); } // can't be bigger than number of squares on board

            initialise();
            _maxMinesAllowed = maxMinesAllowedToHit;
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
            bool enoughMines = false;
            do
            {
                var rand = new Random();
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        var result = rand.NextDouble();
                        if (result < _mineFactor)
                        {
                            if (!isMineCell(new Cell(i, j)))
                            {
                                setCellAsMine(new Cell(i, j));
                            }
                        }
                    }
                }
                Console.WriteLine("_minesInBoard " + _minesInBoard + " minecount " + _maxMinesAllowed);
                enoughMines = (_minesInBoard >= _maxMinesAllowed);
            } while (!enoughMines);
        }

        public Mines(Cell[] cellsSetAsMine, int mineCount)
        {
            initialise();
            _maxMinesAllowed = mineCount;
            foreach (Cell cell in cellsSetAsMine)
            {
                setCellAsMine(cell);
            }
        }

        private bool noLivesRemaining()
        {
            return (_maxMinesAllowed - _numberMinesHit <= 0);
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
            _minesInBoard++;
        }

        public string toString()
        {

            StringBuilder val = new StringBuilder();
            val.Append(string.Format("Mine count ={0} ", _minesInBoard));
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Cell cell = new Cell(i, j);
                    if (isMineCell(cell)) cellReference(val, cell);
                }
            }

            return val.Remove(val.Length - 1, 1).ToString();
        }

        private void cellReference(StringBuilder val, Cell cell)
        {
            val.Append(cell.toString());
            val.Append(" ");
        }


        public int NumberMines
        {
            get { return _numberMinesHit; }
        }
        public void addMineHit() {
            _numberMinesHit++;
        }
        public int livesRemaining()
        {
            return _maxMinesAllowed - _numberMinesHit;
        }
        public int minesInBoard
        {
            get => _minesInBoard;
        }
        public int MaxMinesAllowed
        {
            get => _maxMinesAllowed;
        }
        public bool isInRange(int row, int column)
        {
            if (row < 0) { return false; }
            if (row > 7) { return false; }
            if (column < 0) { return false; }
            if (column > 7) { return false; }
            return true;
        }

        public bool isInRange(Cell cell)
        {
            int row = cell.i;
            int column = cell.j;
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
        // Just increment the moves
        private Player notValidMoveIncrementMoves(Player player)
        {
            int moves = player.Moves + 1;
            return new Player(this, player.getCurrentPosition(), moves, false);
        }

        public Player Up(Player playerBeforeMove)
        {
            var cell = playerBeforeMove.getCurrentPosition();
            var movedCell = cell.Up();
            Console.WriteLine("player up " + movedCell.toString());
            return updatePlayer(playerBeforeMove, movedCell);
        }

        private Player updatePlayer(Player playerBeforeMove, Cell movedCell)
        {
            var moves = playerBeforeMove.Moves; ;

            if (!isInRange(movedCell)) return notValidMoveIncrementMoves(playerBeforeMove);
            if (isMineCell(movedCell))
            {
                _numberMinesHit++;
            }
            moves++;
            bool gameOver = gameTerminates(movedCell);
            return new Player(this, movedCell, moves, gameOver);
        }

        public Player Down(Player playerBeforeMove)
        {
            var cell = playerBeforeMove.getCurrentPosition();
            var movedCell = cell.Down();
            return updatePlayer(playerBeforeMove, movedCell);
        }

        public Player Right(Player playerBeforeMove)
        {
            var cell = playerBeforeMove.getCurrentPosition();
            var movedCell = cell.Right();
            return updatePlayer(playerBeforeMove, movedCell);
        }

        public Player Left(Player playerBeforeMove)
        {
            var cell = playerBeforeMove.getCurrentPosition();
            var movedCell = cell.Left();
            return updatePlayer(playerBeforeMove, movedCell);
        }

        private bool gameTerminates(Cell movedCell)
        {
            if  ( noLivesRemaining() ) return true; 
            if (movedCell.j == boardSize - 1) { return true; }
            return false;
        }
        public bool gameOverPlayerWins(Cell position)
        {
            return position.j == 7;
        }
    }

}
