using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    public class DefaultStringPropertyConvention: IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.Length(300);
            instance.Nullable();
        }
    }
}
