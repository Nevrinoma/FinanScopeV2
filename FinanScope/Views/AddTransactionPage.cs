using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinanScope.Views
{
    public partial class AddTransactionPage : ContentPage
    {
        public AddTransactionPage(MainViewModel viewModel, bool isExpense = false)
        {
            BindingContext = viewModel;

            var amountEntry = new Entry();
            amountEntry.SetBinding(Entry.TextProperty, nameof(viewModel.TransactionAmount));

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, nameof(viewModel.TransactionName));

            var confirmButton = new Button { Text = "Confirm" };
            confirmButton.SetBinding(Button.CommandProperty, nameof(viewModel.AddTransactionCommand));

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.SetBinding(Button.CommandProperty, nameof(viewModel.GoBackCommand));

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Amount" },
                    amountEntry,
                    new Label { Text = "Name" },
                    nameEntry,
                    confirmButton,
                    cancelButton
                }
            };

            if (isExpense)
            {
                confirmButton.TextColor = Color.Red;
                amountEntry.TextColor = Color.Red;
                amountEntry.Text = "-";
                stackLayout.Children.Add(new Label { Text = "Expense", TextColor = Color.Red });
            }
            else
            {
                confirmButton.TextColor = Color.Green;
                amountEntry.TextColor = Color.Green;
                
                stackLayout.Children.Add(new Label { Text = "Income", TextColor = Color.Green });
            }

            Content = stackLayout;
        }
    }
}
