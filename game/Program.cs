using System.Security;

namespace game
{

    class Program
    {
        static void Main(string[] args)
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            var mines = new Mines(mineFillFactor, mineCount);
            var player = new Player('a', mines);

        }
    }
}
