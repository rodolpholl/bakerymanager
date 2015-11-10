using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {

        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.Identity();
            instance.Column(string.Format("Id{0}",instance.EntityType.Name));
        }
    }
}
