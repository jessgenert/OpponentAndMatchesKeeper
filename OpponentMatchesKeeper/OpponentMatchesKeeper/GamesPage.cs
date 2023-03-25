using Xamarin.Forms;

namespace OpponentMatchesKeeper
{
    public class GamesPage : ContentPage
    {

        public GamesPage()
        {
            Title = "Prev Title";

            ListView listViewGames = new ListView
            {
                ItemsSource = App.Database.GetGames(),
                ItemTemplate = new DataTemplate(typeof(GamesCell)),
                RowHeight = GamesCell.RowHeight

            };
            Content = listViewGames;
        }


        public class GamesCell : ViewCell
        {
            public const int RowHeight = 80;
            private Label lblMatchNumber = new Label();
            public GamesCell()
            {
                Label lblName = new Label { };
                lblName.FontAttributes = FontAttributes.Bold;
                lblName.SetBinding(Label.TextProperty, "GameName");

                Label lblDesc = new Label { };
                lblDesc.SetBinding(Label.TextProperty, "Description");

                Label lblMatchCount = new Label { Text = "# Matches: " };

                Label lblRating = new Label { HorizontalOptions = LayoutOptions.EndAndExpand };
                lblRating.SetBinding(Label.TextProperty, "Rating");

                View = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {

                        new StackLayout
                        {
                            Children = { lblName }
                        },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Start,
                            Children = { lblDesc, lblRating }
                        },
                        new StackLayout
                        {
                            Orientation= StackOrientation.Horizontal,
                            Children = { lblMatchCount, lblMatchNumber }
                        }

                    }
                };
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                Games game = (Games)BindingContext;
                lblMatchNumber.Text = App.Database.GetGameCount(game).ToString();
            }
        }
    }

}