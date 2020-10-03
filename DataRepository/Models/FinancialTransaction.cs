using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanceManagement.DataRepository.Models
{
    public partial class FinancialTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }
        public bool IsExpense { get; set; }

        public virtual Category Category { get; set; }
    }
}
