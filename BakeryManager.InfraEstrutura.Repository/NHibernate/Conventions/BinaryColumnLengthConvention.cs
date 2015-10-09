﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;


namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{
    public class BinaryColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(byte[]));
        }
        

        public void Apply(IPropertyInstance instance)
        {
            instance.Length(int.MaxValue);
            //instance.CustomSqlType("varbinary(MAX)");
        }

    }
}
