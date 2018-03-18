using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserving.Models
{
    public class CumulativeClaimData
    {
        public CumulativeClaimData()
        {
            this.Data= new List<CumulativeClaimDataRow>();
        }

        public CumulativeClaimData(string pro, IList<CumulativeClaimDataRow> data)
        {
            this.Product = pro;
            this.Data = data;    
        }

        public string Product { get; set; }

        public IList<CumulativeClaimDataRow> Data { get; set; }
    }

    public class CumulativeClaimDataRow
    {
        public CumulativeClaimDataRow()
        {
        }

        public CumulativeClaimDataRow(int oriYear, int devYearNum, decimal amount)
        {
            this.OriginalYear = oriYear;
            this.DevelopmentYearNumber = devYearNum;
            this.Amount = amount;
        }

        public int OriginalYear { get; set; }

        public int DevelopmentYearNumber { get; set; }

        public decimal Amount { get; set; }
    }
}
