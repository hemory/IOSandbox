using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IOSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDir);

            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");

            var fileContents = ReadSoccerResults(fileName);


            Console.ReadLine();
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<GameResult> ReadSoccerResults(string fileName)
        {
            var soccerResults = new List<GameResult>();

            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    GameResult gameResult = new GameResult();
                    string[] values = line.Split(',');

                    if (DateTime.TryParse(values[0], out DateTime gameDate))
                    {
                        gameResult.GameDate = gameDate;
                    }

                    gameResult.TeamName = values[1];
                    if (Enum.TryParse(values[2], out HomeOrAway homeOrAway))
                    {
                        gameResult.HomeOrAway = homeOrAway;
                    }



                    soccerResults.Add(values);

                }
            }

            return soccerResults;

        }

    }
}

