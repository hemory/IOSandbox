using Newtonsoft.Json;

namespace IOSandbox
{

    public class RootObject
    {
        public Player[] Players { get; set; }
    }

    public class Player
    {
        [JsonProperty(PropertyName = "first_name")] //matches JSon object name with whatever I want
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "points_per_game")]
        public double PointsPerGame { get; set; }


        [JsonProperty(PropertyName = "second_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "team_name")]
        public string TeamName { get; set; }



    }

}
