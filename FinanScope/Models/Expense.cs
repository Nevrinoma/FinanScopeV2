using System;
using System.Collections.Generic;
using System.Text;

namespace FinanScope.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
