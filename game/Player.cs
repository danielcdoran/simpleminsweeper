using System.Net;

namespace game
{
    public class Player
    {
        private Cell? _currentPosition;
        int _moves;
        int _livesRemaining;
        bool _gameOver;
        const string columns = "abcdefgh"; // list of column names to find by IndexOf
        Mines _board;

        // Player start on columns a to h
        // Start on row 0 and must go to row 7 to win
        public Player(Mines mines)
        {
            _board = mines;
        }
        public Player(Mines mines,Cell position){

                       _board = mines;
            _currentPosition = position;
            _moves = 0;
            _livesRemaining = mines.livesRemaining();
            _gameOver = false;
        }

        public Player(Mines mines, Cell position, int moves, bool gameOver)
        {
            _board = mines;
            _currentPosition = position;
            _moves = moves;
            _livesRemaining = mines.livesRemaining();
            _gameOver = gameOver;
        }

        public int Moves
        {
            get => _moves;
        }
        public int LivesRemaining
        {
            get => _livesRemaining;
        }
        public bool GameOver
        {
            get => _gameOver;
        }
        public Player Up()
        {
            if (_gameOver) return this;
            return _board.Up(this);
        }
        public Player Down()
        {
            if (_gameOver) return this;
            return _board.Down(this);
        }

        public Player Right()
        {
            if (_gameOver) return this;
            return _board.Right(this);
        }
        public Player Left()
        {
            if (_gameOver) return this;
            return _board.Left(this);
        }

        public Cell getCurrentPosition()
        {
            return _currentPosition;
        }
        public bool gameOverPlayerWins(Cell position)
        {
            return _board.gameOverPlayerWins(position);
        }

  // 1 - Game won - no lives remianing
// 2 - Player won. Reached row 8
// 3 - Game not finished yet
        public int gameStatus()
        {
            return _board.gameStatus(_currentPosition);
        }
        public string playerStatus()
        {
            return _currentPosition.position() + " lives remaining " + _livesRemaining + " moves taken " + _moves;
        }
        public Player runCommand(Char command)
        {

            switch (Char.ToUpper(command))
            {
                case 'U':
                    return this.Up();

                case 'D':
                    return this.Down();

                case 'R':
                    return this.Right();

                case 'L':
                    return this.Left();

                default:
                    return this;
            };
        }


        public Player setStartPosition(Char initialColumn)
        {
            //   CultureInfo[] cultures= { CultureInfo.CreateSpecificCulture("en-US"),
            //                 CultureInfo.InvariantCulture,
            //                 CultureInfo.CreateSpecificCulture("tr-TR") };           
            char lower = Char.ToLower(initialColumn);
            int i = columns.IndexOf(lower);
            if (i < 0) { { throw new InvalidColumn(initialColumn); } }
            _currentPosition = new Cell(i, 0);
            if (_board.isMineCell(_currentPosition))
            {
                _board.addMineHit();
            }
            _livesRemaining = _board.livesRemaining();
            return this;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            var other = obj as Player;
            // private Cell _currentPosition;
            if (ReferenceEquals(null, other)) return false;
            if (_moves != other.Moves) return false;
            if (_livesRemaining != other.LivesRemaining) return false;
            if (_board != other._board) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return (_moves, _livesRemaining, _board).GetHashCode();
        }

    }

}