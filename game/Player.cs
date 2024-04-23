using System.Net;

namespace game
{
    public class Player
    {
        private Cell _currentPosition;
        int _moves;
        int _livesRemaining;
        bool _playerWins;
        bool _gameOver;
        const string columns = "abcdefgh"; // list of column names to find by IndexOf
        Mines _board;

        // Player start on columns a to h
        // Start on row 0 and must go to row 7 to win
        public Player(char initialColumn, Mines mines)
        {
            //   CultureInfo[] cultures= { CultureInfo.CreateSpecificCulture("en-US"),
            //                 CultureInfo.InvariantCulture,
            //                 CultureInfo.CreateSpecificCulture("tr-TR") };           
            char lower = Char.ToLower(initialColumn);
            int i = columns.IndexOf(lower);
            if (i < 0) { { throw new InvalidColumn(initialColumn); } }
            _board = mines;
            _currentPosition = new Cell(i, 0);
            if ( _board.isMineCell(_currentPosition)) {
                _board.addMineHit();
            }
            _livesRemaining = mines.livesRemaining();
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
        public bool GameOver {
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
        public string playerStatus()
        {
            return _currentPosition.position() + " lives remaining " + _livesRemaining + " moves taken " + _moves;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (object.ReferenceEquals(this, obj)) return true;

            var other = obj as Player;
            // private Cell _currentPosition;
            if (_moves != other.Moves) return false;
            if (_livesRemaining != other.LivesRemaining) return false;
            if (_board != other._board) return false;

            return true;
        }

    }

}