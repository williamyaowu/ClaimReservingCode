using ClaimReserving.Models;
using System.Collections.Generic;

namespace ClaimReserving.Services
{
    public interface IPaymentService
    {
        IList<Payment> GetAllPayment();
    }
}
