using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BakeryManager.InfraEstrutura.Repository.NHibernate;
using BakeryManager.InfraEstrutura.Repository.NHibernate.Config;

using Spring.Context;
using Spring.FluentContext;
using System.Configuration;
using BakeryManager.InfraEstrutura.Repository;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Initializer
{
    public static class FluentConfigurator
    {

        public static IApplicationContext  GetFluentConfigurator(string BusinessEntityAssembly, string BusinessManagementAssembly, string ConnectionString)
        {
            var ctx = new FluentApplicationContext();

            //Configurando os parâmetros
            ctx.RegisterDefault<RepositoryParameter>()

                .BindProperty(x => x.AuditoryEnabled).ToValue(false)

                .BindProperty(x => x.ConnectionString)
                .ToValue(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString)

                .BindProperty(x => x.DomainMap).ToValue(BusinessEntityAssembly)
                .BindProperty(x => x.GenerateSchema).ToValue(true)
                .BindProperty(x => x.ORMUtilizado).ToValue(ORM.NHibernate).GetReference();

           //Registrando a Sessão
            ctx.RegisterDefault<ConfigurationAutoMapper>()
                    .BindProperty(x => x.Parameters).ToRegisteredDefaultOf<RepositoryParameter>();

            ctx.RegisterDefault<SessionFactorySqlServer2008NHibernate>()
                .BindProperty(x => x.Config)
                .ToRegisteredDefaultOf<ConfigurationAutoMapper>()
                .GetReference();

           

            //Criando estrutura genérica para atender o requisito acima.
            //here
            var assBm = Assembly.Load(BusinessManagementAssembly);
            var assBe = Assembly.Load(BusinessEntityAssembly);

            foreach (var bm in assBm.ExportedTypes)
            {

                var be = assBe.ExportedTypes.FirstOrDefault(x => x.Name.Equals(bm.Name.Substring(0, bm.Name.Length - 2)));

                var model = Activator.CreateInstance(typeof(RepositoryBaseNHibernate<>).MakeGenericType(be));

                var registDefault = ctx.GetType().GetMethod("RegisterDefault").MakeGenericMethod(model.GetType()).Invoke(ctx, null);
                

                var obj = registDefault.GetType()
                        .GetMethod("BindPropertyNamed")
                        .MakeGenericMethod(typeof (SessionFactorySqlServer2008NHibernate))
                        .Invoke(registDefault, new[] { "SessionFactory" });

                obj.GetType().GetMethod("ToRegisteredDefault").Invoke(obj, null);



                var regdefault = ctx.GetType().GetMethod("RegisterDefault").MakeGenericMethod(bm).Invoke(ctx,null);

                var buildPrperty =
                    regdefault.GetType()
                        .GetMethod("BindPropertyNamed")
                        .MakeGenericMethod(model.GetType())
                        .Invoke(regdefault, new[] {"Repository"});

                buildPrperty.GetType().GetMethod("ToRegisteredDefault").Invoke(buildPrperty, null);
            } 
            //here

            return ctx;

        }

        
    }
}
