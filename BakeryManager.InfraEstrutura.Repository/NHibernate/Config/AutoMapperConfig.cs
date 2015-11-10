using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config
{
    /// <summary>(type.BaseType == null 
    /// Classe de Configuração do AutoMapeamento.
    /// </summary>
    internal class AutoMapperConfig : DefaultAutomappingConfiguration
    {


        /// <summary>
        /// Domíno a ser mapeado automatiacmene
        /// </summary>
        private string DomainMap { get; set; }

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="DomainMap">Domínio a ser mapeado.</param>
        public AutoMapperConfig(string DomainMap)
        {
            this.DomainMap = DomainMap;
        }

        /// <summary>
        /// Indica qual átributo será mapeado como Id.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public override bool IsId(FluentNHibernate.Member member)
        {
            return member.Name == string.Concat("Id", member.DeclaringType.Name);
        }

        /// <summary>
        /// Indica quais entidades poderão ser mapeadas.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool ShouldMap(Type type)
        {

            Console.WriteLine("Tipo Pai: " + type.BaseType.Name);

            return type.Namespace == DomainMap && 
                   type.Name != "EntityBase" && 
                   type.IsClass &&
                   (type.BaseType == null || !type.GetInterfaces().Any(x => x.Name == "IDomainNonMapper"));
        }

        public override IEnumerable<IAutomappingStep> GetMappingSteps(AutoMapper mapper, IConventionFinder conventionFinder)
        {
            return base.GetMappingSteps(mapper, conventionFinder).Select(GetDecoratedStep);
        }

        IAutomappingStep GetDecoratedStep(IAutomappingStep step)
        {
            if (step is HasManyToManyStep)
            {
                return new CustomHasManyToManyStep(this, step);
            }

            return step;
        }



    }
}
