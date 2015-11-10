using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.InfraEstrutura.Helpers.Security;
using NHibernate.Event;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config.Auditory
{
    internal class AuditInsertEventListener : AuditEventListenerBase, IPostInsertEventListener
    {

        public AuditInsertEventListener()
        {
            Console.Write("Auditoria Instert Ativada.");
        }

        protected static string GetValueByProperty(PropertyInfo property, object entity)
        {

            if (!property.PropertyType.IsPrimitive && !property.PropertyType.Namespace.Contains("System"))
            {


                return property.GetValue(entity).ToString();
          

            }
            else
                return property.GetValue(entity) == null ? string.Empty : property.GetValue(entity).ToString();

        }

        public void OnPostInsert(PostInsertEvent e)
        {
            if ( Params.SkipTrakingList.Contains(e.Entity.GetType().FullName)
                || e.Entity.GetType().Name.Equals("AuditRegister"))
                return;


            foreach (var p in e.Entity.GetType().GetProperties())
            {
                var aud = new AuditRegister()
                {
          
                    ColumnName = p.Name,
                    ContextId = long.Parse(e.Id.ToString()),
                    NewValue = p.GetValue(e.Entity) == null ? string.Empty : p.GetValue(e.Entity).ToString(),
                    OperationDate = DateTime.Now,
                    OperationType = AuditOperationType.Insert,
                    ColumnTitle = AnnotationsAttributes.GetPropertyTitle(e.Entity.GetType(), p.Name),
                    ContextUser = GetUsuarioLogado(),
                    ObjectName = AnnotationsAttributes.GetClassTitle(e.Entity.GetType())
                };

                e.Session.Save(aud);
            }

            return;


        }
    }
}
