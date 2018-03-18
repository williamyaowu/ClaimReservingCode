using ClaimReserving.Infrastructure.Persistant.Session;
using ClaimReserving.Repository;
using ClaimReserving.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserving
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentsession = new TextFilePeresistantSession(@"C:\\test\testData.txt");
            var paymentRepo = new PaymentRepository(paymentsession);

            var resultsession= new TextFilePeresistantSession(@"C:\\test\resultData.txt",true);
            var cumRepo = new CumlativeClaimDataRepository(resultsession);
            var generalRepo = new GeneralInfoRepository(resultsession);

            var paymentService = new PaymentService(paymentRepo);

            var payments = paymentService.GetAllPayment();


            var culService = new CumlativeClaimService(cumRepo, generalRepo);
            var result = culService.CalculateCumlativeCalims(payments);
            culService.Save(result);
        }

     
    }
}
