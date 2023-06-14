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

        private string planName;
        public string PlanName
        {
            get => planName;
            set
            {
                if (planName != value)
                {
                    planName = value;
                    OnPropertyChanged(nameof(PlanName));
                }
            }
        }

        private decimal planTotalAmount;
        public decimal PlanTotalAmount
        {
            get => planTotalAmount;
            set
            {
                if (planTotalAmount != value)
                {
                    planTotalAmount = value;
                    OnPropertyChanged(nameof(PlanTotalAmount));
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
            GoBackCommand = new Command(GoBack); 

            LoadPlans();
        }




        public async Task SavePlanAsync()
        {
            var plan = new Plan
            {
                Name = this.PlanName,
                TotalAmount = this.PlanTotalAmount,
                MonthlyAddition = this.MonthlyAddition
            };

            await databaseService.SavePlanAsync(plan);
            
            PlanName = string.Empty; // Очищаем значения
            PlanTotalAmount = 0;
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
            Plans.Clear();
            var plans = await databaseService.GetPlansAsync();
            

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
