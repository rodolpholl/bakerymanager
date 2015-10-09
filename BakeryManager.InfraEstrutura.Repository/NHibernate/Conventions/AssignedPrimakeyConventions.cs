using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    public static class AssignedPrimakeyConventions
    {
        public static IConvention[] Get()
        {
            var arConv = DefaultConventions.Get().Where(x => x.GetType().Name != typeof(PrimaryKeyConvention).Name).ToList();
            arConv.Add(new PrimaryKeyInformedConvention());
            return arConv.ToArray();
        }
    }
}
