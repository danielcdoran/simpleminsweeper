namespace game {
    public class Player {
        private Cell _currentPosition;
        int _moves;
        int _livesRemaining;
        bool _playerWins ;
        bool _gameOver;
        const string columns="abcdefgh"; // list of column names to find by IndexOf
        Mines _board;

        // Player start on columns a to h
        // Start on row 0 and must go to row 7 to win
        public Player (char initialColumn,Mines mines){
                //   CultureInfo[] cultures= { CultureInfo.CreateSpecificCulture("en-US"),
                //                 CultureInfo.InvariantCulture,
                //                 CultureInfo.CreateSpecificCulture("tr-TR") };           
            char lower = Char.ToLower(initialColumn);
            int i = columns.IndexOf(lower);
            if (i<0) { { throw new InvalidColumn(initialColumn); } }
            mines = _board;
            _currentPosition = new Cell(i,0);
        }
        public void Up() {
            _currentPosition = _currentPosition.Up();
            return; 
        }

        public Cell getCurrentPosition() {
            return _currentPosition;
        }

    }

}