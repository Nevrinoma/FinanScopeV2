using FinanScope.Models;
using FinanScope.Services;
using FinanScope.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var plansList = new ListView();
            plansList.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.Plans));
            plansList.ItemTemplate = new DataTemplate(() =>
            {
                var titleLabel = new Label();
                titleLabel.TextColor = Color.Black;
                titleLabel.SetBinding(Label.TextProperty,nameof(Plan.Name));

                var amountLabel = new Label();
                amountLabel.TextColor = Color.Black;
                amountLabel.SetBinding(Label.TextProperty, nameof(Plan.TotalAmount));

                var monthlyAdditionLabel = new Label();
                monthlyAdditionLabel.TextColor = Color.Black;
                monthlyAdditionLabel.SetBinding(Label.TextProperty, "MonthlyAddition");

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Children = { titleLabel, amountLabel, monthlyAdditionLabel }
                    }
                };
            });



            var addButton = new Button { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddPlanPage(ViewModel));
            };

            // Добавьте следующий код здесь
            Appearing += (sender, e) =>
            {
                ViewModel.LoadPlans();
            };

            Content = new StackLayout
            {
                Children =
                {
                plansList,
                addButton
                }
            };
        }
    }

}
