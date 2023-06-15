using FinanScope.Models;
using FinanScope.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinanScope.ViewModels
{
    public class PlanViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService databaseService;

        private string planName;
        public string Name
        {
            get => planName;
            set
            {
                if (planName != value)
                {
                    planName = value;
                    OnPropertyChanged();
                }
            }
        }
        private decimal planTotalAmount;
        public decimal TotalAmount
        {
            get => planTotalAmount;
            set
            {
                if (planTotalAmount != value)
                {
                    planTotalAmount = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                Name = this.Name,
                TotalAmount = this.TotalAmount,
                MonthlyAddition = this.MonthlyAddition
            };

            await databaseService.SavePlansAsync(plan);

            Name = string.Empty; 
            TotalAmount = 0;
            MonthlyAddition = 0;
            LoadPlans(); 
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
