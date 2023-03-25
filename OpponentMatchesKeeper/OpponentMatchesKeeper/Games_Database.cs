using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpponentMatchesKeeper
{
    public class Games_Database
    {
        readonly SQLiteConnection database;
        public Games_Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            CreateTables();


        }

        public void CreateTables()
        {
            database.CreateTable<Games>();
            if (database.Table<Games>().Count() == 0)
            {
                Games chess = new Games
                {
                    GameName = "Chess",
                    Description = "Simple Grid Game",
                    Rating = 9.5
                };
                SaveGame(chess);
                Games checkers = new Games
                {
                    GameName = "Checkers",
                    Description = "Simple Grid Game",
                    Rating = 5
                };
                SaveGame(checkers);
                Games dominoes = new Games
                {
                    GameName = "Dominoes",
                    Description = "Blocks game",
                    Rating = 6.75
                };
                SaveGame(dominoes);
            }
            database.CreateTable<Matches>();
            database.CreateTable<Opponents>();

        }
        public int SaveGame(Games item)
        {
            return database.Insert(item);

        }


        public int SaveOpponent(Opponents item)
        {
            return database.Insert(item);
        }

        public object DeleteOpponent(Opponents item)
        {
            return database.Delete(item);
        }

        public int SaveMatch(Matches item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }


        }

        public object DeleteMatch(Matches item)
        {
            return database.Delete(item);
        }

        public List<Games> GetGames()
        {
            return database.Table<Games>().ToList();
        }

        public ObservableCollection<Matches> GetOpponentsMatches(int id)
        {
            List<Matches> matches = database.Table<Matches>().Where(x => x.OpponentID == id).ToList();
            return new ObservableCollection<Matches>(matches);


        }

        public ObservableCollection<Opponents> GetOpponents()
        {
            List<Opponents> opponents = database.Table<Opponents>().ToList();
            return new ObservableCollection<Opponents>(opponents);
        }

        public Opponents GetOpponent(int id)
        {
            return database.Table<Opponents>().Where(i => i.ID == id).FirstOrDefault();
        }

        public Games GetGame(int id)
        {
            return database.Table<Games>().Where(i => i.ID == id).FirstOrDefault();
        }

        public int GetGameID(string item)
        {
            return database.Table<Games>().Where(i => i.GameName == item).FirstOrDefault().ID;
        }


        public string GetGameName(Opponents opponent)
        {

            int gameid = database.Table<Matches>().Where(i => i.OpponentID == opponent.ID).FirstOrDefault().GameID;
            return database.Table<Games>().Where(i => i.ID == gameid).FirstOrDefault().GameName;
        }


        public int GetGameCount(Games item)
        {
            return database.Table<Matches>().Where(i => i.GameID == item.ID).Count();
        }

        public void DropTables()
        {
            database.DropTable<Matches>();
            database.DropTable<Opponents>();
            database.DropTable<Games>();
            CreateTables();
            OpponentsPage.Opponents.Clear();
        }
    }
}
