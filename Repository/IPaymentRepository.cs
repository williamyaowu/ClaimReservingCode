using ClaimReserving.Models;
using System.Collections.Generic;

namespace ClaimReserving.Repository
{
    public interface IPaymentRepository
    {
        IList<Payment> ReadAll();
    }
}
