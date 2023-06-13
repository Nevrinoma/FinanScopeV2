using FinanScope.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FinanScope.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged(nameof(TotalAmount));
                }
            }
        }

        public ObservableCollection<Expense> Transactions { get; } = new ObservableCollection<Expense>();

        public Command IncreaseBudgetCommand { get; }
        public Command DecreaseBudgetCommand { get; }

        public MainViewModel(Services.DatabaseService databaseService)
        {
            IncreaseBudgetCommand = new Command(IncreaseBudget);
            DecreaseBudgetCommand = new Command(DecreaseBudget);
        }

        private void IncreaseBudget()
        {
            // Add your logic to increase the budget
        }

        private void DecreaseBudget()
        {
            // Add your logic to decrease the budget
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
