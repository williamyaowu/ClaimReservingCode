using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserving.Models;
using ClaimReserving.Infrastructure.Persistant.Session;
using System.Linq;

namespace ClaimReserving.Repository
{
    public class PaymentRepository
        : IPaymentRepository
    {
        private IPersistantSession _session;

        public PaymentRepository(IPersistantSession session)
        {
            this._session = session;
        }

        public IList<Payment> ReadAll()
        {
            var records = _session.ReadAll();
            var resultList = new List<Payment>();

            foreach(var re in records)
            {
                Payment result;
                if(this.deserialize(re.ToString(),out result))
                {
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        private bool deserialize(string data, out Payment result)
        {
  
                result = null;
                var splitStrings = data.Split(',').Select(p => p.Trim()).ToArray();

                if (splitStrings == null || splitStrings.Length < 4)
                {
                 
                    return false;
                }

                string product = null; 
                if (string.IsNullOrWhiteSpace(splitStrings[0]))
                {
                    return false;
                }
                else
                {
                    product = splitStrings[0];
                }

                int originalYear;
                if(string.IsNullOrWhiteSpace(splitStrings[1])
                    ||!int.TryParse(splitStrings[1],out originalYear))
                {
                    return false;
                }

                int developmentYear;
                if (string.IsNullOrWhiteSpace(splitStrings[2])
                    ||!int.TryParse(splitStrings[2], out developmentYear))
                {
                    return false;
                }

                decimal amount;
                if (string.IsNullOrWhiteSpace(splitStrings[3])
                    ||!decimal.TryParse(splitStrings[3], out amount))
                {
                    return false;
                }

                result = new Payment(product, originalYear, developmentYear, amount);
                return true;
        }

    }
}
