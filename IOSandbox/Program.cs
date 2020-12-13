using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

            fileName = Path.Combine(directory.FullName, "players.json");

            List<Player> players = DeserializePlayers(fileName);

            List<Player> topTenPlayer = GetTopTenPlayer(players);

            foreach (var player in topTenPlayer)
            {
                Console.WriteLine($"Name: {player.FirstName} PPG: {player.PointsPerGame}");
            }

            fileName = Path.Combine(directory.FullName, "topTen.json");

            SerializePlayersToFile(players,fileName);


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

                    int parseInt;

                    if (int.TryParse(values[3], out parseInt))
                    {
                        gameResult.Goals = parseInt;
                    }

                    if (int.TryParse(values[4], out parseInt))
                    {
                        gameResult.GoalAttempts = parseInt;
                    }

                    if (int.TryParse(values[5], out parseInt))
                    {
                        gameResult.ShotsOnGoal = parseInt;
                    }

                    if (int.TryParse(values[6], out parseInt))
                    {
                        gameResult.ShotsOffGoal = parseInt;
                    }

                    double possessionPercent;
                    if (double.TryParse(values[7], out possessionPercent))
                    {
                        gameResult.PossessionPercent = possessionPercent;
                    }

                    soccerResults.Add(gameResult);

                }
            }

            return soccerResults;

        }



        // Turns the Json object into a player object using deserialization
        public static List<Player> DeserializePlayers(string fileName)
        {
            List<Player> players = new List<Player>();

            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader reader = new StreamReader(fileName))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }

            return players;
        }

        public static List<Player> GetTopTenPlayer(List<Player> players)
        {
            List<Player> topTenPlayers = new List<Player>();

            players.Sort(new PlayerComparer());
            players.Reverse();


            int counter = 0;

            foreach (var player in players)
            {
                topTenPlayers.Add(player);
                counter++;
                if (counter == 10)
                {
                    break;
                }
            }
            return topTenPlayers;

        }

        public static void SerializePlayersToFile(List<Player> players, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(fileName))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, players);
            }
        }
    }
}

