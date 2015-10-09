using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Conventions
{

    /// <summary>
    /// Classe de convensão para especificar o nome do Sequence
    /// </summary>
    public class PrimaryKeyWithSequenceConvention : IIdConvention
    {
        public virtual void Apply(IIdentityInstance instance)
        {
            instance.Column(String.Concat("Id", instance.EntityType.Name));
            instance.GeneratedBy.Sequence(string.Format("SQ_{0}", instance.EntityType.Name.ToUpper()));
        }

    }
    }
