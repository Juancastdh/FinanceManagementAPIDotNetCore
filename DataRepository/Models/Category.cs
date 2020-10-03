using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanceManagement.DataRepository.Models
{
    public partial class Category
    {
        public Category()
        {
            Financialtransactions = new HashSet<FinancialTransaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }

        [JsonIgnore]
        public virtual ICollection<FinancialTransaction> Financialtransactions { get; set; }

    }
}
