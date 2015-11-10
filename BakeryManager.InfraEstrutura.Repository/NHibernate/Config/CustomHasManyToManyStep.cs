using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;


namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config
{
    public class CustomHasManyToManyStep : IAutomappingStep
    {
        private readonly IAutomappingConfiguration Configuration;
        private readonly IAutomappingStep DefaultManyToManyStep;

        public CustomHasManyToManyStep(IAutomappingConfiguration configuration, IAutomappingStep defaultManyToManyStep)
        {
            Configuration = configuration;
            DefaultManyToManyStep = defaultManyToManyStep;
        }

        #region Implementation of IAutomappingStep

        public bool ShouldMap(Member member)
        {
            if (DefaultManyToManyStep.ShouldMap(member))
            {
                return true;
            }

            //modify this statement to check for other attributes or conventions
            return member.MemberInfo.IsDefined(typeof(HasManyToManyAttribute), true);
        }

        public void Map(ClassMappingBase classMap, Member member)
        {
            if (DefaultManyToManyStep.ShouldMap(member))
            {
                DefaultManyToManyStep.Map(classMap, member);
                return;
            }

            var Collection = CreateManyToMany(classMap, member);
            classMap.AddCollection(Collection);
        }

        #endregion

        private CollectionMapping CreateManyToMany(ClassMappingBase classMap, Member member)
        {
            var ParentType = classMap.Type;
            var ChildType = member.PropertyType.GetGenericArguments()[0];

            var Collection = CollectionMapping.For(CollectionTypeResolver.Resolve(member));
            Collection.ContainingEntityType = ParentType;
            Collection.Set(x => x.Name, Layer.Defaults, member.Name);
            Collection.Set(x => x.Relationship, Layer.Defaults, CreateManyToMany(member, ParentType, ChildType));
            Collection.Set(x => x.ChildType, Layer.Defaults, ChildType);
            Collection.Member = member;

            SetDefaultAccess(member, Collection);
            SetKey(member, classMap, Collection);
            return Collection;
        }

        private void SetDefaultAccess(Member member, CollectionMapping mapping)
        {
            var ResolvedAccess = MemberAccessResolver.Resolve(member);

            if (ResolvedAccess != Access.Property && ResolvedAccess != Access.Unset)
            {
                mapping.Set(x => x.Access, Layer.Defaults, ResolvedAccess.ToString());
            }

            if (member.IsProperty && !member.CanWrite)
            {
                mapping.Set(x => x.Access, Layer.Defaults,
                            Configuration.GetAccessStrategyForReadOnlyProperty(member).ToString());
            }
        }

        private static ICollectionRelationshipMapping CreateManyToMany(Member member, Type parentType, Type childType)
        {
            var ColumnMapping = new ColumnMapping();
            ColumnMapping.Set(x => x.Name, Layer.Defaults, "Id_" + childType.Name);

            var Mapping = new ManyToManyMapping {ContainingEntityType = parentType};
            Mapping.Set(x => x.Class, Layer.Defaults, new FluentNHibernate.MappingModel.TypeReference(childType));
            Mapping.Set(x => x.ParentType, Layer.Defaults, parentType);
            Mapping.Set(x => x.ChildType, Layer.Defaults, childType);
            Mapping.AddColumn(Layer.Defaults, ColumnMapping);

            return Mapping;
        }

        private static void SetKey(Member property, ClassMappingBase classMap, CollectionMapping mapping)
        {
            var ColumnName = "Id_"+property.DeclaringType.Name;
            var ColumnMapping = new ColumnMapping();
            ColumnMapping.Set(x => x.Name, Layer.Defaults, ColumnName);

            var Key = new KeyMapping {ContainingEntityType = classMap.Type};
            Key.AddColumn(Layer.Defaults, ColumnMapping);

            mapping.Set(x => x.Key, Layer.Defaults, Key);

        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HasManyToManyAttribute : Attribute
    {
    }
}
