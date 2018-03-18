using ClaimReserving.Models;
using ClaimReserving.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ClaimReserving.Services
{
    public class PaymentService
    {
        private IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            this._paymentRepository = paymentRepository;
        }

        public IList<Payment> GetAllPayment()
        {
            return _paymentRepository.ReadAll();
        }
    }
}
