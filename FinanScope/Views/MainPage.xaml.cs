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
    public class MainPage : ContentPage
    {
        public MainViewModel ViewModel { get; private set; }

        public MainPage(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;

            // Кнопка для добавления суммы в бюджет
            var addButton = new Button { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                // Здесь логика для добавления суммы в бюджет...
            };

            // Кнопка для вычитания суммы из бюджета
            var subtractButton = new Button { Text = "Subtract" };
            subtractButton.Clicked += async (s, e) =>
            {
                // Здесь логика для вычитания суммы из бюджета...
            };

            // Здесь будет отображаться общий бюджет
            var budgetLabel = new Label();
            budgetLabel.SetBinding(Label.TextProperty, nameof(ViewModel.TotalAmount));

            // Здесь будут отображаться расходы
            var expensesList = new ListView();
            expensesList.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.Expenses));

            // Создание главного макета страницы
            Content = new StackLayout
            {
                Children =
                {
                    addButton,
                    subtractButton,
                    budgetLabel,
                    expensesList
                }
            };
        }
    }
}