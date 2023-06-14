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
                titleLabel.SetBinding(Label.TextProperty, "Title");

                var amountLabel = new Label();
                amountLabel.SetBinding(Label.TextProperty, "PlanAmount");

                var monthlyAdditionLabel = new Label();
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
