using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Id;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    public class PrimaryKeyInformedConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.Custom<Assigned>();
            instance.Column(string.Format("Id{0}", instance.EntityType.Name));
        }
    }
}
