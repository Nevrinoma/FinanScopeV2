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
        public string Title { get; internal set; }
        public decimal Amount { get; internal set; }
        // Возможно, вам понадобятся дополнительные свойства, такие как дата создания, дата окончания и т.д.
    }

}
