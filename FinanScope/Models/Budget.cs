using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanScope.Models
{
    [Table("Budgets")]
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
