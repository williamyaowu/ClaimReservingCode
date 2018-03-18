using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ClaimReserving.Services;
using System;

namespace ClaimReserving
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new AppConfig
            {
                InputFilePath = @".\Data.txt",
                OutputFilePath = @".\Result.txt"
            };

            if (args.Length == 2 && !string.IsNullOrWhiteSpace(args[0]))
            {
                config.InputFilePath = args[0];
            }


            if (args.Length == 2&&!string.IsNullOrWhiteSpace(args[1]))
            {
                config.OutputFilePath = args[1];
            }

            var container = new WindsorContainer();

            container.Register(Component.For<AppConfig>().Instance(config).LifestyleSingleton());
            container.Install(new ApplicationIocInstaller());

            using (var scope = container.BeginScope())
            {
                var paymentService = container.Resolve<IPaymentService>();
                var payments = paymentService.GetAllPayment();
                var culService = container.Resolve<ICumlativeClaimService>();
                var result = culService.CalculateCumlativeCalims(payments);
                culService.Save(result);
            }
        }


    }

    public class AppConfig
    {
        public string InputFilePath;

        public string OutputFilePath;
    }
}
