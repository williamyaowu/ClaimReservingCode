using ClaimReserving.Models;
using System.Collections.Generic;

namespace ClaimReserving.Services
{
    public interface ICumlativeClaimService
    {
         IList<CumulativeClaimData> CalculateCumlativeCalims(IList<Payment> payments);
        bool Save(IList<CumulativeClaimData> records);
    }
}
