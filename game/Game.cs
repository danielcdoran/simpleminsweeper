using System.Security.Cryptography.X509Certificates;

namespace game
{


    public class Game
    {
        const string columns = "abcdefgh";
        Mines _mines;
        public Game(double mineFillFactor, int livesAllowed)
        {
            _mines = new Mines(mineFillFactor, livesAllowed);
        }
        public Game(Mines mines)
        {
            _mines = mines;
        }
        public void RunGame()
        {
            bool gameEnded = false;
            bool correct = true;
            //     int i, j;
            char startColumn = 'z';
            var player = new Player(_mines);
            do
            {
                Console.WriteLine("Set column (a-h) on bottom row to start");
                string? input_column = Console.ReadLine();
                if (input_column is not null)
                {
                    char[] characters = input_column.ToCharArray();
                    startColumn = Char.ToLower(characters[0]);
                    correct = (columns.IndexOf(startColumn) != -1);
                }
                if (!correct) Console.WriteLine("Try again. This column is not allowed");
            } while (!correct);
            player.setStartPosition(startColumn);
            correct = false;
            Char command = ' ';

            do
            {
                do
                {
                    Console.WriteLine("Move U(Up),D(Down),R(Right),L(Left)");
                    string? inputMovement = Console.ReadLine();
                    if (inputMovement is not null)
                    {
                        correct = inputMovement.Length > 0;
                        command = inputMovement.ToCharArray()[0];
                    }
                } while (!correct);

                player = player.runCommand(command);
                string output = player.playerStatus();
                Console.WriteLine(output);
                gameEnded = player.GameOver;
            } while (!gameEnded);
            gameStatus(player);
        }
        public void gameStatus(Player player){
                        int status = player.gameStatus();
                        switch (status) {
                            case 1:
                            Console.WriteLine("Player lost");
                            break;
                            case 2:
                            Console.WriteLine("Player won");
                            break;
                            case 3:
                            Console.WriteLine("Game is still going");
                            break;
                            default:
                            Console.WriteLine("Error in reporting game staatus");
                            break;
                        }
        }

        public Player runCommands(Player player, char[] commands)
        {
            foreach (char command in commands)
            {
                player = player.runCommand(command);
                Console.WriteLine(player.playerStatus());
            }
            return player;
        }
        public string runCommands(string commandString, Mines mines)
        {
            _mines = mines;
            char[] commands = commandString.ToCharArray();
            var player = new Player(_mines);
            player = player.setStartPosition(commands[0]);
            string allConsoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                Console.WriteLine(player.playerStatus());
                player =runCommands(player, commands[1..]);
                gameStatus(player);
                allConsoleOutput = stringWriter.ToString();
            }

            return allConsoleOutput;
        }
    }
}

// const double mineFillFactor = 0.1;
// const int mineCount = 2;
// const string columns = "abcdefgh";
// var mines = new Mines(mineFillFactor, mineCount);
// Console.WriteLine(mines.toString());
// bool gameEnded = false;
// bool correct = true;
// //     int i, j;
// char startColumn = 'z';
//             var player = new Player(mines);
// do
// {
//     Console.WriteLine("Set column (a-h) on bottom row to start");
//     string? input_column = Console.ReadLine();
//     if (input_column is not null)
//     {
//         char[] characters = input_column.ToCharArray();
//         startColumn = Char.ToLower(characters[0]);
//         correct = (columns.IndexOf(startColumn) != -1);
//     }
//     if (!correct) Console.WriteLine("TRy again. This column is not allowed");
// } while (!correct);
// player.setStartPosition(startColumn);
// correct = false;
// Char command = ' ';

// do
// {
//     do
//     {
//         Console.WriteLine("Move U(Up),D(Down),R(Right),L(Left)");
//         string? inputMovement = Console.ReadLine();
//         if (inputMovement is not null)
//         {
//             correct = inputMovement.Length > 0;
//             command = inputMovement.ToCharArray()[0];
//         }
//     } while (!correct);

//     player = player.runCommand(command);
//     string output = player.playerStatus();
//     Console.WriteLine(output);
//     gameEnded = player.GameOver;
// } while (!gameEnded);
