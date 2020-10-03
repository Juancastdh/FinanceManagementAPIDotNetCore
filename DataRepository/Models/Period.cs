using System;
using System.Collections.Generic;

namespace FinanceManagement.DataRepository.Models
{
    public partial class Period
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
