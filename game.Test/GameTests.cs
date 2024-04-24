using System.Runtime.CompilerServices;
using game;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace game.Test
{
    public class GameTests
    {
        const string expectedDirectory = "expectedoutput";
        const double mineFillFactor = 0.1;
        const int mineCount = 2;
        const string actualDirectory = "actualoutput";

        [Fact]
        public void given_NoMines_when_UseCommands_thenPlayerWins()
        {
            string outputFilename = "given_NoMines_when_UseCommands_thenPlayerWins.txt";
            var mines = new Mines(mineCount);
            var game = new Game(mineFillFactor, mineCount);
            var player = new Player(mines);
            player = player.setStartPosition('a');
            string actualOutput = game.runCommands("cUURULUDUUUU", mines);
            createTestfile(actualDirectory, "given_NoMines_when_UseCommands_thenPlayerWins.txt", actualOutput);
            String actualString = readIntoString(actualDirectory, outputFilename);
            String expectedString = readIntoString(expectedDirectory, outputFilename);
            Assert.Equal(expectedString, actualString);
        }
        static string getTestOutputDir(string testDir)
        {
            string pathToBinDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string testFilesRelativePath = @"..\testfiles\" + testDir;
            return Path.GetFullPath(Path.Combine(pathToBinDirectory, testFilesRelativePath)); ;
        }

        static string readIntoString(string testDirectory, string fileName)
        {
            string outputDir = getTestOutputDir(testDirectory);
            DirectoryInfo di = Directory.CreateDirectory(outputDir);
            string actualFile = Path.Combine(di.FullName, fileName);
            StreamReader reader = new StreamReader(actualFile);
            string contents = reader.ReadToEnd();
            reader.Close();
            return contents;
        }

        static void createTestfile(string testDirectory, string fileName, string actualOutput)
        {
            string outputDir = getTestOutputDir(testDirectory);
            DirectoryInfo di = Directory.CreateDirectory(outputDir);
            string actualFile = Path.Combine(di.FullName, fileName);
            // using (FileStream fs = new FileStream(actualFile, FileMode.CreateNew))
            // {
            using (StreamWriter actualStream = new StreamWriter(actualFile, false))
            {
                actualStream.Write(actualOutput);
            } 
        }
    }
    // static bool compareFiles(string path1, string path2)
    // {
    //     using (FileStream fs1 = new FileStream(path1, FileMode.Open),
    //           fs2 = new FileStream(path2, FileMode.Open))
    //     {
    //         int c1 = 0;
    //         int c2 = 0;
    //         do
    //         {
    //             c1 = fs1.ReadByte();
    //             c2 = fs2.ReadByte();
    //         }
    //         while (c1 == c2 && c1 != -1 && c2 != -1);
    //         return c1 == c2;
    //     }
    // }
}

