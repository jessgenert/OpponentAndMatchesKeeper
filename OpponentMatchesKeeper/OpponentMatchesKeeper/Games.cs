using SQLite;
using System;

namespace OpponentMatchesKeeper
{
    public class Games
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public string GameName { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}
