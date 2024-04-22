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
            var expected = new Cell(0, 0);
            var mines = new Mines(mineFillFactor, mineCount);
            var player = new Player('a', mines);
            Assert.Equal(expected, player.getCurrentPosition());
        }
        [Fact]
        public void given_ValidColumn_h_then_InitaliseCell()
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var expected = new Cell(0, 0);
            var mines = new Mines(mineFillFactor, mineCount);
            var player = new Player('h', mines);
            Assert.Equal(expected, player.getCurrentPosition());
        }

    }
}

