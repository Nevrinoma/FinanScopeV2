using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanScope.Models
{
    public class Plan
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MonthlyAddition { get; set; }
        
    }

}
