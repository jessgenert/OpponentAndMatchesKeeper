using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;


namespace OpponentMatchesKeeper
{
    public class MatchesPage : ContentPage
    {
        public static ObservableCollection<Matches> Matches = new ObservableCollection<Matches>();

        public static Opponents Opponent = new Opponents();

        public static Matches match = new Matches();


        public MatchesPage(Opponents opponent)
        {

            Title = "Opponents";



            Opponent = opponent;


            Matches = App.Database.GetOpponentsMatches(Opponent.ID);

            string lastPickedGame = Matches.Count == 0 ? "Chess" : App.Database.GetGame(Matches.LastOrDefault().GameID).GameName;

            ListView listViewMatches = new ListView
            {
                ItemsSource = Matches,
                ItemTemplate = new DataTemplate(typeof(MatchesCell)),
                RowHeight = MatchesCell.RowHeight
            };


            // Table to contain our "form"
            TableView table = new TableView { Intent = TableIntent.Form };

            DatePicker dDate = new DatePicker { Date = DateTime.Now };
            dDate.Format = "dddd, MMMM dd, yyyy";
            ViewCell cDate = new ViewCell
            {

                View = new StackLayout
                {
                    Margin = new Thickness(17, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    Children = { dDate },

                }
            };

            EntryCell eComments = new EntryCell { Label = "Comment: " };

            List<string> gameNames = new List<string>();
            List<Games> gameList = App.Database.GetGames();
            foreach (Games game in gameList)
            {
                gameNames.Add(game.GameName);
            }

            Picker gamePicker = new Picker { WidthRequest = 100, ItemsSource = gameNames, SelectedItem = lastPickedGame };

            Label lblGame = new Label { Text = "Game: ", VerticalTextAlignment = TextAlignment.Center };

            ViewCell cGame = new ViewCell
            {
                View = new StackLayout
                {
                    Margin = new Thickness(17, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    Children = { lblGame, gamePicker }
                }
            };

            Label lblWin = new Label { Text = "Win?", VerticalTextAlignment = TextAlignment.Center };

            Switch swtWin = new Switch { HorizontalOptions = LayoutOptions.EndAndExpand };

            ViewCell cWin = new ViewCell
            {
                View = new StackLayout
                {
                    Margin = new Thickness(17, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    Children = { lblWin, swtWin }
                }
            };



            Button btnAdd = new Button { Text = "Add", WidthRequest = 75 };
            btnAdd.Clicked += (sender, e) =>
            {

                match.Win = swtWin.IsToggled;
                match.OpponentID = Opponent.ID;
                match.Comments = eComments.Text;
                match.Date = dDate.Date;
                match.GameID = App.Database.GetGameID(gamePicker.SelectedItem.ToString());



                eComments.Text = "";
                dDate.Date = DateTime.Now;
                dDate.Format = "dddd, MMMM dd, yyyy";









                App.Database.SaveMatch(match);

                int currentMatchIndex = Matches.IndexOf(match);
                if (currentMatchIndex == -1)
                {
                    Matches.Add(match);
                    match = new Matches();
                }
                else
                {
                    Matches.Remove(match);
                    Matches.Insert(currentMatchIndex, match);
                    match = new Matches();
                }









            };

            ViewCell cAdd = new ViewCell
            {
                View = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Children = { btnAdd }
                }
            };

            TableSection section = new TableSection("Add Match")
            {
                cDate, eComments, cGame, cWin, cAdd
            };

            table.Root = new TableRoot { section };

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                }
            };

            listViewMatches.ItemTapped += (sender, e) =>
            {
                listViewMatches.SelectedItem = null; // deselect the list views current item

                match = e.Item as Matches;

                eComments.Text = match.Comments;
                swtWin.IsToggled = match.Win;
                Opponent.ID = match.OpponentID;
                dDate.Date = match.Date;
                dDate.Format = "dddd, MMMM dd, yyyy";
            };

            StackLayout layout = new StackLayout();
            grid.Children.Add(listViewMatches, 0, 1);
            grid.Children.Add(table, 0, 2);
            layout.Children.Add(grid);

            Content = layout;
        }


    }



    public class MatchesCell : ViewCell
    {
        public const int RowHeight = 80;
        private Label lblFirstName = new Label();
        private Label lblLastName = new Label();
        private Label lblGame = new Label();

        public MatchesCell()
        {

            Label lblDate = new Label { };
            lblDate.SetBinding(Label.TextProperty, "Date", stringFormat: "{0:D}");

            Label lblComments = new Label { HorizontalOptions = LayoutOptions.EndAndExpand };
            lblComments.SetBinding(Label.TextProperty, "Comments");

            Label lblWin = new Label { Text = "Win?", HorizontalOptions = LayoutOptions.EndAndExpand };

            Switch swtWin = new Switch { HorizontalOptions = LayoutOptions.End, IsEnabled = false };
            swtWin.SetBinding(Switch.IsToggledProperty, "Win");

            View = new StackLayout
            {

                Orientation = StackOrientation.Vertical,
                Children =
                    {
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { lblFirstName, lblLastName }
                        },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { lblDate, lblComments }
                        },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { lblGame, lblWin, swtWin }
                        }
                    }
            };

            MenuItem deleteMatch = new MenuItem()
            {
                Text = "Delete",
                IsDestructive = true

            };

            deleteMatch.Clicked += (sender, e) =>
            {
                ListView parent = (ListView)Parent;
                MatchesPage.Matches = (ObservableCollection<Matches>)parent.ItemsSource;
                Matches match = (Matches)BindingContext;
                MatchesPage.Matches.Remove(match);
                App.Database.DeleteMatch(match);
            };

            ContextActions.Add(deleteMatch);
            lblFirstName.Text = MatchesPage.Opponent.FirstName;
            lblLastName.Text = MatchesPage.Opponent.LastName;
            lblGame.Text = App.Database.GetGameName(MatchesPage.Opponent);






        }

    }

}
