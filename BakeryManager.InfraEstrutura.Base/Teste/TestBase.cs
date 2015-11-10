using BakeryManager.InfraEstrutura.Base.Teste.Contracts;

namespace BakeryManager.InfraEstrutura.Base.Teste
{
    public abstract class TestBase : ITestBase
    {
        // Code that runs on application startup
        protected TestBase()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
        }
            
    }
}
