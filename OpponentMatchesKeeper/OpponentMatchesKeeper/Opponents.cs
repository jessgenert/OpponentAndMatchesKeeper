﻿using SQLite;

namespace OpponentMatchesKeeper
{
    public class Opponents
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
