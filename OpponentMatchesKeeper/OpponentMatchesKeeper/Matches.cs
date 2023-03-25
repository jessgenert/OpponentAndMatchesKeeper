using SQLite;
using System;

namespace OpponentMatchesKeeper
{
    public class Matches
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int OpponentID { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int GameID { get; set; }
        public bool Win { get; set; }

    }
}
