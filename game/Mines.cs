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
        const int maxIndex = boardSize - 1;
        bool[,]? _board;
        private int _numberMinesHit;
        private int _maxLivesAllowed;
        private int _minesInBoard;
        private double _mineFactor;


        // This constructor randomly creates mines. It must at least maxLivesAllowed mines otherwise the playerwill 
        // always win (there are not enough mines to cause a loss)
        // Mines are randomly created but the probability must be greater than 0.0 and less than 1.0.
        // The lower limit is 0.01 but there is no reason for this. It just needs to be a small probability

        public Mines(double mineFactor, int maxLivesAllowed)
        {
            if (mineFactor < 0.01) { throw new InvalidBoardFillFactor(mineFactor); } // if fillFactor is 0 mines will never be created
            if (mineFactor >= 1.0) { throw new InvalidBoardFillFactor(mineFactor); }

            if (maxLivesAllowed < 1) { throw new InvalidMineCount(maxLivesAllowed); }
            if (maxLivesAllowed > 64) { throw new InvalidMineCount(maxLivesAllowed); } // can't be bigger than number of squares on board

            initialise();
            _maxLivesAllowed = maxLivesAllowed;
            _mineFactor = mineFactor;
            makeBoardWithMines();
        }
        public Mines(Cell[] cellsSetAsMine, int mineCount)
        {
            initialise();
            _maxLivesAllowed = mineCount;
            setMinesFromArray(cellsSetAsMine);
        }
        void setMinesFromArray(Cell[] cellsSetAsMine)
        {
            foreach (Cell cell in cellsSetAsMine)
            {
                setCellAsMine(cell);
            }
        }

        // This creates a board with 0 (zero) mines in it
        public Mines(int mineCount)
        {
            initialise();
            _maxLivesAllowed = mineCount;
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
                enoughMines = (_minesInBoard >= _maxLivesAllowed);
            } while (!enoughMines);
        }

        private bool noLivesRemaining()
        {
            return (_maxLivesAllowed - _numberMinesHit <= 0);
        }
        private bool isMineCell(Cell cell)
        {
            var i = cell.i;
            var j = cell.j;
            if (!cell.isInRange()) return false;
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


        public int NumberMinesHit
        {
            get { return _numberMinesHit; }
        }
        public void addMineHit()
        {
            _numberMinesHit++;
        }
        public int livesRemaining()
        {
            return _maxLivesAllowed - _numberMinesHit;
        }
        public int minesInBoard
        {
            get => _minesInBoard;
        }
        public int MaxLivesAllowed
        {
            get => _maxLivesAllowed;
        }
        public bool isInRange(int row, int column)
        {
            if (row < 0) { return false; }
            if (row > maxIndex) { return false; }
            if (column < 0) { return false; }
            if (column > maxIndex) { return false; }
            return true;
        }

        public bool isInRange(Cell cell)
        {
            return isInRange(cell.i,cell.j);
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
        public bool gameTerminates(Cell position)
        {
            if (_maxLivesAllowed == _numberMinesHit) return true;
            if (position.j == boardSize - 1) return true;
            return false;
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
        // 1 - Player lost - no lives remaining
        // 2 - Player won. Reached row 8
        // 3 - Game not finished yet
        public int gameStatus(Cell movedCell)
        {
            if (noLivesRemaining()) return 1;
            if (movedCell.j == boardSize - 1) { return 2; }
            return 3;
        }
        public bool gameOverPlayerWins(Cell position)
        {
            return position.j == maxIndex;
        }
    }

}
