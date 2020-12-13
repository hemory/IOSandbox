using System.Collections.Generic;

namespace IOSandbox
{
    public class PlayerComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.PointsPerGame < y.PointsPerGame)
            {
                return -1;
            }

            if (x.PointsPerGame > y.PointsPerGame)
            {
                return 1;
            }

            return 0;
        }
    }
}
