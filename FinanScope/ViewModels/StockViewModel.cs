using FinanScope.Models;
using FinanScope.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinanScope.ViewModels
{
    public class StockViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService databaseService;
        private string stockName;
        public string Name
        {
            get => stockName;
            set
            {
                if (stockName != value)
                {
                    stockName = value;
                    OnPropertyChanged();
                }
            }
        }
        private decimal stockAmount;
        public decimal Amount
        {
            get => stockAmount;
            set
            {
                if (stockAmount != value)
                {
                    stockAmount = value;
                    OnPropertyChanged();
                }
            }
        }
        private string symbol;
        public string Symbol
        {
            get => symbol;
            set
            {
                if (symbol != value)
                {
                    symbol = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Stocks> Stocks { get; } = new ObservableCollection<Stocks>();
        public Command SaveStocksCommand { get; }
        public Command GoBackCommand { get; }
        public StockViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;

            SaveStocksCommand = new Command(async () => await SaveStockAsync());
            GoBackCommand = new Command(GoBack);
            LoadStocks();
        }
        public async Task SaveStockAsync()
        {
            var stock = new Stocks
            {
                Name = this.Name,
                Amount = this.Amount,
                Symbol = this.Symbol
            };

            await databaseService.SaveStocksAsync(stock);

            Name = string.Empty; // Очищаем значения
            Amount = 0;
            Symbol = string.Empty;
            LoadStocks(); // Обновите список планов после добавления нового
        }
        private async void SaveStock()
        {
            await SaveStockAsync();
            GoBack();
        }
        private async void GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async void LoadStocks()
        {
            Stocks.Clear();
            var stocks = await databaseService.GetStocksAsync();
            foreach (var stock in stocks)
            {
                Stocks.Add(stock);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
