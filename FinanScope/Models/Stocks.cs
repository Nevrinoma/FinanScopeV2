using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanScope.Models
{
    [Table("Stocks")]
    public class Stocks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

}
