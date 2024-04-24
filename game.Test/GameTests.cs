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
            createTestfile(actualDirectory, outputFilename, actualOutput);
            String actualString = readIntoString(actualDirectory, outputFilename);
            String expectedString = readIntoString(expectedDirectory, outputFilename);
            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void given_2Mines_when_HitsBoth_thenPlayerLosses()
        {
            string outputFilename = "given_2Mines_when_HitsBoth_thenPlayerLosses.txt";
            Cell[] mineCells = new Cell[3] { new Cell(2, 2), new Cell(4, 2), new Cell(4, 6) };
            var mines = new Mines(mineCells, mineCount);
            var game = new Game(mines);
            var player = new Player(mines);
            player = player.setStartPosition('a');
            string actualOutput = game.runCommands("cUURR", mines);
            createTestfile(actualDirectory, outputFilename, actualOutput);
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
}

