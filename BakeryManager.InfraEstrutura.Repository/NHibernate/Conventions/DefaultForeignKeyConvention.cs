using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    public class DefaultForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            return property == null
                       ? string.Format("ID_{0}", type.Name)
                       : string.Format("ID_{0}", property.Name);
        }
    }
}
