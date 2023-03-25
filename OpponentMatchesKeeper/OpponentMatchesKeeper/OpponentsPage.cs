using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace OpponentMatchesKeeper
{

    public class OpponentsPage : ContentPage
    {

        public static ObservableCollection<Opponents> Opponents = new ObservableCollection<Opponents>();


        public OpponentsPage()
        {

            Title = "Opponents";

            Opponents = App.Database.GetOpponents();

            ListView listViewOpponents = new ListView
            {

                ItemsSource = Opponents,
                ItemTemplate = new DataTemplate(typeof(OpponentsCell)),
                RowHeight = OpponentsCell.RowHeight

            };

            listViewOpponents.ItemTapped += (sender, e) =>
            {
                listViewOpponents.SelectedItem = null; // deselect the list views current item
                Navigation.PushAsync(new MatchesPage(e.Item as Opponents));
            };

            Button btnNewOpponent = new Button { Text = "Add New Opponent" };
            btnNewOpponent.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new AddNewOpponent());
            };

            StackLayout layout = new StackLayout();
            layout.Children.Add(listViewOpponents);
            layout.Children.Add(btnNewOpponent);
            Content = layout;

        }
    }

    public class OpponentsCell : ViewCell
    {
        public const int RowHeight = 55;

        public OpponentsCell()
        {
            Label lblFirstName = new Label { VerticalTextAlignment = TextAlignment.Center };
            lblFirstName.SetBinding(Label.TextProperty, "FirstName");

            Label lblLastName = new Label { VerticalTextAlignment = TextAlignment.Center };
            lblLastName.SetBinding(Label.TextProperty, "LastName");

            Label lblPhone = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalTextAlignment = TextAlignment.Center };
            lblPhone.SetBinding(Label.TextProperty, "Phone");

            View = new StackLayout
            {
                Padding = new Thickness(30, 30, 0, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { lblFirstName, lblLastName, lblPhone },

            };

            MenuItem deleteOpponent = new MenuItem()
            {
                Text = "Delete",
                IsDestructive = true
            };

            deleteOpponent.Clicked += (sender, e) =>
            {
                ListView parent = (ListView)Parent;
                OpponentsPage.Opponents = (ObservableCollection<Opponents>)parent.ItemsSource;
                Opponents opponent = new Opponents();
                opponent = (Opponents)BindingContext;
                ObservableCollection<Matches> opponentsMatches = App.Database.GetOpponentsMatches(opponent.ID);
                foreach (Matches match in opponentsMatches)
                {
                    App.Database.DeleteMatch(match);
                }
                OpponentsPage.Opponents.Remove(opponent);
                App.Database.DeleteOpponent(opponent);

            };

            ContextActions.Add(deleteOpponent);
        }

    }

}