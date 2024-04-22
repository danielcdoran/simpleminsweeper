using game;

namespace game.Test
{

    public class MineTests
    {

        [Fact]
        public void GivenMinesFillFactor_thenProduceBoardWithMines()
        {
            Mines initial = new Mines(0.9, 2);
            string output = initial.toString();
            int size = output.Length;
            Assert.True(initial.NumberMines > 2);
        }

        [Fact]
        public void GivenMineList_thenProduceMines()
        {
            int expectedMineCount = 3;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            Mines initial = new Mines(mineCells, 2);
            Assert.Equal(expectedMineCount, initial.NumberMines);
            // Assert.True(initial.toStringMines().Length > 22);
        }

        [Fact]
        public void given_ValidColumn_a_then_InitaliseCell()
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var mines = new Mines(mineFillFactor, mineCount);
            var expected = mines.getCell(0, 0);
            var player = new Player('a', mines);
            Assert.Equal(expected, player.getCurrentPosition());
        }
        [Fact]
        public void given_ValidColumn_h_then_InitaliseCell()
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var mines = new Mines(mineFillFactor, mineCount);
            var expected = mines.getCell(0, 7);
            var player = new Player('h', mines);
            Assert.Equal(expected, player.getCurrentPosition());
        }
        [Fact]
        public void given_UpCommandNoMine_then_moveRegistered()
        {
            const int mineCount = 2;
            Cell[] mineCells = new Cell[3] { new Cell(0, 0), new Cell(1, 2), new Cell(2, 3) };
            var mines = new Mines(mineCells, mineCount);
            var expected = mines.getCell(7, 0);
            var player = new Player('h', mines);
            var movedPlayer = player.Up();
            // Assert.Equal(expected, player.getCurrentPosition());
            Assert.Equal(expected.i, player.getCurrentPosition().i);
            Assert.Equal(expected.j, player.getCurrentPosition().j);
        }
    }
}

