using ClaimReserving.Models;
using System.Collections.Generic;

namespace ClaimReserving.Repository
{
    public interface ICumlativeClaimDataRepository
    {
        bool Save(IList<CumulativeClaimData> records);
    }
}
