using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions
{
    public class DefaultManytoManyConvention : ManyToManyTableNameConvention
    {

        protected override string GetBiDirectionalTableName(FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection, FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector otherSide)
        {
            return string.Format("TBL_{0}_{1}", collection.EntityType.Name, otherSide.EntityType.Name);
        }

        protected override string GetUniDirectionalTableName(FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection)
        {
            return string.Format("TBL_{0}_{1}", collection.EntityType.Name, collection.ChildType.Name);
        }

    }

}
