using FinanScope.Models;
using FinanScope.Services;
using FinanScope.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FinanScope.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Expense> Expenses { get; set; }
        public ObservableCollection<Expense> Transactions { get; } = new ObservableCollection<Expense>();
        private readonly DatabaseService databaseService;


        private decimal _totalAmount;
        public string TransactionName { get; set; }
        public decimal TransactionAmount { get; set; }

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

        


        public Command IncreaseBudgetCommand { get; }
        public Command DecreaseBudgetCommand { get; }
        public Command AddTransactionCommand { get; }
        public Command GoBackCommand { get; }


        public MainViewModel(Services.DatabaseService databaseService)
        {
            this.databaseService = databaseService;

            IncreaseBudgetCommand = new Command(IncreaseBudget);
            DecreaseBudgetCommand = new Command(DecreaseBudget);
            AddTransactionCommand = new Command(AddTransaction);
            GoBackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            LoadTransactions();


        }
        private async void LoadTransactions()
        {
            var transactions = await databaseService.GetTransactionsAsync();
            Transactions.Clear();

            
            var sortedTransactions = transactions.OrderByDescending(t => t.Date);

            foreach (var transaction in sortedTransactions)
            {
                Transactions.Add(transaction);
            }

            
            TotalAmount = Transactions.Sum(transaction => transaction.Amount);
        }




        private async void IncreaseBudget()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddTransactionPage(this, isExpense: false));

        }
        private async void AddTransaction()
        {
            
            var newTransaction = new Expense
            {
                Name = TransactionName,
                Amount = TransactionAmount,
                Date = DateTime.Now
            };

            
            Transactions.Insert(0, newTransaction);

            
            TotalAmount += TransactionAmount;

            
            await databaseService.SaveTransactionAsync(newTransaction);

            
            TransactionName = string.Empty;
            TransactionAmount = 0;

            
            OnPropertyChanged(nameof(Transactions));
            OnPropertyChanged(nameof(TotalAmount));
        }


        private async void DecreaseBudget()
        {
            
            var newTransaction = new Expense
            {
                Name = TransactionName,
                Amount = -TransactionAmount, 
                Date = DateTime.Now
            };

            
            Transactions.Insert(0, newTransaction);

            
            TotalAmount -= TransactionAmount;

            
            await databaseService.SaveTransactionAsync(newTransaction);

            
            TransactionName = string.Empty;
            TransactionAmount = 0;

            
            OnPropertyChanged(nameof(Transactions));
            OnPropertyChanged(nameof(TotalAmount));
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
