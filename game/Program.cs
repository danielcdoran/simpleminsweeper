using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Security;

namespace game
{

    class Program
    {
        static void Main(string[] args)
        {
            const double mineFillFactor = 0.1;
            const int mineCount = 2;
            // const string columns = "abcdefgh";
            var mines = new Mines(mineFillFactor, mineCount);
            var game = new Game(mines);
            game.RunGame();
    
        //     Console.WriteLine(mines.toString());
        //     bool gameEnded = false;
        //     bool correct = true;
        //     //     int i, j;
        //     char startColumn = 'z';
        //                 var player = new Player(mines);
        //     do
        //     {
        //         Console.WriteLine("Set column (a-h) on bottom row to start");
        //         string? input_column = Console.ReadLine();
        //         if (input_column is not null)
        //         {
        //             char[] characters = input_column.ToCharArray();
        //             startColumn = Char.ToLower(characters[0]);
        //             correct = (columns.IndexOf(startColumn) != -1);
        //         }
        //         if (!correct) Console.WriteLine("TRy again. This column is not allowed");
        //     } while (!correct);
        //     player.setStartPosition(startColumn);
        //     correct = false;
        //     Char command = ' ';

        //     do
        //     {
        //         do
        //         {
        //             Console.WriteLine("Move U(Up),D(Down),R(Right),L(Left)");
        //             string? inputMovement = Console.ReadLine();
        //             if (inputMovement is not null)
        //             {
        //                 correct = inputMovement.Length > 0;
        //                 command = inputMovement.ToCharArray()[0];
        //             }
        //         } while (!correct);

        //         player = player.runCommand(command);
        //         string output = player.playerStatus();
        //         Console.WriteLine(output);
        //         gameEnded = player.GameOver;
        //     } while (!gameEnded);

        }
    }
}
