using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Security;

namespace game
{   class Program
    {
        static void Main(string[] args)
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            // const string columns = "abcdefgh";
            var mines = new Mines(mineFillFactor, mineCount);
            var game = new Game(mines);
            game.RunGame();
    
        }
    }
}
