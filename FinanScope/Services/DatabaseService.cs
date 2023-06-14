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
            _database.CreateTableAsync<Plan>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
            _database.CreateTableAsync<Expense>().Wait();
            //_database.DeleteAllAsync<Budget>().Wait();
            //_database.DeleteAllAsync<Expense>().Wait();
            //_database.DeleteAllAsync<Plan>().Wait();
        }

        public Task<List<Budget>> GetBudgetsAsync()
        {
            return _database.Table<Budget>().ToListAsync();
        }

        public Task<int> SaveBudgetAsync(Budget budget)
        {
            return _database.InsertAsync(budget);
        }

        public Task<List<Expense>> GetTransactionsAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<int> SaveTransactionAsync(Expense expense)
        {
            return _database.InsertAsync(expense);
        }





        public Task<List<Plan>> GetPlansAsync()
        {
            return _database.Table<Plan>().ToListAsync();
        }

        public async Task<int> SavePlanAsync(Plan plan)
        {
            int result;
            if (plan.Id != 0)
            {
                result = await _database.UpdateAsync(plan);
            }
            else
            {
                result = await _database.InsertAsync(plan);
            }

            //System.Diagnostics.Debug.WriteLine($"Saved plan with ID {plan.Id}. Result: {result}");
            return result;
        }


        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _database.DeleteAsync(plan);
        }

        
    }

}
