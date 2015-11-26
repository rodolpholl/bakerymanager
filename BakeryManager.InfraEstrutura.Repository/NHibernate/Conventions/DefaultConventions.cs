using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions
{
    internal static class DefaultConventions
    {
        public static IConvention[] Get()
        {
            var defaultCoventions = new IConvention[]
                                        {
                                            new PrimaryKeyConvention(),
                                            new DefaultClassConvention(),
                                            new DefaultForeignKeyConvention(),
                                            //new DefaultManytoManyConvention(),
                                            new EnumConvention(),
                                            new DefaultOneToManyTableNameConvention(),
                                            new DefaultStringPropertyConvention(),
                                            new DefaultHasManyToManyConvention(),
                                            new BinaryColumnLengthConvention(),
                                            DefaultLazy.Always()
                                        };
            return defaultCoventions;
        }
    }
}
