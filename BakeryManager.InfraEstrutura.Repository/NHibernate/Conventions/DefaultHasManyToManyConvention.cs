using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    internal class DefaultHasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {

            if (instance.OtherSide == null)
            {
                instance.Inverse();
                instance.Cascade.AllDeleteOrphan();
            }
            else
                instance.Cascade.AllDeleteOrphan();
            
        }
    }
}
