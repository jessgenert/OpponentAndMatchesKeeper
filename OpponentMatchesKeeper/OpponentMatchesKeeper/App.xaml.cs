using Xamarin.Forms;

namespace OpponentMatchesKeeper
{
    public partial class App : Application
    {
        static Games_Database database;

        public static Games_Database Database
        {
            get
            {
                if (database == null) // havent connected yet
                {
                    database = new Games_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("GamesSQLite.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            database = Database;

            MainPage = new NavigationPage(new OpponentsPage());


            ToolbarItem tbGames = new ToolbarItem { Text = "Games" };
            tbGames.Clicked += (sender, e) => { MainPage.Navigation.PushAsync(new GamesPage()); };
            MainPage.ToolbarItems.Add(tbGames);

            ToolbarItem tbSettings = new ToolbarItem { Text = "Settings" };
            tbSettings.Clicked += (sender, e) => { MainPage.Navigation.PushAsync(new SettingsPage()); };
            MainPage.ToolbarItems.Add(tbSettings);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}


