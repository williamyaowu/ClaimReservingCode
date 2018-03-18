using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserving.Models
{
    public class Payment
    {
        public Payment(string pro, int oriYear,int devYear,decimal amount)
        {
            this.Product = pro;
            this.OriginalYear = oriYear;
            this.DevelopmentYear = devYear;
            this.Amount = amount;
        }

        public string Product { get; set; }

        public int OriginalYear { get; set; }

        public int DevelopmentYear { get; set; }

        public decimal Amount { get; set; }

        public int DevelopmentYearNumber
        {
            get
            {
                return this.DevelopmentYear - this.OriginalYear + 1;
            }
        }
    }
}
