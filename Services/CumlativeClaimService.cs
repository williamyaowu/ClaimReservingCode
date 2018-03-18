using ClaimReserving.Models;
using ClaimReserving.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserving.Services
{
    public class CumlativeClaimService
    {
        private ICumlativeClaimDataRepository _cumlativeClaimDataRepository;
        private IGeneralInfoRepository _generalInfoRepository;
        public CumlativeClaimService(ICumlativeClaimDataRepository cumRepo,
                                      IGeneralInfoRepository generalRepo)
        {
            this._cumlativeClaimDataRepository = cumRepo;
            this._generalInfoRepository = generalRepo;
        }
        public IList<CumulativeClaimData> CalculateCumlativeCalims(IList<Payment> payments)
        {
            int minOriginalYear, maxOrigianlYear, maxDevYear;

            var products = this.getProducts(payments);
            this.getOriginalYearRange(payments, out minOriginalYear, out maxOrigianlYear);

            maxDevYear = this.getMaxDevelopmentYear(payments);

            var maxDevYearNum = maxDevYear - minOriginalYear + 1;

            var resultCollection = new List<CumulativeClaimData>(); 
            foreach (var pro in products)
            {
                var data = this.getClaimDataRows(pro, minOriginalYear, maxOrigianlYear, maxDevYearNum, payments);

                resultCollection.Add(new CumulativeClaimData(pro, data));
            }

            return resultCollection;
        }

        public bool Save(IList<CumulativeClaimData> records)
        {
            var minOriginalYear = records.SelectMany(p => p.Data).Select(p => p.OriginalYear).Min();
            var maxDevYearNum= records.SelectMany(p => p.Data).Select(p => p.DevelopmentYearNumber).Max();

            var title = string.Format("{0}, {1}", minOriginalYear, maxDevYearNum);

            if (!_generalInfoRepository.Write(title)) return false;

            if (!_cumlativeClaimDataRepository.Save(records)) return false;

            return true;
        }

        private void getOriginalYearRange(IList<Payment> payments, out int minYear, out int maxYear)
        {
            minYear = 0;
            maxYear = 0;

            if (payments == null || payments.Count() == 0) return;

            minYear = payments.Select(p => p.OriginalYear).Min();
            maxYear = payments.Select(p => p.OriginalYear).Max();
        }


        private int getMaxDevelopmentYear(IList<Payment> payments)
        {
            if (payments == null || payments.Count() == 0) return 0;

            return payments.Select(p => p.DevelopmentYear).Max();
        }

        private IList<string> getProducts(IList<Payment> payments)
        {
            if (payments == null || payments.Count() == 0) return new List<string>();

            return payments.Select(p => p.Product)
                           .Distinct()
                           .ToList();
        }

        private IList<CumulativeClaimDataRow> getClaimDataRows(string pro, int minOriginalYear,int maxOrigianlYear, int maxDevYearNum, IList<Payment> payments)
        {
            var productClaimDataRowList = new List<CumulativeClaimDataRow>();
            for (var oriYear = minOriginalYear; oriYear <= maxOrigianlYear; oriYear++)
            {
                var devYearBouldle = minOriginalYear + maxDevYearNum - oriYear;
                for (var devYearNumber = 1; devYearNumber <= devYearBouldle; devYearNumber++)
                {
                    var amount = payments.Where(p => p.Product == pro &&
                                                p.OriginalYear == oriYear &&
                                                p.DevelopmentYearNumber <= devYearNumber).
                                        Sum(p => p.Amount);
                    productClaimDataRowList.Add(new CumulativeClaimDataRow(oriYear, devYearNumber, amount));
                }
            }
            return productClaimDataRowList;
        }

    }
}
