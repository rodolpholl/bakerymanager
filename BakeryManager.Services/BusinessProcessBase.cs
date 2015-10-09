using BakeryManager.InfraEstrutura.Repository.NHibernate.Initializer;
using Spring.FluentContext;

namespace BakeryManager.Services
{
    public class BusinessProcessBase
    {

        public T GetObject<T>()
        {
            return FluentConfigurator.GetFluentConfigurator("BakeryManager.Entities",
                                                            "BakeryManager.Repositories",
                                                            "cnxBakeryManager").GetObject<T>();
        }

    }
}