using System.Security;

namespace game
{

    class Program
    {
        static void Main(string[] args)
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            const string columns = "abcdefgh";
            var mines = new Mines(mineFillFactor, mineCount);
            Console.WriteLine(mines.toString());
            bool gameEnded = false;
            bool correct = true;
            //     int i, j;
            char startColumn;
                do
                {
                    Console.WriteLine("Set column (a-h) on bottom row to start");
                    string input_column = Console.ReadLine();
                    char[] characters = input_column.ToCharArray();
                    startColumn = Char.ToLower(characters[0]);
                    correct = (columns.IndexOf(startColumn) != -1);
                    if (!correct) Console.WriteLine("TRy again. This column is not allowed");
                } while (!correct);
            Char command;
              var player = new Player(startColumn, mines);
            do
            {
                do
                {
                    Console.WriteLine("Move U(Up),D(Down),R(Right),L(Left)");
                    string input_direction = Console.ReadLine();
                    char[] characters = input_direction.ToCharArray();
                    command = Char.ToUpper(characters[0]);
                } while (!correct);

                switch (command) {
                    case 'U':
                        player = player.Up();
                        break;
                    case 'D':
                        player = player.Down();
                        break;
                    case 'R':
                        player = player.Right();
                        break;
                    case 'L':
                        player = player.Left();
                        break;
                    default:
                        break;
                };
                string output = player.playerStatus();
                Console.WriteLine(output);
            } while (!gameEnded);

        }
    }
}
