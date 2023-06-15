using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FinanScope.Views
{
    public class AddStocksPage : ContentPage
    {
        public StockViewModel ViewModel { get; private set; }

        public AddStocksPage(StockViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;

            var NameEntry = new Entry();
            NameEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Name));

            var symbolEntry = new Entry();
            symbolEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Symbol));

            var amountEntry = new Entry();
            amountEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Amount));

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (s, e) =>
            {
                await ViewModel.SaveStockAsync();
                await Navigation.PopAsync();
            };


            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoBackCommand));

            Content = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Name" },
                    NameEntry,
                    new Label { Text = "Symbol" },
                    symbolEntry,
                    new Label { Text = "Amount" },
                    amountEntry,
                    saveButton,
                    cancelButton
                }
            };
        }
    }
}