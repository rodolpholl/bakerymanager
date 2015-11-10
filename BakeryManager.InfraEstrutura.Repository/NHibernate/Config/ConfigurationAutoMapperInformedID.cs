using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions;
using FluentNHibernate.Automapping;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config
{
    public class ConfigurationAutoMapperInformedID : ConfigurationNHibernate
    {
        public override FluentNHibernate.Cfg.FluentConfiguration GetDefaultConfiguration()
        {
            return base.GetDefaultConfiguration()
                .Mappings(
                    m =>
                    m.AutoMappings.Add(AutoMap.Assembly(Assembly.Load(this.Parameters.DomainMap),
                                                        new AutoMapperConfig(this.Parameters.DomainMap))
                                           .Conventions.Add(AssignedPrimakeyConventions.Get())


                        ));
        }
    }
}
