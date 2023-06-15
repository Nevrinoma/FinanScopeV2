using FinanScope.Models;
using FinanScope.Services;
using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FinanScope.Views
{
    public class StocksPage : ContentPage
    {
        public StockViewModel ViewModel { get; private set; }
        public StocksPage(StockViewModel viewModel)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = viewModel;
            BindingContext = ViewModel;

            var Name = new Label();
            Name.TextColor = Color.Black;
            Name.Text = "Name:";

            var amountText = new Label();
            amountText.TextColor = Color.Black;
            amountText.Text = "Amount:";

            var Price = new Label();
            Price.TextColor = Color.Black;
            Price.Text = "Price:";

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(Name, 0, 0);
            grid.Children.Add(amountText, 1, 0);
            grid.Children.Add(Price, 2, 0);

            var stocksList = new ListView();
            stocksList.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.Stocks));
            stocksList.ItemTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label();
                nameLabel.TextColor = Color.Black;
                nameLabel.SetBinding(Label.TextProperty, nameof(Stocks.Name));

                var amountLabel = new Label();
                amountLabel.TextColor = Color.Black;
                amountLabel.SetBinding(Label.TextProperty, nameof(Stocks.Amount));

                var priceLabel = new Label();
                priceLabel.TextColor = Color.Black;
                priceLabel.SetBinding(Label.TextProperty, new Binding(nameof(Stocks.Amount), converter: new MultiplyConverter()));


                var grid2 = new Grid();
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                grid2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                grid2.Children.Add(nameLabel, 0, 0);
                grid2.Children.Add(amountLabel, 1, 0);
                grid2.Children.Add(priceLabel, 2, 0);

                return new ViewCell
                {
                    View = grid2
                };
            });

            var addButton = new Button { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddStocksPage(ViewModel));
            };


            Appearing += (sender, e) =>
            {
                ViewModel.LoadStocks();
            };

            Content = new StackLayout
            {
                Children =
                {
                        grid,
                    stocksList,
                    addButton
                }
            };

            //string symbol = stocksAPI.GetStocks("AAPL");

            // new Label { Text = "Apple price = " + symbol }

            //Console.WriteLine($"Open {GetStocks("AAPL").Close}");

        }
    }

    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal amount = (decimal)value;
            decimal stockPrice = Math.Round(stocksAPI.GetStocks("AAPL"), 2); // suda

            decimal result = amount * stockPrice;
            return result.ToString("0.00"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}