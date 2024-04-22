using System.Security;

namespace game
{

    class Program
    {
        static void Main(string[] args)
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
Console.WriteLine("BEfore");
            var mines = new Mines(mineFillFactor, mineCount);
            Console.WriteLine(mines.toString());
            var player = new Player('a', mines);
            bool gameEnded = false;
            bool correct = true;
            //     int i, j;
            Char command;

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
                        player.Up();
                        break;
                    case 'D':
                        player.Up();
                        break;
                    case 'R':
                        player.Up();
                        break;
                    case 'L':
                        player.Up();
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
