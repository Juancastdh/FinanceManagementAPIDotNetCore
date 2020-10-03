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

        public override bool Equals(object obj)
        {
            Category category = obj as Category;

            if (category == null)
            {
                return false;
            }

            if (category.Id != Id)
            {
                return false;
            }

            if(category.Name != Name)
            {
                return false;
            }

            if(category.Percentage != Percentage)
            {
                return false;
            }

            return true;
        }
    }
}
