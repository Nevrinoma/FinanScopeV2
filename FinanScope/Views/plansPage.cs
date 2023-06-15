using FinanScope.Models;
using FinanScope.Services;
using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using Xamarin.Forms;

namespace FinanScope.Views
{
    public partial class PlansPage : ContentPage
    {
        public PlanViewModel ViewModel { get; private set; }

        public PlansPage(PlanViewModel viewModel)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = viewModel;
            BindingContext = ViewModel;

            var titleText = new Label();
            titleText.TextColor = Color.Black;
            titleText.Text = "Name:";

            var amountText = new Label();
            amountText.TextColor = Color.Black;
            amountText.Text = "TotalAmount:";

            var monthlyAdditionText = new Label();
            monthlyAdditionText.TextColor = Color.Black;
            monthlyAdditionText.Text = "MonthlyAddition:";

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(titleText, 0, 0);
            grid.Children.Add(amountText, 1, 0);
            grid.Children.Add(monthlyAdditionText, 2, 0);

            var plansList = new ListView();
            plansList.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.Plans));
            plansList.ItemTemplate = new DataTemplate(() =>
            {
                var titleLabel = new Label();
                titleLabel.TextColor = Color.Black;
                titleLabel.SetBinding(Label.TextProperty, nameof(Plan.Name));

                var amountLabel = new Label();
                amountLabel.TextColor = Color.Black;
                amountLabel.SetBinding(Label.TextProperty, nameof(Plan.TotalAmount));

                var monthlyAdditionLabel = new Label();
                monthlyAdditionLabel.TextColor = Color.Black;
                monthlyAdditionLabel.SetBinding(Label.TextProperty, nameof(Plan.MonthlyAddition));

                var grid2 = new Grid();
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid2.Children.Add(titleLabel, 0, 0);
                grid2.Children.Add(amountLabel, 1, 0);
                grid2.Children.Add(monthlyAdditionLabel, 2, 0);

                return new ViewCell
                {
                    View = grid2
                };
            });

            var addButton = new Button { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddPlanPage(ViewModel));
            };

            
            Appearing += (sender, e) =>
            {
                ViewModel.LoadPlans();
            };

            Content = new StackLayout
            {
                Children =
                {
                        grid,
                    plansList,
                    addButton
                }
            };
        }
    }

}
