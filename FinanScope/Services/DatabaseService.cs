using FinanScope.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanScope.Services
{
    public class DatabaseService
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Budget>().Wait();
            _database.CreateTableAsync<Expense>().Wait();
            //_database.DeleteAllAsync<Budget>().Wait();
            //_database.DeleteAllAsync<Expense>().Wait();
        }

        public Task<List<Budget>> GetBudgetsAsync()
        {
            return _database.Table<Budget>().ToListAsync();
        }

        public Task<int> SaveBudgetAsync(Budget budget)
        {
            return _database.InsertAsync(budget);
        }

        // Do the same for Expenses

        public Task<List<Expense>> GetTransactionsAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<int> SaveTransactionAsync(Expense expense)
        {
            return _database.InsertAsync(expense);
        }

    }

}
