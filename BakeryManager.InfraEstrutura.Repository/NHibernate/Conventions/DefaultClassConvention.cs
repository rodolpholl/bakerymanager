using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions
{
    public class DefaultClassConvention : IClassConvention
    {

        public void Apply(IClassInstance instance)
        {
            instance.Table(string.Format("TBL_{0}", instance.EntityType.Name.ToUpper()));
            instance.LazyLoad();
        }
    }
}
