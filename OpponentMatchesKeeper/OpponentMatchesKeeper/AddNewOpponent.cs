using Xamarin.Forms;

namespace OpponentMatchesKeeper
{
    public class AddNewOpponent : ContentPage
    {
        public AddNewOpponent()
        {
            Opponents opponent = new Opponents();

            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.Center };

            // Table to contain our "form"

            TableView table = new TableView { Intent = TableIntent.Form };

            // cells for each property
            EntryCell eFirstName = new EntryCell { Label = "First Name" };
            EntryCell eLastName = new EntryCell { Label = "Last Name" };
            EntryCell eAddress = new EntryCell { Label = "Address" };
            EntryCell ePhone = new EntryCell { Label = "Phone" };
            EntryCell eEmail = new EntryCell { Label = "Email" };

            TableSection section = new TableSection("Add New Opponent")
            {
                eFirstName, eLastName, eAddress, ePhone, eEmail
            };

            table.Root = new TableRoot { section };

            Button btnSave = new Button { Text = "Save" };
            btnSave.Clicked += (sender, e) =>
            {

                opponent.FirstName = eFirstName.Text;
                opponent.LastName = eLastName.Text;
                opponent.Address = eAddress.Text;
                opponent.Phone = ePhone.Text;
                opponent.Email = eEmail.Text;
                App.Database.SaveOpponent(opponent);
                OpponentsPage.Opponents.Add(opponent);
                Navigation.PopAsync();

            };

            layout.Children.Add(table);
            layout.Children.Add(btnSave);
            Content = layout;
        }
    }
}