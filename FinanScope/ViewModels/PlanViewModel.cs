using FinanScope.Models;
using FinanScope.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinanScope.ViewModels
{
    public class PlanViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService databaseService;

        private string planTitle;
        public string PlanTitle
        {
            get => planTitle;
            set
            {
                if (planTitle != value)
                {
                    planTitle = value;
                    OnPropertyChanged(nameof(PlanTitle));
                }
            }
        }

        private decimal planAmount;
        public decimal PlanAmount
        {
            get => planAmount;
            set
            {
                if (planAmount != value)
                {
                    planAmount = value;
                    OnPropertyChanged(nameof(PlanAmount));
                }
            }
        }

        private string planAmountText;
        public string PlanAmountText
        {
            get => PlanAmount.ToString();
            set
            {
                decimal amount;
                if (Decimal.TryParse(value, out amount))
                {
                    PlanAmount = amount;
                    OnPropertyChanged(nameof(PlanAmountText));
                }
                else
                {
                    // Показать ошибку пользователю или обработать иначе
                }
            }
        }



        private decimal monthlyAddition;
        public decimal MonthlyAddition
        {
            get => monthlyAddition;
            set
            {
                if (monthlyAddition != value)
                {
                    monthlyAddition = value;
                    OnPropertyChanged(nameof(MonthlyAddition));
                }
            }
        }

        public ObservableCollection<Plan> Plans { get; } = new ObservableCollection<Plan>();

        public Command SavePlanCommand { get; }
        public Command GoBackCommand { get; }

        public PlanViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;

            SavePlanCommand = new Command(async () => await SavePlanAsync());
            GoBackCommand = new Command(GoBack); // Используйте GoBack вместо GoBackAsync

            LoadPlans();
        }




        public async Task SavePlanAsync()
        {
            var plan = new Plan
            {
                Title = this.PlanTitle,
                Amount = this.PlanAmount,
                MonthlyAddition = this.MonthlyAddition
            };

            await databaseService.SavePlanAsync(plan);
            
            PlanTitle = string.Empty; // Очищаем значения
            PlanAmount = 0;
            MonthlyAddition = 0;
            LoadPlans(); // Обновите список планов после добавления нового
        }




        private async void SavePlan()
        {
            await SavePlanAsync();
            GoBack();
        }


        private async void GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void LoadPlans()
        {
            var plans = await databaseService.GetPlansAsync();
            Plans.Clear();

            foreach (var plan in plans)
            {
                Plans.Add(plan);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
