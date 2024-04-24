using game;

namespace game.Test
{

    public class MineTests
    {
        // This could be a "flakey" tet because it uses a random function;
        [Fact]
        public void GivenMinesFillFactor_thenProduceBoardWithMines()
        {
            Mines initial = new Mines(0.9, 2);
            string output = initial.toString();
            int size = output.Length;
            Assert.True(initial.minesInBoard > 2);
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(0.0)]
        public void givenMinesFillFactorGT1_thenProduceInvalidBoardFillFactorException(double fillFactor)
        {
            Assert.Throws<InvalidBoardFillFactor>(() => new Mines(fillFactor, 2));
        }


        [Fact]
        public void GivenMineList_thenProduceMines()
        {
            int expectedMineCount = 3;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            Mines initial = new Mines(mineCells, 2);
            Assert.Equal(expectedMineCount, initial.minesInBoard);
        }

        [Fact]
        public void given_ValidColumn_a_then_InitaliseCell()
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var mines = new Mines(mineFillFactor, mineCount);
            var expected = mines.getCell(0, 0);
            var player = new Player(mines);
            player = player.setStartPosition('a');
            Assert.Equal(expected, player.getCurrentPosition());
        }
        [Fact]
        public void given_ValidColumn_h_then_InitaliseCell()
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var mines = new Mines(mineFillFactor, mineCount);
            var expected = mines.getCell(7, 0);
            var player = new Player(mines);
            player = player.setStartPosition('h');
            Assert.Equal(expected, player.getCurrentPosition());
        }
        [Fact]
        public void given_UpCommandNoMine_then_moveRegistered()
        {
            const int mineCount = 2;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var expected = mines.getCell(7, 0);
            var player = new Player(mines);
            player = player.setStartPosition('h');
            var movedPlayer = player.Up();
            Assert.Equal(expected.i, player.getCurrentPosition().i);
            Assert.Equal(expected.j, player.getCurrentPosition().j);
        }


        [Fact]
        public void given_UpCommandHitsMine_then_MovesIncreaseLivesDecrease()
        {
            const int mineCount = 2;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var player = new Player(mines, new Cell(1, 1), 1, false);
            var movedPlayer = player.Up();
            var expected = new Player(mines, new Cell(1, 2), 2, false);
            // Assert.Equal(expected, player.getCurrentPosition());
            Assert.Equal(expected, movedPlayer);
            // Assert.Equal(expected, player.getCurrentPosition().j);
        }
        [Fact]
        public void given_UpCommandOntoMine_then_GameEndWithZeroLives()
        {
            const int mineCount = 1;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var player = new Player(mines, new Cell(1, 1), 1, false);
            var movedPlayer = player.Up();
            var expected = new Player(mines, new Cell(1, 2), 2, false);
            // Assert.Equal(expected, player.getCurrentPosition());
            Assert.Equal(expected, movedPlayer);
            // Assert.Equal(expected, player.getCurrentPosition().j);
        }
        [Fact]
        public void given_UpCommandOntoLastLine_then_GameEndPlayerWins()
        {
            const int mineCount = 1;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var player = new Player(mines, new Cell(1, 6), 1, false);
            var movedPlayer = player.Up();
            var expected = new Player(mines, new Cell(1, 7), 2, true);
            // Assert.Equal(expected, player.getCurrentPosition());
            Assert.Equal(expected, movedPlayer);
            // Assert.Equal(expected, player.getCurrentPosition().j);
        }

        [Fact]
        public void given_DownCommand_then_MoveDown()
        {
            const int mineCount = 1;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var player = new Player(mines, new Cell(1, 6), 1, false);
            var movedPlayer = player.Down();
            var expected = new Player(mines, new Cell(1, 5), 2, true);
            Assert.Equal(expected, movedPlayer);
        }
    }
}

