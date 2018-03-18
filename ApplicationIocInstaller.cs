using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ClaimReserving.Infrastructure.Persistant.Session;
using ClaimReserving.Repository;
using ClaimReserving.Services;

namespace ClaimReserving
{
    public class ApplicationIocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var config = container.Resolve<AppConfig>();

            container.Register(Component.For<IPersistantSession>().UsingFactoryMethod(() =>    
            {
                return new TextFilePeresistantSession(config.InputFilePath);
            }).Named("InPutSession").LifestyleScoped());

            container.Register(Component.For<IPaymentRepository>().UsingFactoryMethod((con) =>
            {
                var inputSession = con.Resolve<IPersistantSession>("InPutSession");
                return new PaymentRepository(inputSession);
            }).LifestyleScoped());

            container.Register(Component.For<IPersistantSession>().UsingFactoryMethod(() =>
            {
                return new TextFilePeresistantSession(config.OutputFilePath, true); ;
            }).Named("OutPutSession").LifestyleScoped());

            container.Register(Component.For<ICumlativeClaimDataRepository>().UsingFactoryMethod((con) =>
            {
                var outputSession = con.Resolve<IPersistantSession>("OutPutSession");
                return new CumlativeClaimDataRepository(outputSession);
            }).LifestyleScoped());
            container.Register(Component.For<IGeneralInfoRepository>().UsingFactoryMethod((con) =>
            {
                var outputSession = con.Resolve<IPersistantSession>("OutPutSession");
                return new GeneralInfoRepository(outputSession);
            }).LifestyleScoped());

            container.Register(Component.For<IPaymentService>().ImplementedBy<PaymentService>().LifestyleTransient());
            container.Register(Component.For<ICumlativeClaimService>().ImplementedBy<CumlativeClaimService>().LifestyleTransient());
        }
    }
}
