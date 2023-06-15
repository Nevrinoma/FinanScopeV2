using FinanScope.ViewModels;
using Xamarin.Forms;

namespace FinanScope.Views
{
    public partial class AddPlanPage : ContentPage
    {
        public PlanViewModel ViewModel { get; private set; }

        public AddPlanPage(PlanViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;

            var titleEntry = new Entry();
            titleEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Name));

            var amountEntry = new Entry();
            amountEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.TotalAmount));



            var monthlyAdditionEntry = new Entry();
            monthlyAdditionEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.MonthlyAddition));

            

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (s, e) =>
            {
                await ViewModel.SavePlanAsync();
                await Navigation.PopAsync();
            };


            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoBackCommand));

            Content = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Plan Title" },
                    titleEntry,
                    new Label { Text = "Plan Amount" },
                    amountEntry,
                    new Label { Text = "Monthly Addition" },
                    monthlyAdditionEntry,
                    
                    saveButton,
                    cancelButton
                }
            };
        }
    }

}