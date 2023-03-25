
using Xamarin.Forms;

namespace OpponentMatchesKeeper
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            Label lblDesc = new Label { Text = "Clicking this button will delete all opponents and matches" };

            Button btnDelete = new Button { Text = "Clear All Stored" };
            btnDelete.Clicked += (sender, e) =>
            {
                App.Database.DropTables();
            };

            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    lblDesc, btnDelete
                }
            };
        }
    }
}