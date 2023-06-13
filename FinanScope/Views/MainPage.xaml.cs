using FinanScope.Models;
using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinanScope.Views
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel { get; private set; }

        public MainPage(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);

            var addButton = new Button { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddTransactionPage(ViewModel, isExpense: false));
            };

            var subtractButton = new Button { Text = "Subtract" };
            subtractButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddTransactionPage(ViewModel, isExpense: true));
            };

            var budgetLabel = new Label();
            budgetLabel.SetBinding(Label.TextProperty, nameof(ViewModel.TotalAmount));
            budgetLabel.FontSize = 36;
            budgetLabel.HorizontalOptions = LayoutOptions.Center;

            var expensesList = new ListView();
            expensesList.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.Transactions));
            expensesList.ItemTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, nameof(Expense.Name));

                var amountLabel = new Label();
                amountLabel.SetBinding(Label.TextProperty, nameof(Expense.Amount));
                amountLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(Expense.Amount), converter: new ColorConverter()));

                var dateLabel = new Label();
                dateLabel.SetBinding(Label.TextProperty, nameof(Expense.Date), converter: new DateTimeConverter());
                dateLabel.FontSize = 12;
                dateLabel.TextColor = Color.Gray;

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Children = { nameLabel, amountLabel, dateLabel }
                    }
                };
            });

            Content = new StackLayout
            {
                Margin = 5,
                Children =
                {
                    budgetLabel,
                    expensesList,
                    addButton,
                    subtractButton
                }
            };
        }

        public MainPage()
        {
        }

        public class ColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is decimal amount)
                {
                    if (amount >= 0)
                    {
                        return Color.Green;
                    }
                    else
                    {
                        return Color.Red;
                    }
                }
                return Color.Default;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class DateTimeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime dateTime)
                {
                    return dateTime.ToString("dd.MM.yyyy HH:mm", culture);
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }
}
